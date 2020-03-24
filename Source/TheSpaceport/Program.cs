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
            ControlParkingspace();
            
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
                //Create ship
            }
            else
            {
                var test = new CreateNewCustomer().AddNameToPerson(name).AddFunds().StarshipControl().Charge().AddToDataBase();
            }
        }
    }

    
}