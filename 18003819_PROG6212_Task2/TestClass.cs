using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18003819_PROG6212_Task2
{
    class TestClass
    {
        string questionID, question, answer1, answer2, answer3, answer4, correctAnswer;
        string testID, lectureID, testName;

        public TestClass(string testID, string lectureID, string testName)
        {
            this.testID = testID;
            this.lectureID = lectureID;
            this.testName = testName;
        }
        public TestClass()
        {

        }

        public TestClass(string questionID, string question, string answer1, string answer2, string answer3, string answer4, string correctAnswer)
        {
            this.questionID = questionID;
            this.question = question;
            this.answer1 = answer1;
            this.answer2 = answer2;
            this.answer3 = answer3;
            this.answer4 = answer4;
            this.correctAnswer = correctAnswer;
        }

        public string QuestionID { get => questionID; set => questionID = value; }
        public string Question { get => question; set => question = value; }
        public string Answer1 { get => answer1; set => answer1 = value; }
        public string Answer2 { get => answer2; set => answer2 = value; }
        public string Answer3 { get => answer3; set => answer3 = value; }
        public string Answer4 { get => answer4; set => answer4 = value; }
        public string CorrectAnswer { get => correctAnswer; set => correctAnswer = value; }
        public string TestID { get => testID; set => testID = value; }
        public string LectureID { get => lectureID; set => lectureID = value; }
        public string TestName { get => testName; set => testName = value; }
    }
}
