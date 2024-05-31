namespace ClerarFivem
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

            if (Directory.Exists(defaultPath))
            {
                var dataPath = Path.Combine(defaultPath, "data");
                if (Directory.Exists(dataPath))
                {
                    Utilities.DeleteDirectory(dataPath);
                    Console.WriteLine("Cache cleaned successfully.");
                }
                else
                {
                    Console.WriteLine("Data directory does not exist.");
                }
            }
            else
            {
                Console.WriteLine("The default installation path for FiveM does not exist.");
            }
        }
    }
}
