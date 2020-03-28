using System.Data;
using System.Linq;
using RestSharp;
using System;
using Newtonsoft.Json;

namespace TheSpaceport
{
    public class Login
    {
        private static MyContext context = new MyContext();

        public static void AccessControl()
        {
            RestClient client = new RestClient("https://swapi.co/api/");
            bool loop = true;
            while (loop)
            {
                Console.Write("Please enter your name: ");

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
            var personCheck = context.Persons.Where(p => p.Name == personName).FirstOrDefault();

            if (personCheck != null)
            {
                Console.WriteLine($"Welcome back {personName}");
                MainMenu.Menu(personCheck);

                //Console.WriteLine("Do you want to add mony? \n");
                //Console.WriteLine("[0] - Yes\n");
                //Console.WriteLine("[1] - No\n");
                //int yesNo = int.Parse(Console.ReadLine());

                //if (yesNo == 0)
                //{
                //}
                //else if (yesNo == 1)
                //{
                //}
            }
            else
            {
                var newCustomer = new CreateNewCustomer().AddNameToPerson(personName).AddFunds().UpdateDatabase();
                var getCreatedPerson = context.Persons.Where(p => p.Name == personName).FirstOrDefault();
                MainMenu.Menu(getCreatedPerson);
            }
        }
    }
}