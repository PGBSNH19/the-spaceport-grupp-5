using System;
using System.Collections.Generic;
using System.Text;

namespace TheSpaceport.Classes.Menu
{
    public class MenuProfilePage
    {
        public static void Profile(DatabasePerson Person)
        {
            Console.Clear();
            Console.WriteLine("--------------------Your profile------------------------");
            Console.WriteLine($"Name: {Person.Name}");
            Console.WriteLine($"Customer ID: {Person.PersonID}");
            Console.WriteLine($"Credits: {Person.Credits}");
            Console.WriteLine("--------------------Docking history---------------------");

            for (int i = 0; i < Person.Startships.Count; i++)
            {
                if (Person.Startships[i].Payed == true)
                {
                    Console.WriteLine($"Ticket [{Person.Startships[i].ShipID}]: {Person.Startships[i].ShipName}, was docked here for {Person.Startships[i].NumberOfDays} " +
                        $"days and for a total cost of {Person.Startships[i].PricePerDay * Person.Startships[i].NumberOfDays} and has been checked-out. ");
                }
                else
                {
                    Console.WriteLine($"Ticket [{Person.Startships[i].ShipID}]: {Person.Startships[i].ShipName}, is going to be here for {Person.Startships[i].NumberOfDays} " +
                        $"days and for a total cost of {Person.Startships[i].PricePerDay * Person.Startships[i].NumberOfDays} and has not been checked-out. ");
                }
            }
            Console.WriteLine("Enter Key to go back");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
