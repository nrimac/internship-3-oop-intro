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
            OIB = int.Parse(Console.ReadLine());
            Console.Write("Unesite broj mobitela osobe:");
            BrojMobitela = int.Parse(Console.ReadLine());
        }
    }
    class Event
    {
        public string Name { get; set; } = "Naziv eventa";
        public DateTime StartTime { get; set; } = new DateTime(2020, 12, 6, 17, 0, 1);
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
        public Event()
        {
            InputEventDetails();
        }
        public void InputEventDetails()
        {
            Console.Write("Unesite ime novoga eventa:");
            Name = Console.ReadLine();
            
            Console.Write("Unesite datum i vrijeme početka novoga eventa:");
            while (true)
            {
                try 
                { 
                    StartTime = DateTime.Parse(Console.ReadLine());
                    break;
                }
                catch 
                { 
                    Console.WriteLine("Nevrijedeći datum i vrijeme!\nPonovo unesite datum i vrijeme starta eventa:");
                }
            }
            Console.WriteLine(StartTime.Minute);

            Console.Write("Unesite datum i vrijeme završetka novoga eventa:");
            while (true)
            {
                try
                {
                    EndTime = DateTime.Parse(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Nevrijedeći datum i vrijeme!\nPonovo unesite datum i vrijeme zavrsetka eventa:");
                }
            }

            while (true)
            {
                Console.Write("Unesite tip novoga eventa (Coffee, Lecture, Concert, Study Session):");
                var eventTypeName = Console.ReadLine();

                eventTypeName = eventTypeName.Trim();
                eventTypeName = eventTypeName.ToLower();

                switch (eventTypeName)
                {
                    case "coffee":
                        EventType = (int)eventType.Coffee;
                        return;

                    case "lecture":
                        EventType = (int)eventType.Lecture;
                        return;

                    case "concert":
                        EventType = (int)eventType.Concert;
                        return;

                    case "studysession":
                        EventType = (int)eventType.StudySession;
                        return;

                    default:
                        Console.WriteLine("\nNe postoji uneseni tip eventa!\n");
                        break;
                }
            }
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
        public static bool Confirm()
        {
            Console.WriteLine("Jeste li sigurni da zelite nastaviti? y/n");

            while (true)
            {
                var userResponse = Console.ReadLine();

                switch (userResponse)
                {
                    case "y":
                        return true;
                    case "n":
                        return false;
                    default:
                        Console.WriteLine("Krivi unos! Molim vas unesite y ili n (ovisno o tome zelite li nastaviti).");
                        break;
                }
            }
        }
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
        public static void AddEvent(List<Event> allEvents)
        {
            if (Confirm() == true)
            {
                var newEvent = new Event();

                allEvents.Add(newEvent);

                Console.WriteLine("Event dodan!");
            }
        }
        public static void DeleteEvent(List<Event> allEvents)
        {
            if (Confirm() == true)
            {
                Console.WriteLine("Popis eventova:");
                foreach (var Event in allEvents)
                {
                    Console.WriteLine(Event.Name);
                }

                Console.WriteLine("\nKoji event zelite izbrisati:");
                var userChoice = Console.ReadLine();
                userChoice = userChoice.Trim().ToLower();

                foreach (var Event in allEvents)
                {
                    if(userChoice==Event.Name.ToLower().Trim())
                    {
                        allEvents.Remove(Event);
                        Console.WriteLine("Event izbrisan!");
                        return;
                    }
                }

                Console.WriteLine("Event s upisanim imenom ne postoji!");
                Console.ReadKey();
            }
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
                            AddEvent(allEvents);
                            break;
                        case 2:
                            DeleteEvent(allEvents);
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
                            Console.WriteLine("Gasim se...");
                            return;
                        default:
                            break;
                    }
                }
                catch
                {
                    //Add message here
                    Console.WriteLine("Kys");
                    Console.ReadKey();
                }
            }
        }
    }
}
