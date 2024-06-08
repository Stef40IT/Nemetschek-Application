﻿using System;
using System.Collections.Generic;
using System.Xml.Linq;
namespace ITEvents
{
    internal class Program
    {
        static Dictionary<int, Event> _events = new Dictionary<int, Event> ();
        static Dictionary<string, EventList> eventsList= new Dictionary<string, EventList> ();
        static void Main(string[] args)
        {
            string input;
            Console.WriteLine("Create list of events using the command CreateList");
            while ((input = Console.ReadLine()) != "STOP")
            {
                
                string[] splittedInput = input.Split(' ');
                string command = splittedInput[0];

                switch (command)
                {
                    case "AddEvent":
                        AddEvent(splittedInput[1], splittedInput[2], splittedInput[3], splittedInput[4], splittedInput[5], splittedInput[6]);
                        break;
                    case "CreateList":
                        CreateEventList(splittedInput[1]);
                        break;
                    default:
                        Console.WriteLine("invalid command!");
                        break;
                }
                Console.WriteLine("Use one of the following commands: ");
                Console.WriteLine("    AddEvent(to add a new event to your list with events)");

            }
        }
        private static void AddEvent(string eventName, string eventPlace, string date, string eventType, string lectors, string eventListName)
        {
            try
            {
                Event events = new Event(eventName, eventPlace, date, eventType, lectors);
                if (!eventsList.ContainsKey(eventListName))
                {
                    Console.WriteLine("Could not add this event to your list with events.");
                    return;
                }
                EventList eventList = eventsList[eventListName];
                eventList.AddEvent(events);
                Console.WriteLine($"You added the event {eventName} to the list with events.");

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void CreateEventList(string name)
        {

            try
            {
                EventList eventList = new EventList(name);
                eventsList.Add(name, eventList);
                Console.WriteLine($"You created new list of events {name}.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);

            }
        }
    }
}
