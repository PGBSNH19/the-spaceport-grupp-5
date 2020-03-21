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
            string test = "luke";
            RestClient client = new RestClient("https://swapi.co/api/");
            //var personRequest = new RestRequest($"people/?search={test}", DataFormat.Json);
            //var personResponse = client.Execute(personRequest);

            var starshipRequest = new RestRequest("starships/?search=death");
            var starshipResponse = client.Execute(starshipRequest);


            //var person = JsonConvert.DeserializeObject<CharacterRoot>(personResponse.Content);
            var starship = JsonConvert.DeserializeObject<StarshipRoot>(starshipResponse.Content);

        }
    }

    public class API
    {
        private static RestClient client = new RestClient("https://swapi.co/api/");

        public CharacterRoot GetCharacter()
        {
            var request = new RestRequest("people/1/", DataFormat.Json);
            var response = client.Get<CharacterRoot>(request);

            return response.Data;
        }


    }
    public class Character
    {
        public string name { get; set; }
        public string birth_year{get; set;}
        public string eye_color { get; set; }
        public string gender { get; set; }
        public string hair_color { get; set; }
        public string height { get; set; }
        public string mass { get; set; }
        public string skin_color { get; set; }
        public string homeworld { get; set; }
        public List<string> films { get; set; }
        public List<string> species { get; set; }
        public List<string> starships { get; set; }
        public List<string> vehicles { get; set; }
        public string url { get; set; }
        public string created { get; set; }
        public string edited { get; set; }
    }

    public class CharacterRoot
    {
        public int count { get; set; }
        public string next { get; set; }
        public object previos { get; set; }
        public List<Character> results { get; set; }
    }

    public class Starship
    {
        public string name { get; set; }
        public string model { get; set; }
        public string starship_class { get; set; }
        public string manufacturer { get; set; }
        public string cost_in_credits { get; set; }
        public string length { get; set; }
        public string crew { get; set; }
        public string passengers { get; set; }
        public string max_atmosphering_speed { get; set; }
        public string hyperdrive_rating { get; set; }
        public string MGLT { get; set; }
        public string cargo_capacity { get; set; }
        public string consumables { get; set; }
        public List<string> films { get; set; }
        public List<string> pilots { get; set; }
        public string url { get; set; }
        public string created { get; set; }
        public string edited { get; set; }
    }

    public class StarshipRoot
    {
        public int count { get; set; }
        public string next { get; set; }
        public object previos { get; set; }
        public List<Starship> results { get; set; }
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