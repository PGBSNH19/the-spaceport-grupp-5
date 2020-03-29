using System;
using TheSpaceport.Classes.Menu;

namespace TheSpaceport
{
    public class MainMenu : Login
    {
        public static void Menu(DatabasePerson person)
        {
            bool menu = true;
            while (menu)
            {
                Console.Clear();

                Welcome();

                Console.WriteLine("---- Main Menu -----");
                Console.WriteLine("[0] Dock your ship");
                Console.WriteLine("[1] Checkout ship");
                Console.WriteLine("[2] Profile page");
                Console.WriteLine("[3] Add more Funds");
                Console.WriteLine("[4] Exit");

                string option = Console.ReadLine();

                Console.Clear();
                Welcome();
                switch (option)
                {
                    case "0":

                        MenuDockShip.ControlParkingspace(person);
                        break;

                    case "1":
                        MenuCheckOutStarship.CheckingForShips(person);
                        break;

                    case "2":
                        MenuProfilePage.Profile(person);
                        break;

                    case "3":
                        MenuAddCredits.AddMoreFunds(person);
                        break;

                    case "4":
                        Console.WriteLine("Exit program");
                        menu = false;
                        break;
                }
            }
        }

        public static void Welcome()
        {
            Console.WriteLine(@"
 _______ _             _____                                       _
|__   __| |           / ____|                                     | |
   | |  | |__   ___  | (___  _ __   __ _  ___ ___ _ __   ___  _ __| |_
   | |  | '_ \ / _ \  \___ \| '_ \ / _` |/ __/ _ \ '_ \ / _ \| '__| __|
   | |  | | | |  __/  ____) | |_) | (_| | (_|  __/ |_) | (_) | |  | |_
   |_|  |_| |_|\___| |_____/| .__/ \__,_|\___\___| .__/ \___/|_|   \__|
                            | |                  | |
                            |_|                  |_|

");
        }
    }
}