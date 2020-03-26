using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheSpaceport
{
    public class DatabasePerson
    {
        public int PersonID { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public IList<DatabaseStarship> Startships { get; set; }

        public DatabasePerson()
        {
            Startships = new Collection<DatabaseStarship>();
        }
    }
}