using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ClearFivem
{
    internal class Utilities
    {
        static public void PrintAsciiArt()
        {
            Console.WriteLine("  _____ __  __ _____  ______ _____  _____ ____     _______     _______ _______ ______ __  __ ");
            Console.WriteLine(" |_   _|  \\/  |  __ \\|  ____|  __ \\|_   _/ __ \\   / ____\\ \\   / / ____|__   __|  ____|  \\/  |");
            Console.WriteLine("   | | | \\  / | |__) | |__  | |__) | | || |  | | | (___  \\ \\_/ / (___    | |  | |__  | \\  / |");
            Console.WriteLine("   | | | |\\/| |  ___/|  __| |  _  /  | || |  | |  \\___ \\  \\   / \\___ \\   | |  |  __| | |\\/| |");
            Console.WriteLine("  _| |_| |  | | |    | |____| | \\ \\ _| || |__| |  ____) |  | |  ____) |  | |  | |____| |  | |");
            Console.WriteLine(" |_____|_|  |_|_|    |______|_|  \\_\\_____\\____/  |_____/   |_| |_____/   |_|  |______|_|  |_|");
        }

        static public bool IsProcessRunning(string processName)
        {
            return Process.GetProcessesByName(processName).Any();
        }

        static public void DeleteDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Console.WriteLine($"Directory not found: {path}");
                return;
            }

            try
            {
                var files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                long totalSize = 0;
                int totalFiles = files.Length;
                int deletedFiles = 0;

                foreach (var file in files)
                {
                    try
                    {
                        // Acumular el tamaño del archivo antes de eliminarlo
                        FileInfo fileInfo = new FileInfo(file);
                        totalSize += fileInfo.Length;

                        File.Delete(file);
                        deletedFiles++;
                    }
                    catch
                    {
                        // Omitir errores si el archivo está en uso o no se puede eliminar
                    }

                    DrawProgressBar(deletedFiles, totalFiles);
                }

                // Intentar eliminar el directorio raíz
                Directory.Delete(path, true);
                Console.WriteLine($"\nDirectory deleted successfully. Total space freed: {FormatSize(totalSize)}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting directory {path}: {ex.Message}");
            }
        }

        public static void ClearDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Console.WriteLine($"Directory not found: {path}");
                return;
            }

            var files = Directory.GetFiles(path);
            var directories = Directory.GetDirectories(path);
            long totalSize = 0;
            int totalItems = files.Length + directories.Length;
            int deletedItems = 0;

            foreach (var file in files)
            {
                try
                {
                    // Acumular el tamaño del archivo antes de eliminarlo
                    FileInfo fileInfo = new FileInfo(file);
                    totalSize += fileInfo.Length;

                    File.Delete(file);
                    deletedItems++;
                }
                catch
                {
                    // Omitir errores si el archivo está en uso o no se puede eliminar
                }
                DrawProgressBar(deletedItems, totalItems);
            }

            foreach (var dir in directories)
            {
                try
                {
                    DeleteDirectory(dir); // Recursively delete subdirectories
                    deletedItems++;
                }
                catch
                {
                    // Omitir errores si la carpeta no se puede eliminar
                }
                DrawProgressBar(deletedItems, totalItems);
            }

            Console.WriteLine($"\nDirectory cleaned successfully. Total space freed: {FormatSize(totalSize)}.");
        }

        static public void DrawProgressBar(int progress, int total)
        {
            if (total == 0) total = 1;

            Console.CursorLeft = 0;
            Console.Write("[");
            int totalBars = 30;
            int bars = progress * totalBars / total;

            // Mostrar la barra de progreso
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write(new string(' ', bars));
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write(new string(' ', totalBars - bars));
            Console.BackgroundColor = ConsoleColor.Black;

            Console.Write($"] {progress}/{total} Items cleaned");
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
