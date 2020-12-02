﻿using System;
using System.Collections.Generic;

namespace EventApp
{
    public enum eventType
    {
        Coffee,
        Lecture,
        Concert,
        StudySession
    }
    class Program
    {
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

            var allPeople = new List<Person>();

            allPeople.Add(per1);
            allPeople.Add(per2);
            allPeople.Add(per3);

            //SAMO EVENT "Kavana" IMA POČETNE LJUDE DODANE
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
                Admin.PrintOptions();
               
                var userChoice = Admin.NumberEntry();

                switch (userChoice)
                {
                    case 1:
                        Admin.AddEvent(allEvents,eventsGuests);
                        break;
                    case 2:
                        Admin.DeleteEvent(allEvents);
                        break;
                    case 3:
                        Admin.EditEvent(allEvents);
                        break;
                    case 4:
                        Admin.AddGuest(allEvents,allPeople,eventsGuests);
                        break;
                    case 5:
                        Admin.RemoveGuest(allEvents,allPeople,eventsGuests);
                        break;
                    case 6:
                        Admin.EventDetails(allEvents,eventsGuests);
                        break;
                    case 7:
                        Console.WriteLine("Gasim se...");
                        return;
                    default:
                        break;
                }
            }
        }
    }
}