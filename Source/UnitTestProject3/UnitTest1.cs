using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheSpaceport;
using TheSpaceport.Classes.Menu;
using RestSharp;
using Newtonsoft.Json;

namespace UnitTestProject3
{
    [TestClass]
    public class TestApiValid
    {  
        [TestMethod]
        public void PersonIsValid()
        {
            string validTest = "yoda";
            RestClient client = new RestClient("https://swapi.co/api/");
            var personRequest = new RestRequest($"people/?search={validTest}", DataFormat.Json);
            var personResponse = client.Execute(personRequest);
            var person = JsonConvert.DeserializeObject<JSONCharacterRoot>(personResponse.Content);
            Assert.IsTrue(person.count > 0);
        }

        [TestMethod]
        public void PersonIsNotValid()
        {
            string unvalidTest = "robinsson robban";
            RestClient client = new RestClient("https://swapi.co/api/");
            var personRequest = new RestRequest($"people/?search={unvalidTest}", DataFormat.Json);
            var personResponse = client.Execute(personRequest);
            var person = JsonConvert.DeserializeObject<JSONCharacterRoot>(personResponse.Content);
            Assert.IsFalse(person.count > 0);
        }

        [TestMethod]
        public void SpaceshipIsValid()
        {
            string validSpaceship = "x-wing";
            RestClient client = new RestClient("https://swapi.co/api/");
            var starshipRequest = new RestRequest($"starships/?search={validSpaceship}", DataFormat.Json);
            var starshipResponse = client.Execute(starshipRequest);
            var starship = JsonConvert.DeserializeObject<JSONStarshipRoot>(starshipResponse.Content);
            Assert.IsTrue(starship.count > 0);
        }

        [TestMethod]
        public void SpaceshipIsNotValid()
        {
            string validSpaceship = "volvo amazon";
            RestClient client = new RestClient("https://swapi.co/api/");
            var starshipRequest = new RestRequest($"starships/?search={validSpaceship}", DataFormat.Json);
            var starshipResponse = client.Execute(starshipRequest);
            var starship = JsonConvert.DeserializeObject<JSONStarshipRoot>(starshipResponse.Content);
            Assert.IsFalse(starship.count > 0);
        }
    }
}
