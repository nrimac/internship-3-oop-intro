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
        public static void PrintEvents(Dictionary<Event, List<Person>> eventsGuests)
        {
            
            Console.WriteLine("Popis eventova:");
            foreach (var item in eventsGuests)
            {
                Console.WriteLine(item.Key.Name);
            }
            

            Console.WriteLine();
        }
        public static void PrintPeople(List<Person> allPeople)
        {
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
        public static bool Overlap(Event eventToCheckOverlap, Dictionary<Event, List<Person>> eventsGuests)
        {
            foreach (var item in eventsGuests)
            {
                if (item.Key != eventToCheckOverlap &&
                    ((eventToCheckOverlap.StartTime >= item.Key.StartTime && eventToCheckOverlap.StartTime <= item.Key.EndTime) ||
                    (eventToCheckOverlap.EndTime >= item.Key.StartTime && eventToCheckOverlap.EndTime <= item.Key.EndTime)))
                {
                    return true;
                }
            }

            return false;
        }
        public static bool EventExists(Dictionary<Event, List<Person>> eventsGuests, string name)
        {
            foreach (var item in eventsGuests)
            {
                if (name.ToLower().Trim() == item.Key.Name.ToLower().Trim())
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
        public static void AddEvent(Dictionary<Event,List<Person>> eventsGuests)
        {
            if (Confirm() == true)
            {
                var newEvent = new Event();

                if(Overlap(newEvent,eventsGuests)==true)
                {
                    Console.WriteLine("Vrijeme eventa se podudara s vremenom već postojećeg eventa, event {0} neće biti dodan!",
                        newEvent.Name);

                    EndText();
                    return;
                }

                if(EventExists(eventsGuests,newEvent.Name)==true)
                {
                    Console.WriteLine("Već postoji event s upisanim imenom, event {0} neće biti dodan!", newEvent.Name);

                    EndText();
                    return;
                }

                if(newEvent.EndTime<=newEvent.StartTime)
                {
                    Console.WriteLine("Vrijeme završetka ne može biti nakon vremena početka eventa!\nEvent neće biti dodan!");

                    EndText();
                    return;
                }

                eventsGuests.Add(newEvent,newEvent.AttendingGuests);

                Console.WriteLine("Event dodan!");
                EndText();
            }
        }
        public static void DeleteEvent(Dictionary<Event, List<Person>> eventsGuests)
        {
            if (eventsGuests.Count == 0)
            {
                Console.WriteLine("Nema upisanih eventova.");

                EndText();
                return;
            }

            if (Confirm() == true)
            {
                var chosenEvent = PickEvent(eventsGuests);

                eventsGuests.Remove(chosenEvent);

                Console.WriteLine("Event izbrisan!");
                EndText();
            }
        }
        public static void EditEvent(Dictionary<Event, List<Person>> eventsGuests)
        {
            if (eventsGuests.Count == 0)
            {
                Console.WriteLine("Nema upisanih eventova.");

                EndText();
                return;
            }

            if (Confirm() == true)
            {
                var chosenEvent = PickEvent(eventsGuests);

                EditEventChoice(chosenEvent, eventsGuests);

                return;
            }
        }
        public static void EditEventChoice(Event eventToEdit, Dictionary<Event, List<Person>> eventsGuests)
        {
            Console.WriteLine("Unesite index onoga što želite urediti:");
            Console.WriteLine("1. Ime - {0}\n" +
                "2. Vrijeme početka - {1}\n" +
                "3. Vrijeme završetka - {2}\n" +
                "4. Tip eventa - {3}\n", eventToEdit.Name, eventToEdit.StartTime, eventToEdit.EndTime, eventToEdit.EventType);

            int userChoice;

            do
            {
                userChoice = NumberEntry();

                switch (userChoice)
                {
                    case 1:
                        ChangeEventName(eventToEdit, eventsGuests);
                        break;

                    case 2:
                        ChangeEventTime(eventToEdit, "početno",eventsGuests);
                        break;

                    case 3:
                        ChangeEventTime(eventToEdit, "završno",eventsGuests);
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
        public static void ChangeEventName(Event evenToEdit, Dictionary<Event, List<Person>> eventsGuests)
        {
            Console.WriteLine("Unesite novo ime eventa:");
            var newName = Console.ReadLine();

            if(EventExists(eventsGuests,newName)==true)
            {
                Console.WriteLine("Već postoji event sa tim imenom!");
                return;
            }

            evenToEdit.Name = newName;
            Console.WriteLine("Ime promijenjeno!");
        }
        public static void ChangeEventTime(Event evenToEdit, string startEnd, Dictionary<Event, List<Person>> eventsGuests)
        {
            Console.WriteLine("Unesite novo {0} vrijeme za odabrani event:", startEnd);

            var newTime = new DateTime();

            while (true)
            {
                try
                {
                    newTime = DateTime.Parse(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Nevrijedeći datum!\nMožete unijeti samo datum i vrijeme u formatu yy/mm/dd hh/minmin");
                }
            }

            if (startEnd == "početno")
            {

                if (newTime > evenToEdit.EndTime)
                {
                    Console.WriteLine("Početno vrijeme ne može biti nakon završnoga!");

                    EndText();
                    return;
                }

                var oldTime = evenToEdit.StartTime;
                evenToEdit.StartTime = newTime;

                if (Overlap(evenToEdit, eventsGuests) == false)
                {
                    evenToEdit.StartTime = newTime;
                    Console.WriteLine("Početno vrijeme za event promijenjeno!");

                    EndText();
                    return;
                }

                Console.WriteLine("Već imate zakazan event u to vrijeme!");
                evenToEdit.StartTime = oldTime;

                EndText();
                return;
            }
            else
            {
                if (newTime < evenToEdit.StartTime)
                {
                    Console.WriteLine("Početno vrijeme ne može biti nakon završnoga!");

                    EndText();
                    return;
                }

                var oldTime = evenToEdit.StartTime;
                evenToEdit.EndTime = newTime;

                if (Overlap(evenToEdit, eventsGuests) == false)
                {
                    evenToEdit.EndTime = newTime;
                    Console.WriteLine("Završno vrijeme za event promijenjeno!");

                    EndText();
                    return;
                }

                Console.WriteLine("Već imate zakazan event u to vrijeme!");
                evenToEdit.EndTime = oldTime;

                EndText();
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
        public static void AddGuest(List<Person> allPeople,Dictionary<Event,List<Person>> eventsGuests)
        {
            if (eventsGuests.Count == 0)
            {
                Console.WriteLine("Nema upisanih eventova.");

                EndText();
                return;
            }

            if (Confirm() == true)
            {
                var chosenEvent = PickEvent(eventsGuests);

                if(EventExists(eventsGuests,chosenEvent.Name)==true)
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

                        AddGuestToEvent(chosenEvent,newPerson);

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
                                if(PersonIsInEvent(Person,chosenEvent))
                                {
                                    Console.WriteLine("Osoba je već na eventu!");

                                    EndText();
                                    return;
                                }

                                AddGuestToEvent(chosenEvent, Person);
                                EndText();
                                return;
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
        public static void AddGuestToEvent(Event chosenEvent ,Person aPerson)
        {
            chosenEvent.AttendingGuests.Add(aPerson);

            Console.WriteLine("Osoba dodana");
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
        public static bool PersonIsInEvent(Person aPerson, Event chosenEvent)
        {
            foreach(var Person in chosenEvent.AttendingGuests)
            {
                if (aPerson == Person)
                    return true;
            }

            return false;
        }
        public static void RemoveGuest(List<Person> allPeople, Dictionary<Event, List<Person>> eventsGuests)
        {
            if (eventsGuests.Count == 0)
            {
                Console.WriteLine("Nema upisanih eventova.");

                EndText();
                return;
            }

            if (Confirm() == true)
            {
                var chosenEvent = PickEvent(eventsGuests);

                if(chosenEvent.AttendingGuests.Count==0)
                {
                    Console.WriteLine("Odabrani event nema nikoga u sebi!");

                    EndText();
                    return;
                }

                Console.WriteLine("Upisite OIB osobe koju želite maknuti:");

                PrintPeople(chosenEvent.AttendingGuests);

                var personalIdNumber = NumberEntry();

                foreach (var Person in chosenEvent.AttendingGuests)
                {
                    if (Person.OIB == personalIdNumber)
                    {
                        chosenEvent.AttendingGuests.Remove(Person);
                        
                        Console.WriteLine("Osoba maknuta s eventa!");

                        EndText();
                        return;
                    }
                }   
            }
        }
        public static void EventDetails(Dictionary<Event, List<Person>> eventsGuests)
        {
            if (eventsGuests.Count == 0)
            {
                Console.WriteLine("Nema upisanih eventova.");

                EndText();
                return;
            }

            while (true)
            {
                switch (EditEventMenu())
                {
                    case 1:
                        PrintEventDetails(eventsGuests);
                        EndText();
                        break;

                    case 2:
                        var pickedEvent = PickEvent(eventsGuests);
                        PrintAttendingGuests(eventsGuests, pickedEvent);
                        break;

                    case 3:
                        var pickedEvent1 = PrintEventDetails(eventsGuests);
                        PrintAttendingGuests(eventsGuests,pickedEvent1);
                        break;

                    case 4:
                        return;

                    default:
                        Console.WriteLine("Možete unijeti samo brojeve izmedu 1 i 4!");
                        EndText();
                        break;
                }
            }
        }
        public static Event PrintEventDetails(Dictionary<Event, List<Person>> eventsGuests)
        {
            var userChoice=PickEvent(eventsGuests);

            foreach (var item in eventsGuests)
            {
                 if (userChoice.Name.Trim().ToLower() == item.Key.Name.Trim().ToLower())
                 { 
                    Console.WriteLine("{0} - {1}\nPočetak: {2}\nKraj: {3}\nTrajanje: {4}\n{5} osoba dolazi na event", item.Key.Name, item.Key.EventType,
                    item.Key.StartTime, item.Key.EndTime, Duration(item.Key), item.Value.Count);

                    return userChoice;
                 }
            }

            return userChoice;
        }
        public static string Duration(Event someEvent)
        {
            TimeSpan duration = someEvent.EndTime - someEvent.StartTime;

            if (duration.Days > 0)
                return string.Format("{0} dana {1} sati {2} minuta", duration.Days, duration.Hours, duration.Minutes);
            if (duration.Hours > 0)
                return string.Format("{0} sati {1} minuta", duration.Hours, duration.Minutes);
            if (duration.Minutes > 0)
                return string.Format("{0} minuta", duration.Minutes);
            return string.Format("0 minuta");
        }
        public static void PrintAttendingGuests(Dictionary<Event, List<Person>> eventsGuests, Event pickedEvent)
        {
            Console.WriteLine("Popis gostiju:");

            if (pickedEvent.AttendingGuests.Count == 0)
            {
                Console.WriteLine("*prazan*");
            }
            else
            {
                PrintPeople(pickedEvent.AttendingGuests);
            }

            EndText();
        }
        public static int EditEventMenu()
        {
            Console.Clear();

            Console.WriteLine("Odaberite opciju:\n" +
                "1. - Ispis detalja eventa (Ime eventa - Type - Start time - End time - Trajanje - Broj ljudi na eventu)\n" +
                "2. - Ispis svih osoba na eventu (Ime - Prezime - Broj mobitela)\n" +
                "3. - Kombinirane prve dvije opcije\n" +
                "4. - Povratak na main menu\n");

            var userChoice = NumberEntry();

            return userChoice;
        }
        public static Event PickEvent(Dictionary<Event, List<Person>> eventsGuests)
        {
            PrintEvents(eventsGuests);

            Console.WriteLine("Unesite ime eventa:");

            while (true)
            {
                var userChoice = Console.ReadLine();

                if (EventExists(eventsGuests, userChoice))
                {
                    foreach (var item in eventsGuests)
                    {
                        if (userChoice.Trim().ToLower() == item.Key.Name.Trim().ToLower())
                            return item.Key;
                    }
                }
                else
                {
                    Console.WriteLine("Ne postoji event s unesenim imenom!\nPonovno unesite:");
                }
            }
        }
    }
}
