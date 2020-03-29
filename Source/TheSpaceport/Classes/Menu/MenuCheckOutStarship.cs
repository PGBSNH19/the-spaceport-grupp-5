using System.Data;
using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TheSpaceport
{
    public class MenuCheckOutStarship
    {
        private static SpaceportContext context = new SpaceportContext();

        public static void CheckingForShips(DatabasePerson currentPerson)
        {
            var unpayedStarships = context.Spaceships.Where(p => p.Person == currentPerson && p.Payed == false).ToList();
            
            if (unpayedStarships.Count > 0)
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
                if(currentPerson.Startships[i].Payed== false)
                {
                    Console.WriteLine($"[{i}] {currentPerson.Startships[i].ShipName} Price: {currentPerson.Startships[i].NumberOfDays * currentPerson.Startships[i].PricePerDay}");
                }
                    
            }

            while (loop)
            {
                Console.WriteLine("Please select a ship to checkout:  ");
                try
                {
                    selector = int.Parse(Console.ReadLine());
                    Console.WriteLine($"you have selected: {currentPerson.Startships[selector].ShipName}");
                    CheckOutShip(currentPerson.Startships[selector], currentPerson);
                    loop = false;
                }
                catch
                {
                    Console.WriteLine("Invalid input");
                }
            }
        }

        public static void CheckOutShip(DatabaseStarship shipToCheckout, DatabasePerson person)
        {
            int totalsum = shipToCheckout.NumberOfDays * shipToCheckout.PricePerDay;
            if (totalsum <= person.Credits)
            {
                shipToCheckout.Payed = true;
                person.Credits = person.Credits - totalsum;
                using (var myContext = new SpaceportContext())
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