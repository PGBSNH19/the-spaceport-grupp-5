using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TheSpaceport
{
    public class Program : MainMenu
    {
        public static string defaultConnectionString { get; set; }

        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var config = builder.Build();
            defaultConnectionString = config.GetConnectionString("DefaultConnection");

            Menu();
        } 
    }
}