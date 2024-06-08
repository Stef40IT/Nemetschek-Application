using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEvents
{
    public class Event
    {
        public Event(string eventName, string eventPlace, string date, string eventType, string lectors)
        {
            this.EventName = eventName;
            this.EventPlace = eventPlace;
            this.Date = date;
            this.EventType = eventType;
            this.Lectors = lectors;
        }

        public string EventName { get; set; }
        public string EventPlace { get; set; }
        public string Date { get; set; }
        public string EventType { get; set; }
        public string Lectors { get; set; }

        public override string ToString()
        {
            return $"The {this.EventType} {this.EventName} with lector/s {this.Lectors} will be held in {this.EventPlace} in {this.Date}.";
        }
    }
}
