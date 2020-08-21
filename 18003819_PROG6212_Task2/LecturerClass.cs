
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18003819_PROG6212_Task2
{
    class LecturerClass
    {
        /// <summary>
        /// Object to store lecture details
        /// </summary>
        private string lastName;
        private string firstName;
        private string password;
        private string lectureID;
        string testName;

        public LecturerClass()
        {
        }
        public LecturerClass(string lectureID, string testName)
        {
            this.lectureID = lectureID;
            this.testName = testName;
        }
        public LecturerClass(string lectureID, string firstName, string lastName, string password)
        {
            this.lastName = lastName;
            this.lectureID = lectureID;
            this.firstName = firstName;
            this.password = password;
        }
        public string LectureID { get => lectureID; set => lectureID = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Password { get => password; set => password = value; }
        public string TestName { get => testName; set => testName = value; }
    }
}

