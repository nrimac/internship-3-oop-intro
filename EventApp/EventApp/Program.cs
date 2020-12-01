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
        public string EventType { get; set; } = "Coffee";
        public List<Person> AttendingGuests { get; set; } = new List<Person>();
        public Event(string name, string eventType, DateTime startTime, DateTime endTime)
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
                var eventTypeName = Console.ReadLine().ToLower().Trim();

                switch (eventTypeName)
                {
                    case "coffee":
                        EventType = "Coffee";
                        return;

                    case "lecture":
                        EventType = "Lecture";
                        return;

                    case "concert":
                        EventType = "Concert";
                        return;

                    case "study session":
                        EventType = "Study Session";
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
            Console.Clear();
            Console.WriteLine("Jeste li sigurni da zelite nastaviti? y/n");

            while (true)
            {
                var userResponse = Console.ReadLine();

                switch (userResponse)
                {
                    case "y":
                        Console.Clear();
                        return true;
                    case "n":
                        EndText();
                        Console.Clear();
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
        public static void EventsList(List<Event> allEvents)
        {
            Console.WriteLine("Popis eventova:");
            foreach (var Event in allEvents)
            {
                Console.WriteLine(Event.Name);
            }
            Console.WriteLine();
        }
        public static void EndText()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
        public static bool Overlap(Event eventToCheckOverlap,List<Event> allEvents)
        {
            foreach (var Event in allEvents)
            {
                if(Event!=eventToCheckOverlap && 
                    ((eventToCheckOverlap.StartTime>=Event.StartTime && eventToCheckOverlap.StartTime<=Event.EndTime)||
                    (eventToCheckOverlap.EndTime>=Event.StartTime && eventToCheckOverlap.EndTime<=Event.EndTime)))
                {
                    return true;
                }
            }

            return false;
        }
        public static void AddEvent(List<Event> allEvents)
        {
            if (Confirm() == true)
            {
                var newEvent = new Event();

                allEvents.Add(newEvent);

                Console.WriteLine("Event dodan!");
                EndText();
            }
        }
        public static void DeleteEvent(List<Event> allEvents)
        {
            if (Confirm() == true)
            {
                EventsList(allEvents);

                Console.WriteLine("Koji event zelite izbrisati:");
                var userChoice = Console.ReadLine();

                foreach (var Event in allEvents)
                {
                    if(userChoice.ToLower().Trim()==Event.Name.ToLower().Trim())
                    {
                        allEvents.Remove(Event);
                        Console.WriteLine("Event izbrisan!");

                        EndText();
                        return;
                    }
                }

                Console.WriteLine("Event s upisanim imenom ne postoji!");
                EndText();
            }
        }
        public static void EditEvent(List<Event> allEvents)
        {
            if(Confirm()==true)
            {
                EventsList(allEvents);

                Console.WriteLine("Unesite ime eventa koji želite urediti:");
                var userChoice = Console.ReadLine();

                foreach (var Event in allEvents)
                {
                    if(userChoice.Trim().ToLower()==Event.Name.Trim().ToLower())
                    {
                        EditEventChoice(Event,allEvents);

                        EndText();
                        return;
                    }
                }

                Console.WriteLine("Event s upisanim imenom ne postoji!");
                EndText();
            }
        }
        public static void EditEventChoice(Event eventToEdit, List<Event> allEvents)
        {
            Console.WriteLine("Unesite index onoga što želite urediti:");
            Console.WriteLine("1. Ime - {0}\n" +
                "2. Vrijeme početka - {1}\n" +
                "3. Vrijeme završetka - {2}\n" +
                "4. Tip eventa - {3}\n",eventToEdit.Name,eventToEdit.StartTime,eventToEdit.EndTime,eventToEdit.EventType);

            int userChoice;

            do
            {
                while (true)
                {
                    try
                    {
                        userChoice = int.Parse(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Mozete unijeti samo broj!");
                    }
                }

                switch (userChoice)
                {
                    case 1:
                        ChangeEventName(eventToEdit,allEvents);
                        break;

                    case 2:
                        ChangeEventTime(eventToEdit,allEvents,"početno");
                        break;

                    case 3:
                        ChangeEventTime(eventToEdit,allEvents,"završno");
                        break;

                    case 4:
                        ChangeEventType(eventToEdit);
                        break;

                    default:
                        Console.WriteLine("Broj mora biti izmedu 1 i 4!");
                        break;
                }
            } while (userChoice < 1 || userChoice > 4);
        }
        public static void ChangeEventName(Event evenToEdit,List<Event> allEvents)
        {
            Console.WriteLine("Unesite novo ime eventa:");
            var newName = Console.ReadLine();

            foreach (var Event in allEvents)
            {
                if(newName.Trim().ToLower()==Event.Name.Trim().ToLower())
                {
                    Console.WriteLine("Već postoji event sa tim imenom!");
                    return;
                }
            }

            evenToEdit.Name = newName;
            Console.WriteLine("Ime promijenjeno!");
        }
        public static void ChangeEventTime(Event evenToEdit, List<Event> allEvents,string startEnd)
        {
            Console.WriteLine("Unesite novo {0} vrijeme za odabrani event:", startEnd);
            var newTime = DateTime.Parse(Console.ReadLine());

            if (startEnd == "početno")
            {

                if (newTime > evenToEdit.EndTime)
                { 
                    Console.WriteLine("Početno vrijeme ne može biti nakon završnoga!");

                    return;
                }

                var oldTime = evenToEdit.StartTime;
                evenToEdit.StartTime = newTime;

                if (Overlap(evenToEdit, allEvents) == false)
                {
                    evenToEdit.StartTime = newTime;
                    Console.WriteLine("Početno vrijeme za event promijenjeno!");

                    return;
                }

                Console.WriteLine("Već imate zakazan event u to vrijeme!");
                evenToEdit.StartTime = oldTime;

                return;
            }
            else
            {
                if (newTime < evenToEdit.StartTime)
                { 
                    Console.WriteLine("Početno vrijeme ne može biti nakon završnoga!");

                    return;
                }

                var oldTime = evenToEdit.StartTime;
                evenToEdit.EndTime = newTime;

                if (Overlap(evenToEdit, allEvents) == false)
                {
                    evenToEdit.EndTime = newTime;
                    Console.WriteLine("Završno vrijeme za event promijenjeno!");

                    return;
                }

                Console.WriteLine("Već imate zakazan event u to vrijeme!");
                evenToEdit.EndTime = oldTime;

                return;
            }
        }
        public static void ChangeEventType(Event evenToEdit)
        {
            Console.WriteLine("Unesite tip eventa (Coffee,Concert,Lecture,Study Session):");

            while (true)
            {
                var userChoice = Console.ReadLine().Trim();

                if (userChoice.ToLower() == "coffee" || userChoice.ToLower() == "concert" || userChoice.ToLower() == "lecture" || userChoice.ToLower() == "study session")
                {
                    evenToEdit.EventType = userChoice;
                    Console.WriteLine("Tip eventa promijenjen!");
                    return;
                }

                Console.WriteLine("Ne postoji uneseni tip eventa!\n" +
                    "Ponovno unesite:");
            }
        }
        public static void AddGuest(List<Event> allEvents)
        {
            if (Confirm() == true)
            {

            }
        }
        public static void RemoveGuest(List<Event> allEvents)
        {
            if (Confirm() == true)
            {

            }
        }
        public static void EventDetails(List<Event> allEvents,Dictionary<Event,List<Person>> eventsGuests)
        {

        }
        static void Main(string[] args)
        {
            var defaultTime = new DateTime(2020, 12, 6, 17, 0, 0);

            var coffeeEvent = new Event("Kavana", "Coffee", defaultTime.AddDays(1), defaultTime.AddDays(1).AddHours(1));
            var lectureEvent = new Event("DUMP-Predavanje", "Lecture", defaultTime.AddDays(2), defaultTime.AddDays(2).AddHours(1));
            var concertEvent = new Event("Nirvana-Koncert", "Concert", defaultTime.AddDays(3), defaultTime.AddDays(3).AddHours(1));
            var studySessionEvent = new Event("Study Group", "Study Session", defaultTime.AddDays(4), defaultTime.AddDays(4).AddHours(1));

            var allEvents = new List<Event>();

            allEvents.Add(coffeeEvent);
            allEvents.Add(lectureEvent);
            allEvents.Add(concertEvent);
            allEvents.Add(studySessionEvent);

            var per1 = new Person("Ante", "Antić", 11111, 098111111);
            var per2 = new Person("Mate", "Matić", 22222, 098222222);
            var per3 = new Person("Marko", "Markić", 33333, 098333333);

            coffeeEvent.AttendingGuests.Add(per1);
            coffeeEvent.AttendingGuests.Add(per2);
            coffeeEvent.AttendingGuests.Add(per3);

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
                            EditEvent(allEvents);
                            break;
                        case 4:
                            AddGuest(allEvents);
                            break;
                        case 5:
                            RemoveGuest(allEvents);
                            break;
                        case 6:
                            EventDetails(allEvents,eventsGuests);
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
                    Console.WriteLine("Možete unijeti samo broj!");
                    Console.ReadKey();
                }
            }
        }
    }
}