using System;

namespace TheSpaceport
{
    public class MainMenu : Login
    {
        public static void Menu(DatabasePerson person)
        {
            bool menu = true;
            while (menu)
            {
                Console.WriteLine("Welcom to the Spaceport, you have to be in starwars movie to park here \n");
                Console.WriteLine("plz enter you name to identify your self \n");

                Console.WriteLine("[0] Dock your ship");
                Console.WriteLine("[1] Checkout ship");
                Console.WriteLine("[2] Docking history");
                Console.WriteLine("[3] Exit");

                string option = Console.ReadLine();

                Console.Clear();

                switch (option)
                {
                    case "0":
                        Program.ControlParkingspace(person);
                        break;

                    case "1":
                        MenuCheckOutStarship.CheckingForShips(person);
                        break;

                    case "2":
                        MenuCheckOutStarship.History(person);
                        break;

                    case "3":
                        MenuCheckOutStarship.History(person);
                        break;

                    case "4":
                        Console.WriteLine("Exit program");
                        Console.ReadKey();
                        menu = false;
                        break;
                }
            }
        }
    }
}