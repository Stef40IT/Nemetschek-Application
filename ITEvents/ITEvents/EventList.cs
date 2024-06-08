﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEvents
{


    public class EventList
    {
        private readonly List<Event> _events = new List<Event>();
        private string name;
        public EventList(string name)
        {
            this.Name = name;
        }

        public string Name 
        {
            
            get => this.Name;
            private set
            {
                if (value == null) throw new ArgumentException("Invalid Event");
                this.name = value;
            }
        }

        public void AddEvent(Event events)
        {
            if(events == null) return;
            this._events.Add(events);
        }


        public override string ToString()
        {
            if (this._events.Count == 0) return $"The list {this.Name} has no available events.";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"List {this.Name} has {this._events.Count} event/s:");
            foreach (var events in this._events)
                sb.AppendLine(events.ToString());

            return sb.ToString().Trim();
        }
    }
}
