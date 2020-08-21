using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _18003819_PROG6212_Task2
{
    /// <summary>
    /// Interaction logic for LecturerViewMarks.xaml
    /// </summary>
    public partial class LecturerViewMarks : Window
    {
        /// <summary>
        /// form to display the students marks for each test they've taken
        /// </summary>
       
        string lecturerID, testName, studentNumber;
        List<StudentClass> students = new List<StudentClass>();
        List<TestClass> testClassList = new List<TestClass>();
        List<string> studentAnswers = new List<string>();

        //Declarations to connect to SQL Server
        SqlCommand command;
        SqlDataAdapter adapter = new SqlDataAdapter();
        SqlDataReader dataReader;
        SqlConnection cnn;
        public LecturerViewMarks(string LecturerID)
        {
            InitializeComponent();
            lecturerID = LecturerID;
            lecturerViewMarks.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            LectureMenu LectureMenu = new LectureMenu(lecturerID);
            LectureMenu.Show();
            this.Hide();
        }
        private void queryDatabaseStudentResults()
        {
            //method that pulls in the data from my tables in the database
            string sqlInput = "Select S.StudentNumber,ST.TestName, ST.TestMark from Student S join Student_Test ST on S.StudentNumber = ST.StudentNumber join Test on Test.TestName = ST.TestName where Test.LecturerID = '" + lecturerID + "';";
            cnn = new SqlConnection(Properties.Settings.Default.con1);
            cnn.Open();
            command = new SqlCommand(sqlInput, cnn);
            dataReader = command.ExecuteReader();

            int count = 0;
            students.Clear();

            //goes through the table and retrieves each record 
            while (dataReader.Read())
            {
                try
                {
                    students.Add(new StudentClass { StudentID = Convert.ToString(dataReader.GetValue(0)), Test = Convert.ToString(dataReader.GetValue(1)), Mark = Convert.ToDouble(dataReader.GetValue(2)) });
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
                count++;

            }
            cnn.Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            queryDatabaseStudentResults();
            string test = "";
            for (int i = 0; i < students.Count; i++)
            {
                test = students[i].Test;
                if (test.Length <= 8)
                    test = test + "\t";
                resultsListBox.Items.Add(students[i].StudentID + "\t\t" + test + "\t\t" + students[i].Mark);
            }
        }
        private void ResultsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //method to allow the lecturer to click on that specific student and view their answers for the test they've taken

            string selectItem = resultsListBox.SelectedItem.ToString();

            string[] Testpiece = new string[100];
            Testpiece = selectItem.Split('\t');
            testName = Testpiece[2];

            var firstSpaceIndex = selectItem.IndexOf("\t");
           studentNumber = selectItem.Substring(0, firstSpaceIndex);


            string result = "";
            QueryDatabaseQuestion_AnswerTable();
            QueryDatabaseStudentAnswerTable();

            for (int i = 0; i < testClassList.Count; i++)
            {
                result += "Question " + (i + 1).ToString() + ":\t" + testClassList[i].Question +
                    "\nPossible Answers:\t" + testClassList[i].Answer1 +
                                   "\n\t\t" + testClassList[i].Answer2 +
                                   "\n\t\t" + testClassList[i].Answer3 +
                                   "\n\t\t" + testClassList[i].Answer4 +
                    "\nCorrect Answer: " + testClassList[i].CorrectAnswer +
                    "\nYour Answer: " + studentAnswers[i] + "\n\n";

            }
            MessageBox.Show(result, "Memo");
        }
        private void QueryDatabaseQuestion_AnswerTable()
        {
            //method that pulls in the data from my tables in the database
            string sqlInput = "select QuestionID, Question, Answer1, Answer2, Answer3, Answer4, CorrectAnswer from Question_Answer join Test on Test.TestID = Question_Answer.TestID where TestName = '" + testName + "'; ";

            SqlConnection cnn;
            cnn = new SqlConnection(Properties.Settings.Default.con1);
            cnn.Open();

            SqlCommand command;
            SqlDataReader dataReader;

            command = new SqlCommand(sqlInput, cnn);

            dataReader = command.ExecuteReader();

            int count = 0;

            //goes through the table and retrieves each record 
            testClassList.Clear();
            while (dataReader.Read())
            {
                try
                {
                    testClassList.Add(new TestClass
                    {
                        QuestionID = Convert.ToString(dataReader.GetValue(0)),
                        Question = Convert.ToString(dataReader.GetValue(1)),
                        Answer1 = Convert.ToString(dataReader.GetValue(2)),
                        Answer2 = Convert.ToString(dataReader.GetValue(3)),
                        Answer3 = Convert.ToString(dataReader.GetValue(4)),
                        Answer4 = Convert.ToString(dataReader.GetValue(5)),
                        CorrectAnswer = Convert.ToString(dataReader.GetValue(6))
                    });
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
                count++;

            }
            cnn.Close();
        }
        private void QueryDatabaseStudentAnswerTable()
        {
            //method that pulls in the data from my tables in the database
            string sqlInput = "select Answer from StudentAnswer join Question_Answer QA on QA.QuestionID = StudentAnswer.QuestionID join Test on QA.TestID = Test.TestID join Student on Student.StudentNumber = StudentAnswer.StudentNumber where Student.StudentNumber = '" + studentNumber + "'AND Test.TestName = '" + testName + "'; ";
            SqlConnection cnn;
            cnn = new SqlConnection(Properties.Settings.Default.con1);
            cnn.Open();

            SqlCommand command;
            SqlDataReader dataReader;

            command = new SqlCommand(sqlInput, cnn);

            dataReader = command.ExecuteReader();

            int count = 0;
            studentAnswers.Clear();
            //goes through the table and retrieves each record 
            while (dataReader.Read())
            {
                try
                {
                    studentAnswers.Add(Convert.ToString(dataReader.GetValue(0)));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
                count++;

            }
            cnn.Close();
        }
    } 
}
