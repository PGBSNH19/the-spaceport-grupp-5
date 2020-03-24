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

            //MyContext myContext = new MyContext();


            //var test2 = myContext.Persons.Where(p => p.Name == "Luke Skywalker").ToList();
            //var test3 = myContext.Spaceships.Where(p => p.PersonID == test2[0].PersonID && p.Payed == false).ToList();


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

            CheckingForShips("Luke Skywalker");
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

        public static void CheckingForShips(string name)
        {
            
            MyContext context = new MyContext();
            var personCheck = context.Persons.Where(p => p.Name == name).ToList();
            var shipCheck = context.Spaceships.Where(p => p.Payed == false && p.PersonID == personCheck[0].PersonID).ToList();
            if (shipCheck.Count > 0)
            {

                ShowAvailableShip(shipCheck, personCheck[0]);
            }
            else
            {
                Console.WriteLine($"No ships available for {personCheck[0].Name}");
            }

        }

        public static void ShowAvailableShip(List<DatabaseStarship> ships, DatabasePerson person)
        {
            bool loop = false;
            int selector;
            Console.WriteLine($"{person.Name} \n" +
                $"Current credits {person.Credits}\n" +
                $"Avaible ships to checkout: ");
            for (int i = 0; i <= (ships.Count - 1); i++)
            {
                Console.WriteLine($"[{i}] {ships[i].ShipName}");
            }
            while (loop != true)
            {

                Console.WriteLine("Please select a ship to checkout:  ");
                ConsoleKeyInfo userInput = Console.ReadKey();
                if (char.IsDigit(userInput.KeyChar))
                {
                    selector = int.Parse(userInput.KeyChar.ToString());
                    Console.WriteLine($"you have selected: {ships[selector].ShipName}");
                    CheckOutShip(ships[selector], person);
                    loop = true;
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
        }
        public static void CheckOutShip(DatabaseStarship shipToRemove, DatabasePerson person)
        {
            int totalsum = shipToRemove.NumberOfDays * shipToRemove.PricePerDay;
            if (totalsum <= person.Credits)
            {
                MyContext myContext = new MyContext();
                person.Credits = person.Credits - totalsum;
                shipToRemove.Payed = true;
                myContext.Persons.Update(person);
                myContext.Spaceships.Update(shipToRemove);
                myContext.SaveChanges();
                Console.WriteLine($"The check out for {shipToRemove.ShipName} succeded, {totalsum} have been withdrawn from your card\n" +
                    $"your current amount of credits: {person.Credits}");


            }
            else
            {
                Console.WriteLine($"Not enough credits on your card {person.Name}");
            }
        }
    }

    
}