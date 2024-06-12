using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEvents
{
    public class Event
    {
        public Event(string eventName, string eventPlace, DateTime date, string eventType, string lectors)
        {
            this.EventName = eventName;
            this.EventPlace = eventPlace;
            this.Date = date;
            this.EventType = eventType;
            this.Lectors = lectors;
        }

        public string EventName { get; set;}
        public string EventPlace { get; set; }
        public DateTime Date { get; set; }
        public string EventType { get; set; }
        public string Lectors { get; set; }

        public override string ToString()
        {
            return $"The event {this.EventName} in {this.EventPlace} with date {this.Date.ToString("dd-MM-yyyy")} is type {this.EventType} with lector {this.Lectors}";
        }
    }
}
