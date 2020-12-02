using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp
{
    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int OIB { get; set; }
        public int BrojMobitela { get; set; }
        public Person()
        {
            InputPersonInfo();
        }
        public Person(string firstName, string lastName, int oib, int brojMobitela)
        {
            FirstName = firstName;
            LastName = lastName;
            OIB = oib;
            BrojMobitela = brojMobitela;
        }
        public void InputPersonInfo()
        {
            Console.Write("Unesite ime osobe:");
            FirstName = Console.ReadLine();

            Console.Write("Unesite prezime osobe:");
            LastName = Console.ReadLine();

            Console.Write("Unesite OIB osobe:");
            OIB = Admin.NumberEntry();
            Console.Write("Unesite broj mobitela osobe:");
            BrojMobitela = Admin.NumberEntry();
        }
    }
}
