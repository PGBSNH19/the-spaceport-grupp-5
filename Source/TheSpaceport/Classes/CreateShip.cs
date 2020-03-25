using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using Newtonsoft.Json;

namespace TheSpaceport
{
    public class CreateShip : IAddStarship
    {
        private DatabaseStarship createStarship = new DatabaseStarship();
        private DatabasePerson createPerson { get; set; }

        public CreateShip(DatabasePerson person)
        {
            this.createPerson = person;
        }

        public IAddStarship StarshipControl()
        {
            RestClient client = new RestClient("https://swapi.co/api/");
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
            this.createStarship.PersonID = this.createPerson.PersonID;
            myContext.Add<DatabaseStarship>(this.createStarship);
            myContext.SaveChanges();
            return this;
        }
    }
}