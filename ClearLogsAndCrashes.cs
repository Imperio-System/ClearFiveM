using System;
using System.IO;

namespace ClearFivem
{
    internal class ClearLogsAndCrashes
    {
        static public void Clear()
        {
            string defaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FiveM\\FiveM.app");

            if (Utilities.IsProcessRunning("FiveM"))
            {
                Console.WriteLine("FiveM is currently running. Please close the application before cleaning logs and crashes.");
                return;
            }

            // Validar si la ruta por defecto existe
            string pathToUse = string.Empty;
            if (Directory.Exists(defaultPath))
            {
                pathToUse = defaultPath;
            }
            else
            {
                // Solicitar al usuario la ruta de instalación
                Console.WriteLine("The default installation path for FiveM does not exist.");
                Console.WriteLine("Please enter the path where FiveM is installed (e.g., C:\\Path\\To\\FiveM\\FiveM.app):");
                pathToUse = Console.ReadLine() ?? string.Empty;

                // Validar si la ruta proporcionada por el usuario es válida
                if (!Directory.Exists(pathToUse))
                {
                    Console.WriteLine("The provided path does not exist. Please make sure the path is correct.");
                    return;
                }
            }

            // Proceder con la limpieza de logs y crashes
            var logsPath = Path.Combine(pathToUse, "logs");
            var crashesPath = Path.Combine(pathToUse, "crashes");

            if (Directory.Exists(logsPath))
            {
                Utilities.ClearDirectory(logsPath);
                Console.WriteLine("Logs cleaned successfully.");
            }
            else
            {
                Console.WriteLine("Logs directory does not exist in the provided path.");
            }

            if (Directory.Exists(crashesPath))
            {
                Utilities.ClearDirectory(crashesPath);
                Console.WriteLine("Crashes cleaned successfully.");
            }
            else
            {
                Console.WriteLine("Crashes directory does not exist in the provided path.");
            }
        }
    }
}
