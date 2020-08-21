using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18003819_PROG6212_Task2
{
    class StudentClass
    {
        private string lastName;
        private string studentID;
        private string firstName;
        private int age;
        private string password;
        private double mark;
        private string test;


        public string StudentID { get => studentID; set => studentID = value; }
        public int Age { get => age; set => age = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Password { get => password; set => password = value; }
        public double Mark { get => mark; set => mark = value; }
        public string Test { get => test; set => test = value; }

        public StudentClass()
        {

        }

        public StudentClass(string studentID, int age, string firstName, string lastName, string password)
        {
            this.lastName = lastName;
            this.age = age;
            this.studentID = studentID;
            this.firstName = firstName;
            this.password = password;
        }

        public StudentClass(string lastName, string studentID, string firstName, int age, string password, double mark, string test)
        {
            this.lastName = lastName;
            this.studentID = studentID;
            this.firstName = firstName;
            this.age = age;
            this.password = password;
            this.mark = mark;
            this.test = test;
        }
        public StudentClass(double mark, string test)
        {
            this.mark = mark;
            this.test = test;
        }
        public StudentClass(string studentID, double mark, string test)
        {
            this.mark = mark;
            this.test = test;
            this.studentID = studentID;
        }
    }
}
