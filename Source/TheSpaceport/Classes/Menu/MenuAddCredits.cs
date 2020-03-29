using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheSpaceport
{
    internal class MenuAddCredits
    {
        public static void AddMoreFunds(DatabasePerson person)
        {
            Console.WriteLine($"You have {person.Credits} in credits");
            Console.WriteLine("Please add credits to your card (Minimum 1000 credits): ");

            bool loop = true;
            while (loop)
            {
                try
                {
                    int inputCreadits = int.Parse(Console.ReadLine());
                    if (inputCreadits >= 1000)
                    {
                        MyContext myContext = new MyContext();
                        person.Credits = inputCreadits + person.Credits;
                        myContext.Entry(myContext.Persons.FirstOrDefault(p => p.PersonID == person.PersonID)).CurrentValues.SetValues(person);
                        myContext.SaveChanges();
                        loop = false;
                    }
                }
                catch
                {
                    Console.WriteLine("Error, please add credits to your card (Minimum 1000 credits): ");
                }
            }
        }
    }
}