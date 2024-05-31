using ClerarFivem;

class Program 
{
    static void Main(string[] args)
    {
        while (true)
        {
            // Limpiar la consola
            Console.Clear();

            // Información del creador y versión del programa con arte ASCII
            Utilities.PrintAsciiArt();
            Console.WriteLine();
            Console.WriteLine("Welcome to ClearFiveM by Imperio Company v1.0");
            Console.WriteLine();

            // Mostrar menú de opciones
            ShowMenu();

            // Leer opción del usuario
            string option = Console.ReadLine() ?? string.Empty;

            // Limpiar la consola antes de realizar la acción
            Console.Clear();

            switch (option)
            {
                case "1":
                    ClearCache.Clear();
                    break;
                case "2":
                    ClearLogsAndCrashes.Clear();
                    break;
                case "3":
                    DigitalEntitlements.Clear();
                    break;
                case "4":
                    ClearTempFiles.Clear();
                    break;
                case "5":
                    Console.WriteLine("Thanks for using ClearFiveM by Imperio Company. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please select a valid option.");
                    break;
            }

            // Esperar que el usuario presione una tecla para volver al menú
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("Please select an option:");
        Console.WriteLine("1. Clean Cache");
        Console.WriteLine("2. Clean Logs and Crashes");
        Console.WriteLine("3. Clean Digital Entitlements (OPTIONAL)");
        Console.WriteLine("3. Clear file keysBings (OPTIONAL)");
        Console.WriteLine("4. Clean Temporary Files (EXPERIMENTAL)");
        Console.WriteLine("5. Exit");
        Console.Write("Option: ");
    }
}
