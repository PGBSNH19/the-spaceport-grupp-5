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
            ControlParkingspace();
            //predicate.DatabasePerson.PersonID == predicate.DatabaseStarship.PersonID
            //    &&


        }

        public static void ControlParkingspace()
        {
            MyContext myContext = new MyContext();
            var availableSlots = myContext.Spaceships.Where(p => p.Payed == false).ToList();
            if(availableSlots.Count < 20)
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