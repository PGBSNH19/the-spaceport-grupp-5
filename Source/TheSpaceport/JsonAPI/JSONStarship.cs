using System.Collections.Generic;

namespace TheSpaceport
{
    public class JSONStarship
    {
        public string name { get; set; }
    }

    public class JSONStarshipRoot
    {
        public int count { get; set; }
        public string next { get; set; }
        public object previos { get; set; }
        public List<JSONStarship> results { get; set; }
    }
}