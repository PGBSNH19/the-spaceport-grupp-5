using System.Collections.Generic;

namespace TheSpaceport
{
    public class JSONCharacter
    {
        public string name { get; set; }

    }

    public class JSONCharacterRoot
    {
        public int count { get; set; }
        public string next { get; set; }
        public object previos { get; set; }
        public List<JSONCharacter> results { get; set; }
    }
}