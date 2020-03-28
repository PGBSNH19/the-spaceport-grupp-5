using System;
using RestSharp;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace TheSpaceport
{
    public class CreateNewCustomer : IAddPerson, IConfigDatabase
    {
        private MyContext myContext = new MyContext();
        public DatabasePerson createPerson = new DatabasePerson();

        private RestClient client = new RestClient("https://swapi.co/api/");

        public IAddPerson AddNameToPerson(string name)
        {
            Console.WriteLine($"Welcome {name}");
            this.createPerson.Name = name;
            return this;
        }

        public IAddPerson AddFunds()
        {
            Console.WriteLine("Please add credits to your card (Minimum 1000 credits): ");
            bool loop = true;
            while (loop)
            {
                try
                {
                    createPerson.Credits = int.Parse(Console.ReadLine());
                    if (createPerson.Credits >= 1000)
                        loop = false;
                }
                catch
                {
                    Console.WriteLine("Error, please add credits to your card (Minimum 1000 credits): ");
                }
            }
            return this;
        }

        public IConfigDatabase UpdateDatabase()
        {
            myContext.Add<DatabasePerson>(this.createPerson);
            myContext.SaveChanges();
            return this;
        }
    }
}