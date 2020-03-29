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
            var unPayedShips = context.Persons.Where(p => p.PersonID == currentPerson.PersonID).Select(m => new DatabasePerson { Startships = m.Startships.Where(s => s.Payed == false).ToList() }).FirstOrDefault();

            if (unPayedShips.Startships.Count > 0)
            {
                ShowAvailableShip(currentPerson);
            }
            else
            {
                Console.WriteLine($"No ships available for {currentPerson.Name}, press any key to go back to main menu");
                Console.ReadKey();
            }
        }

        public static void ShowAvailableShip(DatabasePerson currentPerson)
        {
            //Program.SelectMenu();
            bool loop = true;
            int selector;
            Console.WriteLine($"{currentPerson.Name} \n" +
                $"Current credits {currentPerson.Credits}\n" +
                $"Avaible ships to checkout: ");
            for (int i = 0; i <= (currentPerson.Startships.Count - 1); i++)
            {
                if (currentPerson.Startships[i].Payed == false)
                {
                    Console.WriteLine($"[{i}] {currentPerson.Startships[i].ShipName}");
                }
            }

            while (loop)
            {
                Console.WriteLine("Please select a ship to checkout:  ");
                ConsoleKeyInfo userInput = Console.ReadKey();
                if (char.IsDigit(userInput.KeyChar))
                {
                    selector = int.Parse(userInput.KeyChar.ToString());
                    Console.WriteLine($"you have selected: {currentPerson.Startships[selector].ShipName}");
                    CheckOutShip(currentPerson.Startships[selector], currentPerson);
                    loop = false;
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
        }

        public static void CheckOutShip(DatabaseStarship shipToRemove, DatabasePerson person)
        {
            int totalsum = shipToRemove.NumberOfDays * shipToRemove.PricePerDay;
            if (totalsum <= person.Credits)
            {
                shipToRemove.Payed = true;
                person.Credits = person.Credits - totalsum;
                using (var myContext = new MyContext())
                {
                    myContext.Entry(myContext.Spaceships.FirstOrDefault(s => s.ShipID == shipToRemove.ShipID)).CurrentValues.SetValues(shipToRemove);
                    myContext.Entry(myContext.Persons.FirstOrDefault(p => p.PersonID == person.PersonID)).CurrentValues.SetValues(person);
                    myContext.SaveChanges();
                }
                Console.WriteLine($"The check out for {shipToRemove.ShipName} succeded, {totalsum} have been withdrawn from your card\n" +
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