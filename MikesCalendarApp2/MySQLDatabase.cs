using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MikesCalendarApp2.Models;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace MikesCalendarApp2
{
    public class MySQLDatabase
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        private string command;

        public bool ConnectToServer()
        {
            server = "NickCage.org";
            database = "nintendude";
            uid = "admin";
            password = "soccer12";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";Convert Zero Datetime=True";

            connection = new MySqlConnection(connectionString);

            
            connection.Open();
            return true;
        }
       
        public Events GetAllEvents()
        {
            Events events = new Events();

            return events;
        }
        public Events GetEventsByMonth(int month)
        {
            Events events = new Events();
            ConnectToServer();

            string query = "SELECT * FROM calendarEvents WHERE month = '" + month + "';";

            MySqlCommand sqlcmd = new MySqlCommand(query, connection);

            MySqlDataReader reader = sqlcmd.ExecuteReader();

            while (reader.Read())
            {
                events.AddEvent(int.Parse(reader["EventId"].ToString()), Convert.ToDateTime(reader["startDate"].ToString()), Convert.ToDateTime(reader["endDate"].ToString()), reader["description"].ToString(), reader["streamGame"].ToString(), reader["eventName"].ToString());
            }

            DisconnectFromServer();


            return events;
        }

        private void DisconnectFromServer()
        {
            connection.Close();
        }

        internal Event GetEventById(string eventId)
        {
            Event e = new Event();

            ConnectToServer();

            string query = "SELECT * FROM calendarEvents WHERE eventId = '" + eventId + "';";

            MySqlCommand sqlcmd = new MySqlCommand(query, connection);

            MySqlDataReader reader = sqlcmd.ExecuteReader();

            while (reader.Read())
            {
                e = new Event(int.Parse(reader["EventId"].ToString()), DateTime.Parse(reader["startDate"].ToString()), DateTime.Parse(reader["endDate"].ToString()), reader["description"].ToString(), reader["streamGame"].ToString(), reader["eventName"].ToString());
            }

            DisconnectFromServer();

            return e;
        }

        internal bool EditEvent(DateTime start, DateTime end, string name, string details, string eventId, string game)
        {
            ConnectToServer();

            string query = "UPDATE  `nintendude`.`calendarevents` SET  `eventName` =  '"+ name + "',`description` = '"+ details + "',`startDate` = '"+ start + "',`endDate` = '" + end + "',`month` = '" + start.Month + "',`streamGame` = '" + game + "' WHERE  `calendarevents`.`EventId` = "+eventId+";";

            MySqlCommand sqlcmd = new MySqlCommand(query, connection);

            MySqlDataReader reader = sqlcmd.ExecuteReader();

            return true;
        }

        internal bool CreateEvent(DateTime start, DateTime end, string name, string details,string game)
        {
            Event e = new Event();

            ConnectToServer();

            string query = "INSERT INTO  `calendarevents` (  `eventName` ,  `description` ,  `startDate` ,  `endDate` ,  `month` ,  `streamGame`) VALUES('" + name + "','" + details + "','" + start.ToString() + "','" + end.ToString() + "',"+ start.Month + ",'" + game + "')";

            MySqlCommand sqlcmd = new MySqlCommand(query, connection);

            MySqlDataReader reader = sqlcmd.ExecuteReader();

            return true;
        }
    }
}