using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HTTP5101_Assignment3.Models;
using MySql.Data.MySqlClient;

namespace HTTP5101_Assignment3.Controllers
{
    public class ClassDataController : ApiController
    {
        // Class class allows to access MySQL Database. 
        private SchoolDbContext School = new SchoolDbContext();


        //This Controller Will access the Class table.
        /// <summary>
        /// Returns a list of Classes
        /// </summary>
        /// <example>GET api/ClassData/ListClasses</example>
        /// <returns>
        /// A list of classes (class id, class code, class name)
        /// </returns>
        [HttpGet]
        public IEnumerable<Class> ListClasses()
        {
            // Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open the connection between the web server and database
            Conn.Open();

            // Establish a new command for database
            MySqlCommand cmd = Conn.CreateCommand();

            // Query
            cmd.CommandText = "select * from classes";

            // Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Create an empty list of Classes 
            List<Class> Classes = new List<Class> { };

            // Loop through each row of ResultSet (from database)
            while (ResultSet.Read())
            {

                int ClassId = (int)ResultSet["classid"];
                string ClassCode = (string)ResultSet["classcode"];
                string ClassName = (string)ResultSet["classname"];
                
                Class NewClass = new Class();
                
                NewClass.ClassId = ClassId;
                NewClass.ClassCode = ClassCode;
                NewClass.ClassName = ClassName;

                Classes.Add(NewClass);

            }
            Conn.Close();

            return Classes;
        }


        //This Controller Will access the Class and Teacher table.
        /// <summary>
        /// Returns class data which class id is matching with parameter and teacher info who teaches that class
        /// </summary>
        /// <example>GET api/ClassData/ListClasses/{id}</example>
        /// <returns> 
        /// class data (class id, class code, class name, teacher id) and Teacher's information (teacher first name, last name, ) 
        /// </returns>
        [HttpGet]
        public Class FindClass(int id)
        {
            Class NewClass = new Class();

            // Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open the connection between the web server and database
            Conn.Open();

            // Establish a new command for database
            MySqlCommand cmd = Conn.CreateCommand();

            // Query
            cmd.CommandText = "select * from classes " +
                "left outer join teachers on classes.teacherid = teachers.teacherid " +
                "where classes.classid = " + id;

            // Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Loop through each row of ResultSet (from database)
            while (ResultSet.Read())
            {

                int ClassId = (int)ResultSet["classid"];
                string ClassCode = (string)ResultSet["classcode"];
                long TeacherId = (long)ResultSet["teacherId"];
                string ClassName = (string)ResultSet["classname"];
                DateTime StartDate = (DateTime)ResultSet["startdate"];
                DateTime FinishDate = (DateTime)ResultSet["finishdate"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherLname"];


                NewClass.ClassId = ClassId;
                NewClass.ClassCode = ClassCode;
                NewClass.TeacherId = TeacherId;
                NewClass.ClassName = ClassName;
                NewClass.StartDate = StartDate;
                NewClass.FinishDate = FinishDate;
                NewClass.TeacherFname = TeacherFname;
                NewClass.TeacherLname = TeacherLname;

            }

            Conn.Close();
            return NewClass;
        }

    }

 }