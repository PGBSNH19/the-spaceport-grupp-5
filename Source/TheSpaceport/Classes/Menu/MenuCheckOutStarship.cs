using System.Data;
using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TheSpaceport
{
    public class MenuCheckOutStarship
    {
        private static MyContext context = new MyContext();

        public static void CheckingForShips(DatabasePerson currentPerson)
        {
            var unpayedShips = context.Spaceships.Where(s => s.Person == currentPerson && s.Payed == false).ToList();


            if (unpayedShips.Count > 0)
            {
                ShowAvailableShip(currentPerson, unpayedShips);
            }
            else
            {
                Console.WriteLine($"No ships available for {currentPerson.Name}, press any key to go back to main menu");
                Console.ReadKey();
            }
        }

        public static void ShowAvailableShip(DatabasePerson currentPerson, List<DatabaseStarship> unpayedShips)
        {
            //Program.SelectMenu();
            bool loop = true;
            int selector;
            Console.WriteLine($"{currentPerson.Name} \n" +
                $"Current credits {currentPerson.Credits}\n" +
                $"Avaible ships to checkout: ");
            for (int i = 0; i <= (unpayedShips.Count - 1); i++)
            {               
                    Console.WriteLine($"[{i}] {unpayedShips[i].ShipName} Price: {unpayedShips[i].NumberOfDays * unpayedShips[i].PricePerDay}");
            }

            while (loop)
            {
                Console.WriteLine("Please select a ship to checkout:  ");
                try
                {
                        selector = int.Parse(Console.ReadLine());
                        Console.WriteLine($"you have selected: {unpayedShips[selector].ShipName}");
                        CheckOutShip(currentPerson, (currentPerson.Startships.Where(s=> s.ShipID ==
                        unpayedShips[selector].ShipID).FirstOrDefault()));
                        loop = false;
                }
                catch
                {
                    Console.WriteLine("Invalid input");
                }
            }
        }

        public static void CheckOutShip(DatabasePerson person, DatabaseStarship shipToCheckout)
        {
            int totalsum = shipToCheckout.NumberOfDays * shipToCheckout.PricePerDay;
            if (totalsum <= person.Credits)
            {
                shipToCheckout.Payed = true;
                person.Credits = person.Credits - totalsum;
                using (var myContext = new MyContext())
                {
                    myContext.Entry(myContext.Spaceships.FirstOrDefault(s => s.ShipID == shipToCheckout.ShipID)).CurrentValues.SetValues(shipToCheckout);
                    myContext.Entry(myContext.Persons.FirstOrDefault(p => p.PersonID == person.PersonID)).CurrentValues.SetValues(person);
                    myContext.SaveChanges();
                }
                Console.WriteLine($"The check out for {shipToCheckout.ShipName} succeded, {totalsum} have been withdrawn from your card\n" +
                    $"your current amount of credits: {person.Credits}");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"Not enough credits on your card {person.Name}, please add more funds");
                Console.ReadKey();
            }
        }
    }
}