using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp
{
    static class Admin
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
        public static void PrintEvents(List<Event> allEvents)
        {
            Console.WriteLine("Popis eventova:");
            foreach (var item in allEvents)
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine();
        }
        public static void PrintPeople(List<Person> allPeople)
        {
            Console.WriteLine("Popis ljudi:");
            foreach (var item in allPeople)
            {
                Console.WriteLine(item.FirstName + " " + item.LastName + " - " + item.OIB);
            }
            Console.WriteLine();
        }
        public static void EndText()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
        public static bool Overlap(Event eventToCheckOverlap, List<Event> allEvents)
        {
            foreach (var Event in allEvents)
            {
                if (Event != eventToCheckOverlap &&
                    ((eventToCheckOverlap.StartTime >= Event.StartTime && eventToCheckOverlap.StartTime <= Event.EndTime) ||
                    (eventToCheckOverlap.EndTime >= Event.StartTime && eventToCheckOverlap.EndTime <= Event.EndTime)))
                {
                    return true;
                }
            }

            return false;
        }
        public static bool EventExists(List<Event> allEvents,string name)
        {
            foreach (var Event in allEvents)
            {
                if (name.ToLower().Trim() == Event.Name.ToLower().Trim())
                    return true;
            }
            return false;
        }
        public static int NumberEntry()
        {
            while (true)
            {
                try
                {
                    var integer = int.Parse(Console.ReadLine());
                    return integer;
                }
                catch
                {
                    Console.WriteLine("Možete unijeti samo broj!");
                }
            }
        }
        public static void AddEvent(List<Event> allEvents,Dictionary<Event,List<Person>> eventsGuests)
        {
            if (Confirm() == true)
            {
                var newEvent = new Event();

                if(Overlap(newEvent,allEvents)==true)
                {
                    Console.WriteLine("Vrijeme eventa se podudara s vremenom već postojećeg eventa, event {0} neće biti dodan!",
                        newEvent.Name);

                    return;
                }

                if(EventExists(allEvents,newEvent.Name)==true)
                {
                    Console.WriteLine("Već postoji event s upisanim imenom, event {0} neće biti dodan!", newEvent.Name);

                    return;
                }

                allEvents.Add(newEvent);
                eventsGuests.Add(newEvent,newEvent.AttendingGuests);

                Console.WriteLine("Event dodan!");
                EndText();
            }
        }
        public static void DeleteEvent(List<Event> allEvents)
        {
            if (Confirm() == true)
            {
                PrintEvents(allEvents);

                Console.WriteLine("Koji event zelite izbrisati:");
                var userChoice = Console.ReadLine();

                foreach (var Event in allEvents)
                {
                    if (userChoice.ToLower().Trim() == Event.Name.ToLower().Trim())
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
            if (Confirm() == true)
            {
                PrintEvents(allEvents);

                Console.WriteLine("Unesite ime eventa koji želite urediti:");
                var userChoice = Console.ReadLine();

                foreach (var Event in allEvents)
                {
                    if (userChoice.Trim().ToLower() == Event.Name.Trim().ToLower())
                    {
                        EditEventChoice(Event, allEvents);

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
                "4. Tip eventa - {3}\n", eventToEdit.Name, eventToEdit.StartTime, eventToEdit.EndTime, eventToEdit.EventType);

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
                        ChangeEventName(eventToEdit, allEvents);
                        break;

                    case 2:
                        ChangeEventTime(eventToEdit, allEvents, "početno");
                        break;

                    case 3:
                        ChangeEventTime(eventToEdit, allEvents, "završno");
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
        public static void ChangeEventName(Event evenToEdit, List<Event> allEvents)
        {
            Console.WriteLine("Unesite novo ime eventa:");
            var newName = Console.ReadLine();

            if(EventExists(allEvents,newName)==true)
            {
                Console.WriteLine("Već postoji event sa tim imenom!");
                return;
            }

            evenToEdit.Name = newName;
            Console.WriteLine("Ime promijenjeno!");
        }
        public static void ChangeEventTime(Event evenToEdit, List<Event> allEvents, string startEnd)
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
        public static void AddGuest(List<Event> allEvents,List<Person> allPeople,Dictionary<Event,List<Person>> eventsGuests)
        {
            if (Confirm() == true)
            {
                PrintEvents(allEvents);

                Console.WriteLine("Upišite ime eventa na koji želite dodati osobu:");
                var nameOfEvent = Console.ReadLine();

                if(EventExists(allEvents,nameOfEvent)==true)
                {
                    Console.WriteLine("Unesite 0 ako želite dodati novu osobu ili 1 ako želite dodati postojeću osobu:");
                    int userChoice;

                    while(true)
                    { 
                        userChoice = NumberEntry();

                        if (userChoice != 0 && userChoice != 1)
                        { Console.WriteLine("Mozete unijeti samo 0 ili 1!\nPonovno unesite:"); }
                        else
                        { break; }

                    }

                    if(userChoice==0)
                    {
                        //NEW PERSON

                        var newPerson = new Person();

                        if(PersonExists(newPerson,allPeople))
                        {
                            Console.WriteLine("Već postoji osoba s istim OIB-om!");
                            return;
                        }

                        allPeople.Add(newPerson);

                        AddGuestToEvent(eventsGuests,nameOfEvent,newPerson);

                        EndText();
                        return;
                    }
                    else
                    {
                        //EXISTING PERSON

                        PrintPeople(allPeople);

                        Console.WriteLine("Unesite OIB osobe koju želite dodati na event:");
                        var personalIdNumber = NumberEntry();

                        foreach (var Person in allPeople)
                        {
                            if(Person.OIB==personalIdNumber)
                            {
                                if(PersonIsInEvent(Person,nameOfEvent,eventsGuests))
                                {
                                    Console.WriteLine("Osoba je već na eventu!");

                                    EndText();
                                    return;
                                }

                                AddGuestToEvent(eventsGuests, nameOfEvent, Person);
                            }
                        }

                        Console.WriteLine("Ne postoji osoba s upisanim OIB-om");
                        EndText();
                        return;
                    }

                }

                Console.WriteLine("Ne postoji event s upisanim imenom!");
                EndText();
            }
        }
        public static void AddGuestToEvent(Dictionary<Event, List<Person>> eventsGuests,string nameOfEvent,Person aPerson)
        {
            foreach (var Event in eventsGuests)
            {
                if (Event.Key.Name.Trim().ToLower() == nameOfEvent.Trim().ToLower())
                {
                    Event.Value.Add(aPerson);
                    Console.WriteLine("Osoba dodana!");
                }
            }
        }
        public static bool PersonExists(Person aPerson,List<Person> allPeople)
        {
            foreach (var item in allPeople)
            {
                if(item.OIB==aPerson.OIB)
                {
                    return true;
                }
            }

            return false;
        }
        public static bool PersonIsInEvent(Person aPerson, string nameOfEvent, Dictionary<Event, List<Person>> eventsGuests)
        {
            foreach (var Event in eventsGuests)
            {
                if(Event.Key.Name.ToLower().Trim()==nameOfEvent.Trim().ToLower())
                {
                    foreach (var Guest in Event.Value)
                    {
                        if(Guest.OIB==aPerson.OIB)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        public static void RemoveGuest(List<Event> allEvents, List<Person> allPeople, Dictionary<Event, List<Person>> eventsGuests)
        {
            if (Confirm() == true)
            {
                PrintEvents(allEvents);
                Console.WriteLine("Odaberite event s kojeg želite maknuti osobu:");

                var nameOfEvent = Console.ReadLine();

                if (EventExists(allEvents, nameOfEvent))
                {
                    Console.WriteLine("Upisite OIB osobe koju želite maknuti:");
                    PrintPeople(allPeople);
                    var personalIdNumber = NumberEntry();

                    foreach (var Person in allPeople)
                    {
                        if (Person.OIB == personalIdNumber)
                        {
                            if (PersonIsInEvent(Person, nameOfEvent, eventsGuests))
                            {
                                foreach (var Event in eventsGuests)
                                {
                                    if (Event.Key.Name.Trim().ToLower() == nameOfEvent.Trim().ToLower())
                                    {
                                        Event.Value.Remove(Person);
                                        Console.WriteLine("Osoba maknuta s eventa!");

                                        EndText();
                                        return;
                                    }
                                }
                            }
                        }
                    }

                    Console.WriteLine("Osoba već nije na eventu!");
                    EndText();
                    return;
                }
                else
                {
                    Console.WriteLine("Upisani event ne postoji!");
                    EndText();
                }
            }
        }
        public static void EventDetails(List<Event> allEvents, Dictionary<Event, List<Person>> eventsGuests)
        {

        }
    }
}
