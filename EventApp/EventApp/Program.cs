using System;
using System.Collections.Generic;

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
            EnterPersonInfo();
        }
        public Person(string firstName, string lastName, int oib, int brojMobitela)
        {
            FirstName = firstName;
            LastName = lastName;
            OIB = oib;
            BrojMobitela = brojMobitela;
        }
        public void EnterPersonInfo()
        {
            Console.Write("Unesite ime osobe:");
            FirstName = Console.ReadLine();
            Console.Write("Unesite prezime osobe:");
            LastName = Console.ReadLine();
            Console.Write("Unesite OIB osobe:");
            OIB = int.Parse(Console.ReadLine());
            Console.Write("Unesite broj mobitela osobe:");
            BrojMobitela = int.Parse(Console.ReadLine());
        }
    }
    class Event
    {
        public string Name { get; set; } = "Naziv eventa";
        public DateTime StartTime { get; set; } = new DateTime(2020, 12, 6, 17, 0, 0);
        public DateTime EndTime { get; set; } = new DateTime(2020, 12, 6, 20, 0, 0);
        public int EventType { get; set; } = (int)eventType.Coffee;
        public List<Person> AttendingGuests { get; set; } = new List<Person>();
        public Event(string name, int eventType, DateTime startTime, DateTime endTime)
        {
            Name = name;
            StartTime = startTime;
            EndTime = endTime;
            EventType = eventType;
        }
    }
    public enum eventType
    {
        Coffee,
        Lecture,
        Concert,
        StudySession
    }
    class Program
    {
        public static void PrintOptions()
        {
            Console.Clear();
            Console.WriteLine("Odaberite akciju:\n" +
                "1. Dodavanje eventa\n" +
                "2. Brisanje eventa\n" +
                "3. Edit eventa\n" +
                "4. Dodavanje osobe na event\n" +
                "5. Uklanjanje osobe sa eventa\n" +
                "6. Ispis detalja o eventu\n" +
                "7. Izlaz iz aplikacije\n");
        }
        static void Main(string[] args)
        {
            var defaultTime = new DateTime(2020, 12, 6, 17, 0, 0);

            var coffeeEvent = new Event("Kavana", (int)eventType.Coffee, defaultTime.AddDays(1), defaultTime.AddDays(1));
            var lectureEvent = new Event("DUMP-Predavanje", (int)eventType.Lecture, defaultTime.AddDays(2), defaultTime.AddDays(2));
            var concertEvent = new Event("Nirvana", (int)eventType.Concert, defaultTime.AddDays(3), defaultTime.AddDays(3));
            var studySessionEvent = new Event("Study Group", (int)eventType.StudySession, defaultTime.AddDays(4), defaultTime.AddDays(4));

            var allEvents = new List<Event>();

            allEvents.Add(coffeeEvent);
            allEvents.Add(lectureEvent);
            allEvents.Add(concertEvent);
            allEvents.Add(studySessionEvent);

            var per1 = new Person("Ante", "Antić", 11111, 098111111);
            var per2 = new Person("Mate", "Matić", 22222, 098222222);
            var per3 = new Person("Marko", "Markić", 33333, 098333333);

            var eventsGuests = new Dictionary<Event, List<Person>> {
                {coffeeEvent,coffeeEvent.AttendingGuests },
                {lectureEvent,lectureEvent.AttendingGuests },
                {concertEvent,concertEvent.AttendingGuests },
                {studySessionEvent,studySessionEvent.AttendingGuests }
            };

            while (true)
            {
                PrintOptions();
                try
                {
                    var userChoice = int.Parse(Console.ReadLine());
                    switch (userChoice)
                    {
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        case 7:
                            return;
                        default:
                            break;
                    }
                }
                catch
                {
                    //Add message here
                    Console.WriteLine("");
                    Console.ReadKey();
                }
            }
        }
    }
}
