using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;

namespace MikesCalendarApp2.Models
{
    public class GoogleAPI
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/calendar-dotnet-quickstart.json
        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "Google Calendar API .NET Quickstart";

        public Events GetEvents(int month)
        {
            UserCredential credential;

            using (var stream =
                new FileStream(System.Web.Hosting.HostingEnvironment.MapPath("~\\client_secret_New.json"), FileMode.Open, FileAccess.Read))
            {
                string credPath = "";
                credPath = Path.Combine(credPath, System.Web.Hosting.HostingEnvironment.MapPath("~/.credentials/calendar-dotnet-quickstart.json"));

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List("primary");
            DateTime date = new DateTime();
            date = DateTime.Now;
            DateTime monthDate = new DateTime(date.Year, month, 1);
            request.TimeMin = monthDate;
            DateTime endOfMonth = new DateTime(date.Year,
                                   month,
                                   DateTime.DaysInMonth(date.Year,
                                                        month));
            request.TimeMax = endOfMonth;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 31;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
            
            // List events.
            Events events = request.Execute();
            return events;
        }
        public Event GetEventById(string id)
        {
            UserCredential credential;

            using (var stream =
                new FileStream(System.Web.Hosting.HostingEnvironment.MapPath("~\\client_secret_New.json"), FileMode.Open, FileAccess.Read))
            {
                string credPath = "";
                credPath = Path.Combine(credPath, System.Web.Hosting.HostingEnvironment.MapPath("~/.credentials/calendar-dotnet-quickstart.json"));

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            EventsResource.GetRequest request = service.Events.Get("primary",id);
            DateTime date = new DateTime();
            date = DateTime.Now;
         
            // List events.
            Event events = request.Execute();
            return events;
        }

        public bool CreateEvent(DateTime start, DateTime end, string name,string details)
        {
            try
            {
                Event e = new Event();
                EventDateTime startDate = new EventDateTime();
                EventDateTime endDate = new EventDateTime();
                startDate.DateTime = start;
                endDate.DateTime = end;

                e.Summary = name;
                e.Start = startDate;
                e.End = endDate;
                e.Description = details;

                UserCredential credential;

                using (var stream =
                    new FileStream(System.Web.Hosting.HostingEnvironment.MapPath("~\\client_secret_New.json"), FileMode.Open, FileAccess.Read))
                {
                    string credPath = "";
                    credPath = Path.Combine(credPath, System.Web.Hosting.HostingEnvironment.MapPath("~/.credentials/calendar-dotnet-quickstart.json"));

                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                    Console.WriteLine("Credential file saved to: " + credPath);
                }

                // Create Google Calendar API service.
                var service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                // Define parameters of request.
                EventsResource.InsertRequest request = service.Events.Insert(e, "primary");
                request.Execute();


                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public bool EditEvent(DateTime start, DateTime end, string name, string details,string id)
        {
            try
            {
                Event e = new Event();
                EventDateTime startDate = new EventDateTime();
                EventDateTime endDate = new EventDateTime();
                startDate.DateTime = start;
                endDate.DateTime = end;

                e.Summary = name;da
                e.Start = startDate;
                e.End = endDate;
                e.Description = details;

                UserCredential credential;

                using (var stream =
                    new FileStream(System.Web.Hosting.HostingEnvironment.MapPath("~\\client_secret_New.json"), FileMode.Open, FileAccess.Read))
                {
                    string credPath = "";
                    credPath = Path.Combine(credPath, System.Web.Hosting.HostingEnvironment.MapPath("~/.credentials/calendar-dotnet-quickstart.json"));

                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                    Console.WriteLine("Credential file saved to: " + credPath);
                }

                // Create Google Calendar API service.
                var service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                // Define parameters of request.
                EventsResource.UpdateRequest request = service.Events.Update(e, "primary",id);
                request.Execute();


                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}