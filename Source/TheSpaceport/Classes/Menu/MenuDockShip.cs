using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TheSpaceport.Classes.Menu
{
    public class MenuDockShip
    {
        public static void ControlParkingspace(DatabasePerson person)
        {
            MyContext myContext = new MyContext();
            var availableSlots = myContext.Spaceships.Where(p => p.Payed == false).ToList();
            if (availableSlots.Count < 20)
            {
                Console.WriteLine($"There is { (20 - availableSlots.Count)} avaible docking");
                var addNewShip = new CreateShip(person).StarshipControl().Charge().UpdateDatabase();
            }
            else
            {
                Console.WriteLine("No parkingslots are available for the moment, please come back later!");
            }
        }
    }
}
