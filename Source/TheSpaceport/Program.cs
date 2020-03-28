using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Linq;

namespace TheSpaceport
{
    public class Program : MainMenu
    {
        public static void Main(string[] args)
        {
            //ControlParkingspace();
            Console.WriteLine(@"
 __    __     _                            _____        _____ _            __                                       _
/ / /\ \ \___| | ___ ___  _ __ ___   ___  /__   \___   /__   \ |__   ___  / _\_ __   __ _  ___ ___ _ __   ___  _ __| |_
\ \/  \/ / _ \ |/ __/ _ \| '_ ` _ \ / _ \   / /\/ _ \    / /\/ '_ \ / _ \ \ \| '_ \ / _` |/ __/ _ \ '_ \ / _ \| '__| __|
 \  /\  /  __/ | (_| (_) | | | | | |  __/  / / | (_) |  / /  | | | |  __/ _\ \ |_) | (_| | (_|  __/ |_) | (_) | |  | |_
  \/  \/ \___|_|\___\___/|_| |_| |_|\___|  \/   \___/   \/   |_| |_|\___| \__/ .__/ \__,_|\___\___| .__/ \___/|_|   \__|
                                                                             |_|                  |_|

");

            Console.WriteLine("You have to be in starwars movie to park here");
            Console.WriteLine("plz enter you name to identify your self \n");
            AccessControl();
        }

        // Kollar om personen finns i API
        //AccessControl();
        // komma in i menyn
        //Menu();

        // kollar om det finns ledig parkering
        //ControlParkingspace();

        // kollar om skeppet finns i API
        //StarshipControl();

        //// lägg till pengar i ditt konto
        //AddFunds();

        //// hur lännge du vill parkera
        //Charge();

        //// lägger till i databasen
        //AddToDataBase();

        // skapar eller kollar om

        public static void ControlParkingspace(DatabasePerson person)
        {
            MyContext myContext = new MyContext();
            var availableSlots = myContext.Spaceships.Where(p => p.Payed == false).ToList();
            if (availableSlots.Count < 20)
            {
                Console.WriteLine($"There is { (20 - availableSlots.Count)}");
                var addNewShip = new CreateShip(person).StarshipControl().Charge().UpdateDatabase();
            }
            else
            {
                Console.WriteLine("No parkingslots are available for the moment, please come back later!");
            }
        }

        public static void BackToMenu()
        {
            Console.WriteLine($"[x] Back to main menu");
            string inputKey = Console.ReadLine().ToLower();
            if (inputKey == "x")
            {
                Menu(Login.personCheck);
            }
        }
    }
}