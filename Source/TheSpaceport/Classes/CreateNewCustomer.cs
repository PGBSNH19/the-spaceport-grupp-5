using System;
using RestSharp;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace TheSpaceport
{
    public class CreateNewCustomer : IAccessControl
    {
        public DatabasePerson addPerson = new DatabasePerson();
        public DatabaseStarship addStarship = new DatabaseStarship();

        private RestClient client = new RestClient("https://swapi.co/api/");

        public IAccessControl AddNameToCustomer(string name)
        {
            Console.WriteLine($"Welcome {name}");
            this.addPerson.Name = name;
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
            bool a = true;
            do
            {
                Console.Write("Please validate your starship: ");
                var starshipRequest = new RestRequest($"starships/?search={Console.ReadLine()}", DataFormat.Json);
                var starshipResponse = client.Execute(starshipRequest);
                var starship = JsonConvert.DeserializeObject<JSONStarshipRoot>(starshipResponse.Content);

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

    
}