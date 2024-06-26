﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ITEvents
{


    public class EventList
    {
        private readonly List<Event> _events = new List<Event>();
        private string _name;
        public EventList(string name)
        {
            this.Name = name;
        }

        public string Name 
        {
            
            get => this._name;
            private set
            {
                if (value == null) throw new ArgumentException("Invalid Event");
                this._name = value;
            }
        }

        public void AddEvent(Event events)
        {
            if(events == null) return;
            this._events.Add(events);
        }
        public List<Event> SortAscendingByName()
        {
            this._events.Sort((x, y) => string.Compare(x.EventName, y.EventName));

            var devBitesList = new List<Event>();
            var otherList = new List<Event>();

            
            foreach (var e in this._events)
            {
                if (e.EventName.Contains("Dev Bites"))
                    devBitesList.Add(e);

                else otherList.Add(e); 
            }
            devBitesList.AddRange(otherList);
            return devBitesList;
        }


        public List<Event> SortDescendingByName()
        {
            this._events.Sort((x, y) => string.Compare(y.EventName, x.EventName));

            var devBitesList = new List<Event>();
            var otherList = new List<Event>();


            foreach (var e in this._events)
            {
                if (e.EventName.Contains("Dev Bites"))
                    devBitesList.Add(e);

                else otherList.Add(e);
            }
            devBitesList.AddRange(otherList);
            return devBitesList;
        }

        public List<Event> FilterPlace(string place) =>  DevBites(this._events.Where(e => e.EventPlace == place).ToList());
        public List<Event> FilterDate(string date)
        {
            List<Event> filteredEvent;
            if (int.TryParse(date, out int year)) 
                filteredEvent = FilterByYear(year);
            else if (Enum.TryParse(date, true, out Season season))
                filteredEvent = FilterBySeason(season);
            else if (DateTime.TryParseExact(date, "MMMM", System.Globalization.CultureInfo.InvariantCulture, 
                System.Globalization.DateTimeStyles.None, out DateTime month))
                filteredEvent = FilterByMonth(month.Month);
            else throw new ArgumentException("Invalid date format. Please use month name, year, or season.");
            return DevBites(filteredEvent);
        }
        public List<Event> FilterType(string type) => DevBites(this._events.Where(e => e.EventType == type).ToList());

        private List<Event> FilterByYear(int year) => this._events.Where(e => e.Date.Year == year).ToList();
        private List<Event> FilterByMonth(int month) => this._events.Where(e => e.Date.Month == month).ToList();
        private List<Event> FilterBySeason(Season season) => this._events.Where(e => GetSeason(e.Date) == season).ToList();

        public override string ToString()
        {
            if (this._events.Count == 0) return $"The list {this.Name} has no available events.";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"List {this.Name} has {this._events.Count} event/s:");
            foreach (var events in this._events)
                sb.AppendLine(events.ToString());

            return sb.ToString().Trim();
        }

        private List<Event> DevBites(List<Event> events)
        {
            var devBitesEvents = events.Where(e => e.EventName.Contains("Dev Bites", StringComparison.OrdinalIgnoreCase)).ToList();
            var otherEvents = events.Where(e => !e.EventName.Contains("Dev Bites", StringComparison.OrdinalIgnoreCase)).ToList();

            devBitesEvents.AddRange(otherEvents);
            return devBitesEvents;
        }
        public enum Season
        {
            Winter,
            Spring,
            Summer,
            Autumn,
            Fall
        }
        private Season GetSeason(DateTime date)
        {
            int month = date.Month;
            return month switch
            {
                12 or 1 or 2 => Season.Winter,
                3 or 4 or 5 => Season.Spring,
                6 or 7 or 8 => Season.Summer,
                9 or 10 or 11 => Season.Fall
            };
        }
    }
}
