using System;
using System.IO;

namespace ClearFivem
{
    internal class ClearFileKeysBinds
    {
        static public void Clear()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CitizenFX", "fivem.cfg");

            if (Utilities.IsProcessRunning("FiveM"))
            {
                Console.WriteLine("FiveM is currently running. Please close the application before deleting the file.");
                return;
            }

            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                    Console.WriteLine("File keys binds cleaned successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting file {filePath}: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("File keys binds does not exist.");
            }
        }
    }
}
