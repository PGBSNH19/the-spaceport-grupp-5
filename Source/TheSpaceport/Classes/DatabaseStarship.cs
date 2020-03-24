using System.ComponentModel.DataAnnotations;

namespace TheSpaceport
{
    public class DatabaseStarship
    {
        [Key]
        public int ShipID { get; set; }

        public int PersonID { get; set; }
        public string ShipName { get; set; }
        public int PricePerDay { get; set; }
        public int NumberOfDays { get; set; }
        public bool Payed { get; set; }
    }

    
}