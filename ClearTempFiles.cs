using System;
using System.IO;

namespace ClearFivem
{
    internal class ClearTempFiles
    {
        static public void Clear()
        {
            string tempPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Temp");

            if (Directory.Exists(tempPath))
            {
                Console.WriteLine("Cleaning temporary files...");
                var directories = Directory.GetDirectories(tempPath);
                var files = Directory.GetFiles(tempPath);
                int totalItems = directories.Length + files.Length;
                int cleanedItems = 0;
                int failedItems = 0;
                long totalSizeFreed = 0;

                foreach (var dir in directories)
                {
                    try
                    {
                        if (Directory.Exists(dir))
                        {
                            // Calcular el tamaño antes de eliminar
                            totalSizeFreed += GetDirectorySize(dir);
                            Directory.Delete(dir, true);
                        }
                        cleanedItems++;
                    }
                    catch (UnauthorizedAccessException)
                    {
                        failedItems++; // Contar los errores de acceso denegado
                    }
                    catch (IOException)
                    {
                        failedItems++; // Contar los errores si el archivo está en uso
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Unexpected error deleting directory {dir}: {ex.Message}");
                        failedItems++;
                    }

                    ShowProgress(cleanedItems + failedItems, totalItems);
                }

                foreach (var file in files)
                {
                    try
                    {
                        if (File.Exists(file))
                        {
                            // Acumular el tamaño del archivo antes de eliminarlo
                            FileInfo fileInfo = new FileInfo(file);
                            totalSizeFreed += fileInfo.Length;

                            File.Delete(file);
                        }
                        cleanedItems++;
                    }
                    catch (UnauthorizedAccessException)
                    {
                        failedItems++; // Contar los errores de acceso denegado
                    }
                    catch (IOException)
                    {
                        failedItems++; // Contar los errores si el archivo está en uso
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Unexpected error deleting file {file}: {ex.Message}");
                        failedItems++;
                    }

                    ShowProgress(cleanedItems + failedItems, totalItems);
                }

                Console.WriteLine("\nTemporary files cleaned successfully.");
                Console.WriteLine($"Total items cleaned: {cleanedItems}");
                Console.WriteLine($"Total items failed to clean: {failedItems}");
                Console.WriteLine($"Total space freed: {FormatSize(totalSizeFreed)}.");
            }
            else
            {
                Console.WriteLine("Temporary files directory does not exist.");
            }
        }

        // Calcular el tamaño total de un directorio (incluidos subdirectorios)
        private static long GetDirectorySize(string dirPath)
        {
            long size = 0;

            try
            {
                // Sumar el tamaño de los archivos en el directorio actual
                DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
                foreach (var file in dirInfo.GetFiles())
                {
                    size += file.Length;
                }

                // Sumar el tamaño de los archivos en subdirectorios
                foreach (var dir in dirInfo.GetDirectories())
                {
                    size += GetDirectorySize(dir.FullName);
                }
            }
            catch
            {
                // Silenciar cualquier error al obtener el tamaño
            }

            return size;
        }

        // Método para mostrar el progreso en la consola
        private static void ShowProgress(int processedItems, int totalItems)
        {
            Console.CursorLeft = 0;
            int barWidth = 50;
            int progressBars = processedItems * barWidth / (totalItems > 0 ? totalItems : 1);

            Console.Write($"[{new string('=', progressBars)}{new string(' ', barWidth - progressBars)}] {processedItems}/{totalItems} Items processed");
        }

        // Método para formatear el tamaño en bytes a KB, MB o GB
        static public string FormatSize(long sizeInBytes)
        {
            string[] sizeUnits = { "B", "KB", "MB", "GB", "TB" };
            double size = sizeInBytes;
            int unitIndex = 0;

            while (size >= 1024 && unitIndex < sizeUnits.Length - 1)
            {
                size /= 1024;
                unitIndex++;
            }

            return $"{size:F2} {sizeUnits[unitIndex]}";
        }
    }
}
