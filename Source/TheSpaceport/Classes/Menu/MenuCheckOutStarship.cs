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

        public static void CheckingForShips(DatabasePerson name)
        {
            //var personCheck = context.Persons.Include(p => p.Startships).Where(p => p.PersonID == name.PersonID).FirstOrDefault();
            //var a = context.Spaceships.Where(p => p.Payed == false);

            //var personCheck = context.Persons.Where(p => p.PersonID == name.PersonID).ToList();
            //var shipCheck = context.Spaceships.Where(p => p.Payed == false && p.Person == personCheck[0]).ToList();

            //List<DatabasePerson> list = new List<DatabasePerson>();
            //List<DatabasePerson> newList = list.Where(p => p.PersonID == name.PersonID && p.Startships.Any(u => u.Payed == false)).ToList();

            var test = context.Persons.Where(p => p.PersonID == name.PersonID).Select(m => new DatabasePerson { PersonID = m.PersonID, Name = m.Name, Credits = m.Credits, Startships = m.Startships.Where(s => s.Payed == false).ToList() }).FirstOrDefault();

            if (test.Startships.Count > 0)
            {
                ShowAvailableShip(test, context);
            }
            else
            {
                Console.WriteLine($"No ships available for {name.Name}");
            }
        }

        public static void ShowAvailableShip(DatabasePerson person, MyContext context)
        {
            bool loop = true;
            int selector;
            Console.WriteLine($"{person.Name} \n" +
                $"Current credits {person.Credits}\n" +
                $"Avaible ships to checkout: ");
            for (int i = 0; i <= (person.Startships.Count - 1); i++)
            {
                if (person.Startships[i].Payed == false)
                {
                    Console.WriteLine($"[{i}] {person.Startships[i].ShipName}");
                }
            }
            while (loop)
            {
                Console.WriteLine("Please select a ship to checkout:  ");
                ConsoleKeyInfo userInput = Console.ReadKey();
                if (char.IsDigit(userInput.KeyChar))
                {
                    selector = int.Parse(userInput.KeyChar.ToString());
                    Console.WriteLine($"you have selected: {person.Startships[selector].ShipName}");
                    CheckOutShip(person.Startships[selector], person, context);
                    loop = false;
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
        }

        public static void CheckOutShip(DatabaseStarship shipToRemove, DatabasePerson person, MyContext myContext)
        {
            int totalsum = shipToRemove.NumberOfDays * shipToRemove.PricePerDay;
            if (totalsum <= person.Credits)
            {
                shipToRemove.Payed = true;
                person.Credits = person.Credits - totalsum;
                myContext.Entry(myContext.Spaceships.FirstOrDefault(s => s.ShipID == shipToRemove.ShipID)).CurrentValues.SetValues(shipToRemove);
                myContext.Entry(myContext.Persons.FirstOrDefault(p => p.PersonID == person.PersonID)).CurrentValues.SetValues(person);
                myContext.SaveChanges();

                Console.WriteLine($"The check out for {shipToRemove.ShipName} succeded, {totalsum} have been withdrawn from your card\n" +
                    $"your current amount of credits: {person.Credits}");
            }
            else
            {
                Console.WriteLine($"Not enough credits on your card {person.Name}");
            }
        }

        public static void History(DatabasePerson name)
        {
            Console.WriteLine("This is your History");

            List<DatabasePerson> list = new List<DatabasePerson>();

            List<DatabasePerson> newList = list.Where(p => p.PersonID == name.PersonID && p.Startships.Any(u => u.Payed == false)).ToList();

            var checkPerson = context.Persons.Where(p => p.PersonID == name.PersonID).ToList();
            var checkShip = context.Spaceships.Where(p => p.Payed == true && p.Person == checkPerson[0]).ToList();

            for (int i = 0; i < checkShip.Count; i++)
            {
                Console.WriteLine($"[{i}] {checkShip[i].ShipName}");
            }
        }
    }
}