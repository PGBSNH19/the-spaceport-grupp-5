using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheSpaceport
{
    public class DatabasePerson
    {
        [Key]
        public int PersonID { get; set; }

        public string Name { get; set; }
        public int Credits { get; set; }

        [ForeignKey("PersonID")]
        public List<DatabaseStarship> Startships { get; set; }

    }

    
}