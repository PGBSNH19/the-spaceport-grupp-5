using System;
using System.Collections.Generic;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Data;
using RestSharp;

namespace TheSpaceport
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var client = new RestClient("https://swapi.co/api/starships/");
            var request = new RestRequest("?results=&page=1", Method.GET);

            //request.AddUrlSegment("postid", 1);
            //https://swapi.co/api/starships/?results=&page=4

            var content = client.Execute(request).Content;
        }
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