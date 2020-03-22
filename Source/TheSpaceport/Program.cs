using System;
using System.Collections.Generic;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Data;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace TheSpaceport
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //string test = "mamma";
            //RestClient client = new RestClient("https://swapi.co/api/");

            ////var starshipRequest = new RestRequest("starships/?search=death");
            ////var starshipResponse = client.Execute(starshipRequest);
            ////var starship = JsonConvert.DeserializeObject<StarshipRoot>(starshipResponse.Content);

            //var personRequest = new RestRequest($"people/?search={test}", DataFormat.Json);
            //var personResponse = client.Execute(personRequest);
            //var person = JsonConvert.DeserializeObject<CharacterRoot>(personResponse.Content);
            var test = new CreateCustomer().PersonControl().StarshipControl();

        }
    }

    public interface IAccessControl
    {
        IAccessControl PersonControl();
        IAccessControl StarshipControl();
    }

    public class CreateCustomer : IAccessControl
    {
        public string name { get; set; }
        public string ship { get; set; }

        private static RestClient client = new RestClient("https://swapi.co/api/");
        public IAccessControl PersonControl()
        {
            Console.Write("Please enter your name: ");
            var personRequest = new RestRequest($"people/?search={Console.ReadLine()}", DataFormat.Json);
            Console.WriteLine("Searching.....");
            var personResponse = client.Execute(personRequest);
            var person = JsonConvert.DeserializeObject<CharacterRoot>(personResponse.Content);
            if(person.results.Count > 0)
            {
                Console.WriteLine($"Welcome {person.results[0].name}");
                this.name = person.results[0].name;
            }
            else
            {
                Console.WriteLine("Access denied");
            }
            return this;
        }
        public IAccessControl StarshipControl()
        {
            Console.Write("Please validate your starship: ");
            var starshipRequest = new RestRequest($"starships/?search={Console.ReadLine()}", DataFormat.Json);
            Console.WriteLine("Searching.....");
            var starshipResponse = client.Execute(starshipRequest);
            var starship = JsonConvert.DeserializeObject<StarshipRoot>(starshipResponse.Content);
            if(starship.results.Count > 0)
            {
                Console.WriteLine($"{starship.results[0].name} ready for parking");
                this.ship = starship.results[0].name;
            }
            else
            {
                Console.WriteLine("Unauthorised spaceship");
            }
            return this;
        }
    }

    

    public class Travler
    {
        public string Name { get; set; }

    }
   
    public class Person
    {
        public int PersonID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class House
    {
        public int HouseID { get; set; }
        public int Room { get; set; }
        public List<Person> Persons { get; set; }
    }

    public class MyContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<House> Houses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContext)
        {
            dbContext.UseSqlServer(@"Data Source=den1.mssql8.gear.host;Initial Catalog=thespaceportdb;User id=thespaceportdb;password=Ld0RW!-xKvLa;");
        }
    }
}