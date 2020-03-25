using System;
using RestSharp;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace TheSpaceport
{
    public class CreateNewCustomer : IAddPerson, IAddStarship
    {
        public DatabasePerson createPerson = new DatabasePerson();
        public DatabaseStarship createStarship = new DatabaseStarship();

        private RestClient client = new RestClient("https://swapi.co/api/");

        public IAddPerson AddNameToPerson(string name)
        {
            Console.WriteLine($"Welcome {name}");
            this.createPerson.Name = name;
            return this;
        }

        public IAddPerson AddFunds()
        {
            Console.Write("Please add credits to your card (Minimum 1000 credits): ");
            bool loop = false;
            while (loop == false)
            {
                try
                {
                    createPerson.Credits = int.Parse(Console.ReadLine());
                    if (createPerson.Credits >= 1000)
                        loop = true;
                }
                catch
                {
                    Console.WriteLine("Error, please add credits to your card (Minimum 1000 credits): ");
                }
            }
            return this;
        }

        public IAddStarship StarshipControl()
        {
            bool loop = true;
            while (loop)
            {
                Console.Write("Please validate your starship: ");
                var starshipRequest = new RestRequest($"starships/?search={Console.ReadLine()}", DataFormat.Json);
                var starshipResponse = client.Execute(starshipRequest);
                var starship = JsonConvert.DeserializeObject<JSONStarshipRoot>(starshipResponse.Content);

                if (starship.results.Count > 0)
                {
                    Console.WriteLine($"{starship.results[0].name} ready for parking");
                    this.createStarship.ShipName = starship.results[0].name;
                    this.createStarship.PricePerDay = 1000;
                    loop = false;
                }
                else
                {
                    Console.WriteLine("Unauthorised spaceship");
                }
            }

            return this;
        }

        public IAddStarship Charge()
        {
            bool loop = true;
            Console.WriteLine($"The parkingcost for {this.createStarship.ShipName} will be {this.createStarship.PricePerDay} per day " +
                $".\nEnter how many days you want to park (Minimum 1 day): ");
            while (loop)
            {
                try
                {
                    createStarship.NumberOfDays = int.Parse(Console.ReadLine());
                    loop = false;
                }
                catch
                {
                    Console.WriteLine("Error, please try again");
                }
            }
            return this;
        }

        public IAddStarship AddToDataBase()
        {
            MyContext myContext = new MyContext();
            createStarship.Person = createPerson;
            myContext.Add<DatabasePerson>(this.createPerson);
            myContext.Add<DatabaseStarship>(createStarship);

            myContext.SaveChanges();
            return this;
        }
    }
}