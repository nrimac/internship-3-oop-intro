using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp
{
    class Event
    {
        public string Name { get; set; } = "Naziv eventa";
        public DateTime StartTime { get; set; } = new DateTime(2020, 12, 6, 17, 0, 1);
        public DateTime EndTime { get; set; } = new DateTime(2020, 12, 6, 20, 0, 0);
        public string EventType { 
            get 
            {
                switch (_eventType)
                {
                    case (int)typesOfEvents.Coffee:
                        return "Coffee";
                    case (int)typesOfEvents.Concert:
                        return "Concert";
                    case (int)typesOfEvents.Lecture:
                        return "Lecture";
                    case (int)typesOfEvents.StudySession:
                        return "Study Session";
                    default:
                        return "Coffee";
                }
            } 
            set
            {
                switch (value.Trim().ToLower())
                {
                    case "coffee":
                        _eventType = (int)typesOfEvents.Coffee;
                        break;
                    case "concert":
                        _eventType = (int)typesOfEvents.Concert;
                        break;
                    case "lecture":
                        _eventType = (int)typesOfEvents.Lecture;
                        break;
                    case "study session":
                        _eventType = (int)typesOfEvents.StudySession;
                        break;
                    default:
                        break;
                }
            } 
        }
        private int _eventType = (int)typesOfEvents.Coffee;
        public List<Person> AttendingGuests { get; set; } = new List<Person>();
        public Event(string name, int eventType, DateTime startTime, DateTime endTime)
        {
            Name = name;
            StartTime = startTime;
            EndTime = endTime;
            _eventType = eventType;
        }
        public Event()
        {
            InputEventDetails();
        }
        public void InputEventDetails()
        {
            Console.Write("Unesite ime novoga eventa:");
            Name = Console.ReadLine();

            Console.Write("Unesite datum i vrijeme početka novoga eventa (Format: yyyy/mm/dd hh/minmin):");

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

            Console.Write("Unesite datum i vrijeme završetka novoga eventa(Format: yyyy/mm/dd hh/minmin):");

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
                        _eventType = (int)typesOfEvents.Coffee;
                        return;

                    case "lecture":
                        _eventType=(int)typesOfEvents.Lecture;
                        return;

                    case "concert":
                        _eventType = (int)typesOfEvents.Concert;
                        return;

                    case "study session":
                        _eventType = (int)typesOfEvents.StudySession;
                        return;

                    default:
                        Console.WriteLine("\nNe postoji uneseni tip eventa!\n");
                        break;
                }
            }
        }
    }
}
