using System.ComponentModel.DataAnnotations;

namespace TheSpaceport
{
    public class DatabaseStarship
    {
        public int ShipID { get; set; }

        public DatabasePerson Person { get; set; }
        public string ShipName { get; set; }
        public int PricePerDay { get; set; }
        public int NumberOfDays { get; set; }
        public bool Payed { get; set; }
    }
}