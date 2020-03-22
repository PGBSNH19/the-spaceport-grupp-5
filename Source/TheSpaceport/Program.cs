﻿using System;
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

namespace TheSpaceport
{
    public class Program
    {
        private static void Main(string[] args)
        {
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
        public DataBasePerson addPerson = new DataBasePerson();
        public DataBaseStartship addStarship = new DataBaseStartship();

        private RestClient client = new RestClient("https://swapi.co/api/");

        public IAccessControl PersonControl()
        {
            bool a = true;
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
                    a = false;
                }
                else
                {
                    Console.WriteLine("Access denied");
                }
            } while (a);
            return this;
        }

        public IAccessControl AddFunds()
        {
            Console.Write("Please add credits to your card (Minimum 1000 credits): ");
            bool loop = false;
            while(loop == false)
            {
                try
                {
                    addPerson.Credits = int.Parse(Console.ReadLine());
                    if(addPerson.Credits >= 1000)
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
            bool a = true;
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
                    a = false;
                }
                else
                {
                    Console.WriteLine("Unauthorised spaceship");
                }
            }

            while (a);
            
            return this;
        }

        public IAccessControl Charge()
        {
            bool loop = false;
            Console.WriteLine($"The parkingcost for {this.addStarship.ShipName} will be {this.addStarship.PricePerDay} per day " +
                $".\nEnter how many days you want to park (Minimum 1 day): ");
            while(loop == false)
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
            myContext.Add<DataBasePerson>(this.addPerson);
            myContext.Add<DataBaseStartship>(this.addStarship);
            myContext.SaveChanges();
            return this;
        }
    }

    public class Travler
    {
        public string Name { get; set; }
    }

    public class DataBasePerson
    {
        [Key]
        public int PersonID { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public List<DataBaseStartship> Startships { get; set; }
        
        
        
    }

    public class DataBaseStartship
    {
        [Key]
        public int ShipID { get; set; }
        public int PersonID { get; set; }
        public string ShipName { get; set; }
        public int PricePerDay { get; set; }
        public int NumberOfDays { get; set; }
        
        
    }

    

    public class MyContext : DbContext
    {
        public DbSet<DataBasePerson> Persons { get; set; }
        public DbSet<DataBaseStartship> Spaceships { get; set; }
       


        protected override void OnConfiguring(DbContextOptionsBuilder dbContext)
        {
            dbContext.UseSqlServer(@"Data Source=den1.mssql8.gear.host;Initial Catalog=thespaceportdb;User id=thespaceportdb;password=Ld0RW!-xKvLa;");
        }
    }
}