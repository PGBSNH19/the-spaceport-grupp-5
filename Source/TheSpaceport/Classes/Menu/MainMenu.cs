using System;

namespace TheSpaceport
{
    public class MainMenu : Login
    {
        public static void Menu()
        {
            bool menu = true;
            while (menu)
            {
                Console.WriteLine("Welcom to the Spaceport, please choose an option \n");
                Console.WriteLine("[0] Dock your ship");
                Console.WriteLine("[1] Checkout ship");
                Console.WriteLine("[2] Docking history");
                Console.WriteLine("[3] Exit");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "0":
                        ControlParkingspace();
                        break;
                    case "1":
                        CheckingForShips("Luke Skywalker");
                        break;
                    case "2":
                        Console.WriteLine("Option 2");
                        break;
                    case "3":
                        Console.WriteLine("Exit program");
                        Console.ReadKey();
                        menu = false;
                        break;
                }
            }
        }
    }
}