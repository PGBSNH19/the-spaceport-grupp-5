using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TheSpaceport;
using RestSharp;


namespace UnitTestSpaceport
{
    
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string test = "luke";
            bool testBool = false;
            var personClient = new RestClient("https://swapi.co/api/people/?/search=");
            var personRequest = new RestRequest(test, DataFormat.Json);
            var content = personClient.Execute(personRequest);
            if(content.IsSuccessful)
            {
                testBool = true;
            }
            Assert.IsTrue(testBool == true);

        }
    }
}
