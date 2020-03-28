using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TheSpaceport
{
    public class MyContext : DbContext
    {
        public DbSet<DatabasePerson> Persons { get; set; }
        public DbSet<DatabaseStarship> Spaceships { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContext)
        {
            //dbContext.UseSqlServer(@"Data Source=den1.mssql8.gear.host;Initial Catalog=thespaceportdb;User id=thespaceportdb;password=Ld0RW!-xKvLa;");

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var config = builder.Build();
            var defaultConnectionString = config.GetConnectionString("DefaultConnection");
            dbContext.UseSqlServer(defaultConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DatabasePerson>().HasKey(p => p.PersonID);

            modelBuilder.Entity<DatabasePerson>().HasMany(e => e.Startships).WithOne(c => c.Person).IsRequired();

            modelBuilder.Entity<DatabaseStarship>().HasKey(s => s.ShipID);
        }
    }
}