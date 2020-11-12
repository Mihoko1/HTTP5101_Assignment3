using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace HTTP5101_Assignment3.Models
{
    public class SchoolDbContext
    {
        private static string User { get { return "humber_student"; } }
        private static string Password { get { return "humberisgreat"; } }
        private static string Database { get { return "humber_school"; } }
        private static string Server { get { return "bittsdevelopment.com"; } }
        private static string Port { get { return "3306"; } }


        //private static string User { get { return "root"; } }
        //private static string Password { get { return "root"; } }
        //private static string Database { get { return "schooldb"; } }
        //private static string Server { get { return "localhost"; } }
        //private static string Port { get { return "3306"; } }

        protected static string ConnectionString
        {
            get
            {
                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password;
            }
        }

        public MySqlConnection AccessDatabase()
        {
            //instantiating the MySqlConnaction Class to create an object and connect to database//
            return new MySqlConnection(ConnectionString);
        }
    }
}