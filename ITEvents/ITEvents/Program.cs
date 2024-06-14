using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Xml.Linq;
namespace ITEvents
{
    internal class Program
    {
        static Dictionary<int, Event> _events = new Dictionary<int, Event> ();
        static Dictionary<string, EventList> eventsList= new Dictionary<string, EventList> ();
        static Dictionary<string, Action<string[]>> commands = new Dictionary<string, Action<string[]>>();
        static Dictionary<string, Func<EventList, string[], EventList>> filters = new Dictionary<string, Func<EventList, string[], EventList>>();


        static void Main(string[] args)
        {
            InitializeCommand();
            InitializeFilter();


            string input;
            Console.WriteLine("Create list of events using the command CreateList");

            while ((input = Console.ReadLine()) != "STOP")
            {
                
                string[] splittedInput = input.Split(' ');
                string command = splittedInput[0];

                if (commands.ContainsKey(command)) commands[command](splittedInput);
                else Console.WriteLine("Command Invalid!");
                Console.WriteLine("Use one of the following commands: ");
                Console.WriteLine("    AddEvent(name, place, date, type, lectors(Firstname_Lastname), event list)");
                Console.WriteLine("    Sort(Name, Ascending/Descending, event list)");
                Console.WriteLine("    Filter(criterias(place, data, type, lector), filter, event list)");


            }
        } 
        
        static void InitializeCommand()
        {
            commands["AddEvent"] = AddEvent;
            commands["CreateList"] = CreateEventList;
            commands["Sort"] = Sort;
            commands["Filter"] = ApplyFilters;
            commands["Info"] = EventListInfo;

        }

        

        static void InitializeFilter()
        {
            filters["Place"] = FilterPlace;
            filters["Data"] = FilterDate;
            //filters["Type"] = FilterType;
        }

        

        private static void AddEvent(string[] args)
        {
            try
            {
                string eventName = args[1];
                string eventPlace = args[2];
                DateTime date = DateTime.Parse(args[3]);
                string eventType = args[4];
                string lectors = args[5];
                string eventListName = args[6];

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
        private static void CreateEventList(string[] args)
        {

            try
            {
                string name = args[1];
                EventList eventList = new EventList(name);
                eventsList.Add(name, eventList);
                Console.WriteLine($"You created new list of events {name}.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);

            }
        }
        private static void Sort(string[] args)
        {
            string sortField = args[1];
            string sortOrder = args[2];
            string eventListName = args[3];
            if (!eventsList.ContainsKey(eventListName))
            {
                Console.WriteLine($"Could not get list {eventListName}.");
                return;
            }
            EventList eventList = eventsList[eventListName];

            if (sortField.Equals("Name"))
            {
                if (sortOrder.Equals("Ascending")) eventList.SortAscendingByName();
                else if (sortOrder.Equals("Descending")) eventList.SortDescendingByName();
            }
            Console.WriteLine(eventList.ToString());

        }

        private static void ApplyFilters(string[] args)
        {
            string eventListName = args[args.Length - 1];
            if (!eventsList.ContainsKey(eventListName))
            {
                Console.WriteLine($"Could not get list {eventListName}.");
                return;
            }
            EventList eventList = eventsList[eventListName];

            for (int i = 1; i < args.Length - 1; i += 2)
            {
                string filterType = args[i];
                string filterValue = args[i + 1];

                if (filters.ContainsKey(filterType))
                {
                    eventList = filters[filterType](eventList, new string[] { filterValue });
                }
            }
            Console.WriteLine(eventList.ToString());
        }
        private static void EventListInfo(string[] obj)
        {
            throw new NotImplementedException();
        }

        private static EventList FilterPlace(EventList eventList, string[] args)
        {
            string place = args[0];
            EventList filteredList = new EventList(eventList.Name);
            var filteredEvents = eventList.FilterPlace(place);
            foreach (var e in filteredEvents)
            {
                filteredList.AddEvent(e);
            }
            return filteredList;
        }

        private static EventList FilterDate(EventList eventList, string[] args)
        {
            string date = args[0];
            EventList filteredList = new EventList(eventList.Name);
            var filteredEvents = eventList.FilterDate(date);
            foreach (var e in filteredEvents)
            {
                filteredList.AddEvent(e);
            }
            return filteredList;
        }
        /*private static EventList FilterType(EventList eventList, string[] args)
        {
            string type = args[0];
            EventList filteredList = new EventList(eventList.Name);
            var filteredEvents = eventList.FilterType(type);
            foreach (var e in filteredEvents)
            {
                filteredList.AddEvent(e);
            }
            return filteredList;
        }*/
    }
}
