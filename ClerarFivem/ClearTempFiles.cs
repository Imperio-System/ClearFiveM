using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClerarFivem
{
    internal class ClearTempFiles
    {
        static public void Clear()
        {
            string tempPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Temp");

            if (Directory.Exists(tempPath))
            {
                Console.WriteLine("Cleaning temporary files...");
                Utilities.ClearDirectory(tempPath);
                Console.WriteLine("Temporary files cleaned successfully.");
            }
            else
            {
                Console.WriteLine("Temporary files directory does not exist.");
            }
        }
    }
}
