namespace ClerarFivem
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

            if (Directory.Exists(defaultPath))
            {
                var logsPath = Path.Combine(defaultPath, "logs");
                var crashesPath = Path.Combine(defaultPath, "crashes");

                if (Directory.Exists(logsPath))
                {
                    Utilities.ClearDirectory(logsPath);
                    Console.WriteLine("Logs cleaned successfully.");
                }
                else
                {
                    Console.WriteLine("Logs directory does not exist.");
                }

                if (Directory.Exists(crashesPath))
                {
                    Utilities.ClearDirectory(crashesPath);
                    Console.WriteLine("Crashes cleaned successfully.");
                }
                else
                {
                    Console.WriteLine("Crashes directory does not exist.");
                }
            }
            else
            {
                Console.WriteLine("The default installation path for FiveM does not exist.");
            }
        }
    }
}
