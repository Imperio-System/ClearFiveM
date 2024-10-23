using System;
using System.IO;

namespace ClearFivem
{
    internal class DigitalEntitlements
    {
        static public void Clear()
        {
            string defaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DigitalEntitlements");

            if (Utilities.IsProcessRunning("FiveM"))
            {
                Console.WriteLine("FiveM is currently running. Please close the application before cleaning Digital Entitlements.");
                return;
            }

            if (Directory.Exists(defaultPath))
            {
                Utilities.DeleteDirectory(defaultPath);
                Console.WriteLine("Digital Entitlements directory deleted successfully.");
            }
            else
            {
                Console.WriteLine("Digital Entitlements directory does not exist.");
            }
        }
    }
}
