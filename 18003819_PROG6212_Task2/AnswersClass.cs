using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18003819_PROG6212_Task2
{
    class AnswersClass
    {
        //Object to store students answers, the objects from the database and the result
        string studentsAnswer, correctAnswer, result;

        //loaded constructor
        public AnswersClass(string studentsAnswer, string correctAnswer, string result)
        {
            this.studentsAnswer = studentsAnswer;
            this.correctAnswer = correctAnswer;
            this.result = result;
        }
        //list of gets and sets for object
        public string StudentsAnswer { get => studentsAnswer; set => studentsAnswer = value; }
        public string CorrectAnswer { get => correctAnswer; set => correctAnswer = value; }
        public string Result { get => result; set => result = value; }
    }
}
