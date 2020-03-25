using System.Data;
using System.Linq;
using System;
using System.Collections.Generic;

namespace TheSpaceport
{
    public class MenuCheckOutStarship
    {
        public static void CheckingForShips(string name)
        {
            MyContext context = new MyContext();
            var personCheck = context.Persons.Where(p => p.Name == name).ToList();
            var shipCheck = context.Spaceships.Where(p => p.Payed == false && p.Person == personCheck[0]).ToList();
            if (shipCheck.Count > 0)
            {
                ShowAvailableShip(shipCheck, personCheck[0], context);
            }
            else
            {
                Console.WriteLine($"No ships available for {personCheck[0].Name}");
            }
        }

        public static void ShowAvailableShip(List<DatabaseStarship> ships, DatabasePerson person, MyContext context)
        {
            bool loop = true;
            int selector;
            Console.WriteLine($"{person.Name} \n" +
                $"Current credits {person.Credits}\n" +
                $"Avaible ships to checkout: ");
            for (int i = 0; i <= (ships.Count - 1); i++)
            {
                Console.WriteLine($"[{i}] {ships[i].ShipName}");
            }
            while (loop)
            {
                Console.WriteLine("Please select a ship to checkout:  ");
                ConsoleKeyInfo userInput = Console.ReadKey();
                if (char.IsDigit(userInput.KeyChar))
                {
                    selector = int.Parse(userInput.KeyChar.ToString());
                    Console.WriteLine($"you have selected: {ships[selector].ShipName}");
                    CheckOutShip(ships[selector], person, context);
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
                person.Credits = person.Credits - totalsum;
                shipToRemove.Payed = true;
                myContext.Persons.Update(person);
                myContext.Spaceships.Update(shipToRemove);
                myContext.SaveChanges();
                Console.WriteLine($"The check out for {shipToRemove.ShipName} succeded, {totalsum} have been withdrawn from your card\n" +
                    $"your current amount of credits: {person.Credits}");
            }
            else
            {
                Console.WriteLine($"Not enough credits on your card {person.Name}");
            }
        }
    }
}