using System.Data;
using System.Linq;
using RestSharp;
using System;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace TheSpaceport
{
    public class Login
    {
        private static MyContext context = new MyContext();
        public static DatabasePerson personLogIn;

        public static void AccessControl()
        {   
            RestClient client = new RestClient("https://swapi.co/api/");
            bool loop = true;
            while (loop)
            {
                Console.Write("Please identify yourself, enter your name: ");

                var personRequest = new RestRequest($"people/?search={Console.ReadLine()}", DataFormat.Json);
                var personResponse = client.Execute(personRequest);
                var person = JsonConvert.DeserializeObject<JSONCharacterRoot>(personResponse.Content);

                if (person.results.Count > 0)
                {
                    ControlPersonInDatabase(person.results[0].name);
                    loop = false;
                }
                else
                {
                    Console.WriteLine("Access denied");
                }
            }
        }

        public static void ControlPersonInDatabase(string personName)
        {
            personLogIn = context.Persons.Include(p=> p.Startships).Where(p => p.Name == personName).FirstOrDefault();

            if (personLogIn != null)
            {
                MainMenu.Menu(personLogIn);
            }
            else
            {
                var newCustomer = new CreateNewCustomer().AddNameToPerson(personName).AddFunds().UpdateDatabase();
                personLogIn = context.Persons.Include(p=> p.Startships).Where(p => p.Name == personName).FirstOrDefault();
                MainMenu.Menu(personLogIn);
            }
        }
    }
}