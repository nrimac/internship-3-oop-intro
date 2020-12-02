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
}
