using MikesCalendarApp2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MikesCalendarApp2.Controllers
{
    public class HomeController : Controller
    {
        public static class Globals
        {
            public const Int32 BUFFER_SIZE = 10; // Unmodifiable
            public static String FILE_NAME = "Output.txt"; // Modifiable
            public static readonly String CODE_PREFIX = "US-"; // Unmodifiable
            public static DateTime date = DateTime.Now;
            public static int month = date.Month;
           
                //["UserName"] = UserNameTextBox.Text;


        }
 

        public ActionResult Index()
        {
            if (Session["IsLoggedIn"] == null)
            {
                Session["IsLoggedIn"] = "false";
            }
            
            MySQLDatabase sql = new MySQLDatabase();
            sql.ConnectToServer();
            if (Request.QueryString["month"] != null)
            {
                if (Request.QueryString["month"].ToString() == "-1")
                {
                    Globals.month = Globals.month - 1;
                }
                if (Request.QueryString["month"].ToString() == "1")
                {
                    Globals.month = Globals.month + 1;
                }
            }
            else
            {
                int month2 = Globals.date.Month;
                Globals.month = month2;
            }
            // GoogleAPI eventGetter = new GoogleAPI();
            MySQLDatabase eventGetter = new MySQLDatabase();

            Events events = new Events();
            events = eventGetter.GetEventsByMonth(Globals.month);
            ViewBag.currentMonth = Globals.month;
            ViewBag.events = events;
            return View();
        }

        public ActionResult EventDetails()
        {
            if (Request.QueryString["eventID"] != null)
            {
                HttpCookie myCookie = new HttpCookie("EventId");
                myCookie.Value = Request.QueryString["eventID"].ToString();
                myCookie.Expires = DateTime.Now.AddDays(1d);
                Response.Cookies.Add(myCookie);
                //Redirect to a new view, so we don't have an ugly event id in the url, and I am sure there are seceraty risks too.
                //Also the selected event is now saved as a cookie.
                return RedirectToAction("EventDetail", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            //Will never hit this.    
            return View();
        }

        public ActionResult AddEvent()
        {
            if(Session["IsLoggedIn"].ToString() == "false")
            {
                Response.Redirect("~/Home/Index");
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddEvent(string startDate, string endDate, string name, string details, string game)
        {
            //  GoogleAPI eventCreater = new GoogleAPI();
            MySQLDatabase eventGetter = new MySQLDatabase();

            DateTime start = Convert.ToDateTime(startDate);
            DateTime end = Convert.ToDateTime(endDate);

            bool results = eventGetter.CreateEvent(start, end, name, details, game);
            if (results)
            {
                ViewBag.results = "Event Added";
            }
            else
            {
                ViewBag.results = "Something went wrong.";
            }
            return View();
        }

        public ActionResult EventDetail()
        {
            
            Event e = new Event();
            if (Request.Cookies["EventId"] != null)
            {
                string eventId;
                { eventId = Request.Cookies["EventId"].Value.ToString(); }
                // GoogleAPI eventGetter = new GoogleAPI();
                MySQLDatabase eventGetter = new MySQLDatabase();

                e = eventGetter.GetEventById(eventId);
            }
            ViewBag.e = e;
            return View();
        }

        public ActionResult EventEdit()
        {
            if (Session["IsLoggedIn"].ToString() == "false")
            {
                Response.Redirect("~/Home/Index");
            }
            Event e = new Event();
            if (Request.Cookies["EventId"] != null)
            {
                string eventId;

                { eventId = Request.Cookies["EventId"].Value.ToString(); }
                // GoogleAPI eventGetter = new GoogleAPI();
                MySQLDatabase eventGetter = new MySQLDatabase();

                e = eventGetter.GetEventById(eventId);
            }
            ViewBag.e = e;

            return View();
        }
        [HttpPost]
        public ActionResult EventEdit(string startDate, string endDate, string name, string details, string game)
        {
            // GoogleAPI eventEditor = new GoogleAPI();
            MySQLDatabase eventEditor = new MySQLDatabase();

            DateTime start = Convert.ToDateTime(startDate);
            DateTime end = Convert.ToDateTime(endDate);
            string eventId;
            if (Request.Cookies["EventId"] != null)
            {

                { eventId = Request.Cookies["EventId"].Value.ToString(); }
                bool results = eventEditor.EditEvent(start, end, name, details, eventId,game);


                if (results)
                {
                    ViewBag.results = "Event Edited";

                }
                else
                {
                    ViewBag.results = "Something went wrong.";
                }
                Event e = new Event();
                if (Request.Cookies["EventId"] != null)
                {
                    ///   GoogleAPI eventGetter = new GoogleAPI();

                    e = eventEditor.GetEventById(eventId);
                    ViewBag.e = e;
                }

            }
            else
            {
                ViewBag.results = "Something went wrong.";
            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string UserName, string Password)
        {
            if(UserName == "GoGoMic" & Password == "sixtyNine")
            {
                Session["IsLoggedIn"] = "true";
            }
            Response.Redirect("~/home/index");
            return View();
        }
    }
}