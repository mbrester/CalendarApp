using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MikesCalendarApp2.Models
{
    public class Event
    {
        private int eventId;
        private DateTime startDate;
        private DateTime endDate;
        private string description = "";
        private bool streamHappened = false;
        private string streamURL = "";
        private string streamGame = "";
        private string eventName = "";

        public DateTime EndDate { get => endDate; set => endDate = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public string Description { get => description; set => description = value; }
        public bool StreamHappened { get => streamHappened; set => streamHappened = value; }
        public string StreamURL { get => streamURL; set => streamURL = value; }
        public string StreamGame { get => StreamGame1; set => StreamGame1 = value; }
        public string StreamGame1 { get => streamGame; set => streamGame = value; }
        public string EventName { get => eventName; set => eventName = value; }
        public int EventId { get => eventId; set => eventId = value; }

        public Event(int eventId,DateTime startDate, DateTime endDate, string description, string streamGame, string eventName)
        {
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
            StreamHappened = streamHappened;
            EventName = eventName;
            EndDate = endDate;
            StartDate = startDate;
            Description = description;
            StreamGame = streamGame;
            EventName = eventName;
            EventId = eventId;
        }

        public Event()
        {
        }

    }
}