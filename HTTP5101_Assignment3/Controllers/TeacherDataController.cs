using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HTTP5101_Assignment3.Models;
using MySql.Data.MySqlClient;
using WebGrease.Css.Ast.Selectors;

namespace HTTP5101_Assignment3.Controllers
{
    public class TeacherDataController : ApiController
    {
        // Teacher class allows to access MySQL Database. 
        private SchoolDbContext School = new SchoolDbContext(); 
        
        [HttpGet]
        public IEnumerable<Teacher> ListTeachers() 
        { 
            // Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open the connection between the web server and database
            Conn.Open();

            // Establish a new command for database
            MySqlCommand cmd = Conn.CreateCommand();

            // Query
            cmd.CommandText = "select * from teachers"; 
                

            // Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Create an empty list of Teachers 
            List<Teacher> Teachers = new List<Teacher> {};

            // Loop through each row of ResultSet (from database)
            while (ResultSet.Read())
            {


                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
               
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                
                Teachers.Add(NewTeacher);
   
            }
            Conn.Close();

            return Teachers;
        }

        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            // Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open the connection between the web server and database
            Conn.Open();

            // Establish a new command for database
            MySqlCommand cmd = Conn.CreateCommand();

            // Query
            cmd.CommandText = "select * from teachers " +
                "left outer join classes on teachers.teacherid = classes.teacherid " +
                "where teachers.teacherid = " + id;

            // Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Loop through each row of ResultSet (from database)
            while (ResultSet.Read())
            {

                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];
                string ClassCode = "";
                if (ResultSet["classcode"] == DBNull.Value)
                {
                    ClassCode = "N/A";
                }
                else
                {
                    ClassCode = (string)ResultSet["classcode"];
                }
                 
                string ClassName = "";
                if (ResultSet["className"] == DBNull.Value)
                {
                    ClassName = "N/A";
                }
                else
                {
                    ClassName = (string)ResultSet["className"];
                }

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;
                NewTeacher.ClassCode = ClassCode;
                NewTeacher.ClassName = ClassName;

            }

            return NewTeacher;
        }

    }
}
