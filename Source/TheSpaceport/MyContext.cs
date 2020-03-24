using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheSpaceport
{
    public partial class MyContext : DbContext
    {
        public string defaultConnectionString;

        public MyContext(string a)
        {
            defaultConnectionString = a;
        }

        public DbSet<DatabasePerson> Persons { get; set; }
        public DbSet<DatabaseStarship> Spaceships { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContext)
        {
            //dbContext.UseSqlServer(@"Data Source=den1.mssql8.gear.host;Initial Catalog=thespaceportdb;User id=thespaceportdb;password=Ld0RW!-xKvLa;");
            dbContext.UseSqlServer(defaultConnectionString);
        }
    }
}