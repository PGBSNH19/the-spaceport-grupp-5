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
                    Console.WriteLine($"Ticket [{Person.Startships[i].ShipID}]: Spaceship: {Person.Startships[i].ShipName}, Amount of days: {Person.Startships[i].NumberOfDays}, " +
                        $"Total price: {Person.Startships[i].PricePerDay * Person.Startships[i].NumberOfDays}, Payed: Yes ");
                }
                else
                {
                    Console.WriteLine($"Ticket [{Person.Startships[i].ShipID}]: Spaceship: {Person.Startships[i].ShipName}, Amount of days: {Person.Startships[i].NumberOfDays}, " +
                        $"Total price: {Person.Startships[i].PricePerDay * Person.Startships[i].NumberOfDays}, Payed: No ");
                }
            }
            Console.WriteLine("Enter Key to go back");
            Console.ReadKey();
            Console.Clear();
        }
    }
}