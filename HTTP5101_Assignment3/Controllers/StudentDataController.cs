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


    public class StudentDataController : ApiController
    {


        // Student class allows to access MySQL Database. 
        private SchoolDbContext School = new SchoolDbContext();

        [HttpGet]
        public IEnumerable<Student> ListStudents()
        {

            // Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open the connection between the web server and database
            Conn.Open();

            // Establish a new command for database
            MySqlCommand cmd = Conn.CreateCommand();

            // Query
            cmd.CommandText = "select * from students";

            // Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Create an empty list of Students 
            List<Student> Students = new List<Student> { };

            // Loop through each row of ResultSet (from database)
            while (ResultSet.Read())
            {

                uint StudentId = (uint)ResultSet["studentid"];
                string StudentFname = (string)ResultSet["studentfname"];
                string StudentLname = (string)ResultSet["studentlname"];
                string StudentNumber = (string)ResultSet["studentnumber"];
                DateTime EnrolDate = (DateTime)ResultSet["enroldate"];



                Student NewStudent = new Student();
                NewStudent.StudentId = StudentId;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.EnrolDate = EnrolDate;


                Students.Add(NewStudent);

            }
            Conn.Close();

            return Students;
        }

        public Student FindStudent(int id)
        {
            Student NewStudent = new Student();

            // Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open the connection between the web server and database
            Conn.Open();

            // Establish a new command for database
            MySqlCommand cmd = Conn.CreateCommand();

            // Query
            cmd.CommandText = "select * from students where studentid = " + id;

            // Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();
            
            // Loop through each row of ResultSet (from database)
            while (ResultSet.Read())
            {

                uint StudentId = (uint)ResultSet["studentid"];
                string StudentFname = (string)ResultSet["studentfname"];
                string StudentLname = (string)ResultSet["studentlname"];
                string StudentNumber = (string)ResultSet["studentnumber"];
                DateTime EnrolDate = (DateTime)ResultSet["enroldate"];

                NewStudent.StudentId = StudentId;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.EnrolDate = EnrolDate;

            }

            return NewStudent;
        }
    }
    }
    
