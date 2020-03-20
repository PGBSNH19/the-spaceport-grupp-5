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

namespace TheSpaceport
{
    class Program
    {
        
        static void Main(string[] args)
        {
            


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
