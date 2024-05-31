using System.Diagnostics;

namespace ClerarFivem
{
    internal class Utilities
    {
        static public void PrintAsciiArt()
        {
            Console.WriteLine("    ____                          _          ______                                       ");
            Console.WriteLine("   /  _/___ ___  ____  ___  _____(_)___     / ____/___  ____ ___  ____  ____ _____  __  __");
            Console.WriteLine("   / // __ `__ \\/ __ \\/ _ \\/ ___/ / __ \\   / /   / __ \\/ __ `__ \\/ __ \\/ __ `/ __ \\/ / / /");
            Console.WriteLine(" _/ // / / / / / /_/ /  __/ /  / / /_/ /  / /___/ /_/ / / / / / / /_/ / /_/ / / / / /_/ / ");
            Console.WriteLine("/___/_/_/_/ /_/ .___/\\___/_/  /_/\\____/   \\____/\\____/_/ /_/ /_/ .___/\\__,_/_/ /_/\\__, /  ");
            Console.WriteLine("             /_/                                              /_/                /____/   ");
        }

        static public bool IsProcessRunning(string processName)
        {
            return Process.GetProcessesByName(processName).Any();
        }

        public static void DeleteDirectory(string path)
        {
            var files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
            var directories = Directory.GetDirectories(path, "*", SearchOption.AllDirectories);

            int totalItems = files.Length + directories.Length;
            int deletedItems = 0;

            foreach (var file in files)
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting file {file}: {ex.Message}");
                }
                deletedItems++;
                DrawProgressBar(deletedItems, totalItems);
            }

            foreach (var dir in directories)
            {
                try
                {
                    Directory.Delete(dir, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting directory {dir}: {ex.Message}");
                }
                deletedItems++;
                DrawProgressBar(deletedItems, totalItems);
            }

            try
            {
                Directory.Delete(path, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting directory {path}: {ex.Message}");
            }
            DrawProgressBar(totalItems, totalItems); // Ensure the progress bar shows completion
        }

        public static void ClearDirectory(string path)
        {
            var files = Directory.GetFiles(path);
            int totalFiles = files.Length;
            int deletedFiles = 0;

            foreach (var file in files)
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting file {file}: {ex.Message}");
                }
                deletedFiles++;
                DrawProgressBar(deletedFiles, totalFiles);
            }

            var directories = Directory.GetDirectories(path);
            foreach (var dir in directories)
            {
                try
                {
                    Directory.Delete(dir, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting directory {dir}: {ex.Message}");
                }
                deletedFiles++;
                DrawProgressBar(deletedFiles, totalFiles + directories.Length);
            }
        }

        static public void DrawProgressBar(int progress, int total)
        {
            Console.CursorLeft = 0;
            Console.Write("[");
            int totalBars = 30;
            int bars = progress * totalBars / total;

            for (int i = 0; i < totalBars; i++)
            {
                if (i < bars)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.Write(" ");
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write(" ");
                }
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write($"] {progress}/{total} Files");
        }
    }
}
