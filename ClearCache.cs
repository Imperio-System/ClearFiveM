using System;
using System.IO;

namespace ClearFivem
{
    internal class ClearCache
    {
        static public void Clear()
        {
            string defaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FiveM\\FiveM.app");

            if (Utilities.IsProcessRunning("FiveM"))
            {
                Console.WriteLine("FiveM is currently running. Please close the application before cleaning the cache.");
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

            // Proceder con la limpieza de la caché
            var dataPath = Path.Combine(pathToUse, "data");
            if (Directory.Exists(dataPath))
            {
                Utilities.DeleteDirectory(dataPath);
                Console.WriteLine("Cache cleaned successfully.");
            }
            else
            {
                Console.WriteLine("Data directory does not exist in the provided path.");
            }
        }
    }
}
