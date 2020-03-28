using System;

namespace TheSpaceport
{
    public class MainMenu : Login
    {
        public static CreateNewCustomer a = new CreateNewCustomer();

        public static void Menu(DatabasePerson person)
        {
            bool menu = true;
            while (menu)
            {
                Console.WriteLine("---- Main Menu -----");
                Console.WriteLine("[0] Dock your ship");
                Console.WriteLine("[1] Checkout ship");
                Console.WriteLine("[2] Docking history");
                Console.WriteLine("[3] Add more Funds");
                Console.WriteLine("[4] Exit");

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
                        MenuAddCredits.AddMoreFunds(person);
                        break;

                    case "4":
                        Console.WriteLine("Exit program");
                        menu = false;
                        break;
                }
            }
        }
    }
}