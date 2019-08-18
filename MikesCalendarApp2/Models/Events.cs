using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MikesCalendarApp2.Models
{
    public class Events
    {
        public List<Event> events = new List<Event>();

        public Events(List<Event> events)
        {
            this.events = events;
        }

        public Events()
        {
        }

        public void AddEvent(int eventId, DateTime startDate, DateTime endDate, string description, string streamGame, string eventName)
        {
            Event e = new Event(eventId,startDate, endDate, description,  streamGame, eventName);

            events.Add(e);
        }

        public Event GetEventById(int eventId)
        {       

            return events[eventId];
        }

    }
}