using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TheSpaceport
{
    public class MenuDockShip
    {
        public static void ControlParkingspace(DatabasePerson person)
        {
            //Program.SelectMenu();
            MyContext myContext = new MyContext();
            var availableSlots = myContext.Spaceships.Where(p => p.Payed == false).ToList();
            if (availableSlots.Count < 20)
            {
                Console.WriteLine($"{(20 - availableSlots.Count)} docking spots available");
                var addNewShip = new CreateShip(person).StarshipControl().Charge().UpdateDatabase();
            }
            else
            {
                Console.WriteLine("No docking spots are available for the moment, please come back later!");
                Console.ReadKey();
            }
        }
    }
}