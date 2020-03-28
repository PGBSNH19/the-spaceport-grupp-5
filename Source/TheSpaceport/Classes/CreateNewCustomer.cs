using System;
using RestSharp;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace TheSpaceport
{
    public class CreateNewCustomer : IAddPerson, IConfigDatabase
    {
        private MyContext myContext = new MyContext();
        public DatabasePerson createPerson = new DatabasePerson();

        public string Name;

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
                    int inputCreadit = int.Parse(Console.ReadLine());
                    if (inputCreadit >= 1000)
                    {
                        createPerson.Credits = inputCreadit;
                        loop = false;
                    }
                }
                catch
                {
                    Console.WriteLine("Error, please add credits to your card (Minimum 1000 credits): ");
                }
            }
            return this;
        }

        public void AddMoreFunds(DatabasePerson person)
        {
            Console.WriteLine("Please add credits to your card (Minimum 1000 credits): ");
            int inputCreadits = int.Parse(Console.ReadLine());

            bool loop = true;
            while (loop)
            {
                try
                {
                    if (inputCreadits >= 1000)
                    {
                        person.Credits = inputCreadits + person.Credits;
                        myContext.Entry(myContext.Persons.FirstOrDefault(p => p.PersonID == person.PersonID)).CurrentValues.SetValues(person.Credits);
                        myContext.SaveChanges();
                        loop = false;
                    }
                }
                catch
                {
                    Console.WriteLine("Error, please add credits to your card (Minimum 1000 credits): ");
                }
            }
        }

        public IConfigDatabase UpdateDatabase()
        {
            myContext.Add<DatabasePerson>(this.createPerson);
            myContext.SaveChanges();
            return this;
        }
    }
}