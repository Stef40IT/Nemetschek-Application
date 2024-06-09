using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return this._events;
        }
        public List<Event> SortDescendingByName()
        {
            this._events.Sort((x, y) => string.Compare(y.EventName, x.EventName));
            return this._events;
        }
        public List<string> FilterPlace(string place) => this._events.Where(x => x.EventPlace == place).Select(x => x.EventName).ToList();
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
