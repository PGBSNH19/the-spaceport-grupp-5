using System;
using System.Collections.Generic;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Data;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Configuration;
using System.IO;

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

            var test = new CreateCustomer().PersonControl().AddFunds().StarshipControl().Charge().AddToDataBase();
        }
    }

    public interface IAccessControl
    {
        IAccessControl PersonControl();

        IAccessControl AddFunds();

        IAccessControl StarshipControl();

        IAccessControl Charge();

        IAccessControl AddToDataBase();
    }

    public class CreateCustomer : IAccessControl
    {
        public DatabasePerson addPerson = new DatabasePerson();
        public DatabaseStarship addStarship = new DatabaseStarship();

        private RestClient client = new RestClient("https://swapi.co/api/");

        public IAccessControl PersonControl()
        {
            bool loop = true;
            do
            {
                Console.Write("Please enter your name: ");
                var personRequest = new RestRequest($"people/?search={Console.ReadLine()}", DataFormat.Json);
                var personResponse = client.Execute(personRequest);
                var person = JsonConvert.DeserializeObject<CharacterRoot>(personResponse.Content);
                if (person.results.Count > 0)
                {
                    Console.WriteLine($"Welcome {person.results[0].name}");
                    this.addPerson.Name = person.results[0].name;
                    loop = false;
                }
                else
                {
                    Console.WriteLine("Access denied");
                }
            } while (loop);
            return this;
        }

        public IAccessControl AddFunds()
        {
            Console.Write("Please add credits to your card (Minimum 1000 credits): ");
            bool loop = false;
            while (loop == false)
            {
                try
                {
                    addPerson.Credits = int.Parse(Console.ReadLine());
                    if (addPerson.Credits >= 1000)
                        loop = true;
                }
                catch
                {
                    Console.WriteLine("Error, please add credits to your card (Minimum 1000 credits): ");
                }
            }
            return this;
        }

        public IAccessControl StarshipControl()
        {
            bool loop = true;
            do
            {
                Console.Write("Please validate your starship: ");
                var starshipRequest = new RestRequest($"starships/?search={Console.ReadLine()}", DataFormat.Json);
                var starshipResponse = client.Execute(starshipRequest);
                var starship = JsonConvert.DeserializeObject<StarshipRoot>(starshipResponse.Content);

                if (starship.results.Count > 0)
                {
                    Console.WriteLine($"{starship.results[0].name} ready for parking");
                    this.addStarship.ShipName = starship.results[0].name;
                    this.addStarship.PricePerDay = 1000;
                    loop = false;
                }
                else
                {
                    Console.WriteLine("Unauthorised spaceship");
                }
            } while (loop);
            return this;
        }

        public IAccessControl Charge()
        {
            bool loop = false;
            Console.WriteLine($"The parkingcost for {this.addStarship.ShipName} will be {this.addStarship.PricePerDay} per day " +
                $".\nEnter how many days you want to park (Minimum 1 day): ");
            while (loop == false)
            {
                try
                {
                    addStarship.NumberOfDays = int.Parse(Console.ReadLine());
                    loop = true;
                }
                catch
                {
                    Console.WriteLine("Error, please try again");
                }
            }
            return this;
        }

        public IAccessControl AddToDataBase()
        {
            MyContext myContext = new MyContext();
            myContext.Add<DatabasePerson>(this.addPerson);
            this.addStarship.PersonID = int.Parse(myContext.Entry(this.addPerson).Property("PersonID").CurrentValue.ToString());
            myContext.Add<DatabaseStarship>(this.addStarship);
            myContext.SaveChanges();
            return this;
        }
    }

    public class Travler
    {
        public string Name { get; set; }
    }

    public class DatabasePerson
    {
        [Key]
        public int PersonID { get; set; }

        public string Name { get; set; }
        public int Credits { get; set; }

        [ForeignKey("PersonID")]
        public List<DatabaseStarship> Startships { get; set; }
    }

    public class DatabaseStarship
    {
        [Key]
        public int ShipID { get; set; }

        public int PersonID { get; set; }
        public string ShipName { get; set; }
        public int PricePerDay { get; set; }
        public int NumberOfDays { get; set; }
    }

    //public class MyContext : DbContext
    //{
    //    public DbSet<DatabasePerson> Persons { get; set; }
    //    public DbSet<DatabaseStarship> Spaceships { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder dbContext)
    //    {
    //        //dbContext.UseSqlServer(@"Data Source=den1.mssql8.gear.host;Initial Catalog=thespaceportdb;User id=thespaceportdb;password=Ld0RW!-xKvLa;");
    //        dbContext.UseSqlServer(defaultConnectionString);
    //    }
    //}
}