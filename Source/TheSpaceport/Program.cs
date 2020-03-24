using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Data;
using System.Linq;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;
using System.IO;
using RestSharp;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TheSpaceport
{
    public class Program
    {
        public static string defaultConnectionString { get; set; }

        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var config = builder.Build();
            defaultConnectionString = config.GetConnectionString("DefaultConnection");

            MyContext myContext = new MyContext();


            var test2 = myContext.Persons.Where(p => p.Name == "Luke Skywalker").ToList();
            var test3 = myContext.Spaceships.Where(p => p.PersonID == test2[0].PersonID && p.Payed == false).ToList();


            //var test = myContext.Persons.
            //    Join(myContext.Spaceships,
            //    person => person.PersonID,
            //    ship => ship.PersonID,
            //    (person, ship) => new { DatabasePerson = person, DatabaseStarship = ship })
            //    .Where(predicate => predicate.DatabasePerson.Name == "Luke Skywalker" && predicate.DatabaseStarship.Payed == false).ToList();

            //Console.WriteLine(test[0].DatabasePerson.Name);
            //foreach (var item in test[0].DatabasePerson.Startships)
            //{
            //    Console.WriteLine($"ID: {item.ShipID} Name: {item.ShipName}");
            //}
            //ControlParkingspace();
            //predicate.DatabasePerson.PersonID == predicate.DatabaseStarship.PersonID
            //    &&

            CheckShip("Luke Skywalker");
        }

        public static void ControlParkingspace()
        {
            MyContext myContext = new MyContext();
            var availableSlots = myContext.Spaceships.Where(p => p.Payed == false).ToList();
            if (availableSlots.Count < 20)
            {
                AccessControl();
            }
            else
            {
                Console.WriteLine("No sparkingslots are available for the moment, please come back later!");
            }
        }

        public static void AccessControl()
        {
            RestClient client = new RestClient("https://swapi.co/api/");
            bool loop = false;
            while (loop == false)
            {
                Console.Write("Please enter your name: ");
                var personRequest = new RestRequest($"people/?search={Console.ReadLine()}", DataFormat.Json);
                var personResponse = client.Execute(personRequest);
                var person = JsonConvert.DeserializeObject<JSONCharacterRoot>(personResponse.Content);
                if (person.results.Count > 0)
                {
                    ControlPersonInDatabase(person.results[0].name);
                    loop = true;
                }
                else
                {
                    Console.WriteLine("Access denied");
                }
            }
        }
        public static void ControlPersonInDatabase(string name)
        {
            MyContext context = new MyContext();
            var personCheck = context.Persons.Where(p => p.Name == name).ToList();
            if (personCheck.Count > 0)
            {
                Console.WriteLine($"Welcome back {personCheck[0].Name}");
                var newStarship = new CreateShip(personCheck[0]).StarshipControl().Charge().AddToDataBase();
            }
            else
            {
                var newCustomer = new CreateNewCustomer().AddNameToPerson(name).AddFunds().StarshipControl().Charge().AddToDataBase();
            }
        }

        public static void CheckShip(string name)
        {
            bool loop = false;
            int selector;
            MyContext context = new MyContext();
            var personCheck = context.Persons.Where(p => p.Name == name).ToList();
            var shipCheck = context.Spaceships.Where(p => p.Payed == false && p.PersonID == personCheck[0].PersonID).ToList();
            if (shipCheck.Count > 0)
            {
                Console.WriteLine($"Available Ships for {name}: ");
                for (int i = 0; i <= (shipCheck.Count - 1); i++)
                {
                    Console.WriteLine($"[{i}] {shipCheck[i].ShipName}");
                }
                while (loop != true)
                {

                    Console.WriteLine("Please select a ship to checkout:  ");
                    ConsoleKeyInfo userInput = Console.ReadKey();
                    Console.WriteLine();
                    if (char.IsDigit(userInput.KeyChar))
                    {
                        selector = int.Parse(userInput.KeyChar.ToString());
                        Console.WriteLine($"you have selected: {shipCheck[selector].ShipName}");
                        int ShipID = shipCheck[selector].ShipID;

                        CheckOutShip(ShipID);

                        loop = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                    }
                }
            }
            else
            {
                Console.WriteLine("No ships available");
            }

        }
        public static void CheckOutShip(int ID)
        {
            MyContext context = new MyContext();

            var shipCheckout = context.Spaceships.Where(p => p.ShipID == ID).ToList();

            var personCheckout = context.Persons.Where(p => p.PersonID == shipCheckout[0].PersonID).ToList();

            Console.WriteLine($"Checking credits for: {personCheckout[0].Name}...");

            int totCost = shipCheckout[0].PricePerDay * shipCheckout[0].PricePerDay;

            Console.WriteLine("Total cost for your parking: " + " " + totCost);

            if (totCost < personCheckout[0].Credits)
            {
                Console.WriteLine("Spaceship successfully checked-out, amount remaining on account  " + (personCheckout[0].Credits - totCost));
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Not enough credits registered on acocunt, please register more credits on you account to checkout your spaceship.");
            }


        }
    }
    public class CreateShip : IAddStarship
    {
        DatabaseStarship createStarship = new DatabaseStarship();
        DatabasePerson createPerson { get; set; }
        
        public CreateShip(DatabasePerson person)
        {
            this.createPerson = person;
        }

        public IAddStarship StarshipControl()
        {
            RestClient client = new RestClient("https://swapi.co/api/");
            bool loop = false;
            while (loop == false)
            {
                Console.Write("Please validate your starship: ");
                var starshipRequest = new RestRequest($"starships/?search={Console.ReadLine()}", DataFormat.Json);
                var starshipResponse = client.Execute(starshipRequest);
                var starship = JsonConvert.DeserializeObject<JSONStarshipRoot>(starshipResponse.Content);

                if (starship.results.Count > 0)
                {
                    Console.WriteLine($"{starship.results[0].name} ready for parking");
                    this.createStarship.ShipName = starship.results[0].name;
                    this.createStarship.PricePerDay = 1000;
                    loop = true;
                }
                else
                {
                    Console.WriteLine("Unauthorised spaceship");
                }
            }
            return this;
        }
        public IAddStarship Charge()
        {
            bool loop = false;
            Console.WriteLine($"The parkingcost for {this.createStarship.ShipName} will be {this.createStarship.PricePerDay} per day " +
                $".\nEnter how many days you want to park (Minimum 1 day): ");
            while (loop == false)
            {
                try
                {
                    createStarship.NumberOfDays = int.Parse(Console.ReadLine());
                    loop = true;
                }
                catch
                {
                    Console.WriteLine("Error, please try again");
                }
            }
            return this;
        }
        public IAddStarship AddToDataBase()
        {
            MyContext myContext = new MyContext();
            this.createStarship.PersonID = this.createPerson.PersonID;
            myContext.Add<DatabaseStarship>(this.createStarship);
            myContext.SaveChanges();
            return this;
        }
       
    }

    
}