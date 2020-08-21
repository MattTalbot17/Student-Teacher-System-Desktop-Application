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
    /// This class Loads the students from the database for the lecturer to view them
    /// depending on the test selected, the students who have taken the test will be displayed along with their marks
    /// </summary>
    public partial class ClassListForm : Window
    {
        //global decalarations for class
        string LectureID, testName;
        StudentClass[] studentsArray = new StudentClass[100];
        List<LecturerClass> lectureList = new List<LecturerClass>();
        List<StudentClass> StudentList = new List<StudentClass>();
        StudentClass sc = new StudentClass();
        public ClassListForm(string lectureID)
        {
            //when the form loads it takes in the Lecturers ID that has logged in
            this.LectureID = lectureID;
            //method below loads all the students that exist in the database
            QueryDatabaseStudents();
            InitializeComponent();
            classListForm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            markLabel.Visibility = Visibility.Collapsed;
            testLabel.Visibility = Visibility.Collapsed;
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            //goes back to the Lectures menu
            LectureMenu lecturerMenu = new LectureMenu(LectureID);
            lecturerMenu.Show();
            this.Hide();
        }
        private void QueryDatabaseStudents()
        {
            //method that pulls in the data from my tables in the database
            //all the current students in the database are loaded into a list of objects and then displayed
            string sqlInput = "select * from Student;";
            SqlConnection cnn;
            cnn = new SqlConnection(Properties.Settings.Default.con1);
            cnn.Open();

            SqlCommand command;
            SqlDataReader dataReader;
            command = new SqlCommand(sqlInput, cnn);
            dataReader = command.ExecuteReader();
            int count = 0;
            //goes through the table and retrieves each record 
            while (dataReader.Read())
            {
                try
                {
                    StudentList.Add(new StudentClass
                    {
                        Age = Convert.ToInt32(dataReader.GetValue(1)),
                        FirstName = Convert.ToString(dataReader.GetValue(2)),
                        LastName = Convert.ToString(dataReader.GetValue(3)),
                        Password = Convert.ToString(dataReader.GetValue(4)),
                        StudentID = Convert.ToString(dataReader.GetValue(0))
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
        private void TestNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //method that displays the students that have taken the test and their mark for that test
            markLabel.Visibility = Visibility.Visible;
            testLabel.Visibility = Visibility.Visible;
            Boolean containsResults = false;
            testName = testNameComboBox.SelectedItem.ToString();
            QueryTablesStudent_TestAndStudentAndTest();
            string name;
            string result = "";
            string test = "";

            //loop to go through students list and adds it to the text box
            foreach (var item in StudentList)
            {
                test = item.Test;
                if (test.Length < 8)
                    test = test + "\t";
                name = item.FirstName + " " + item.LastName;
                if (name.Length < 10)
                    name = item.FirstName + " " + item.LastName + "\t";
                  result = item.StudentID + "\t\t" + name + "\t\t" + item.Age + "\t\t" + test + "\t\t\t" + item.Mark + Environment.NewLine;
                containsResults = true;
            }
            //if no student has taken the test a message is displayed
            if (containsResults == false)
                classListTextBox.Text = "No Student has taken the test";
            classListTextBox.Text += result;
        }
        private void QueryDatabaseTestTable()
        {
            //method that pulls in the Tests from my Test Table in the database
            string sqlInput = "select * from Test where LecturerID = '" + LectureID + "';";
            SqlConnection cnn;
            cnn = new SqlConnection(Properties.Settings.Default.con1);
            cnn.Open();

            SqlCommand command;
            SqlDataReader dataReader;

            command = new SqlCommand(sqlInput, cnn);

            dataReader = command.ExecuteReader();

            int count = 0;

            //goes through the table and retrieves each record 
            while (dataReader.Read())
            {
                try
                {
                    lectureList.Add(new LecturerClass { LectureID = Convert.ToString(dataReader.GetValue(1)), TestName = Convert.ToString(dataReader.GetValue(2)) });
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
            //method that loads the test names into the combo box when the form loads
            QueryDatabaseTestTable();
            string name = "";
            try
            {
                //adding students to Text Box
                foreach (var item in StudentList)
                {
                    name = item.FirstName + " " + item.LastName;
                    if (name.Length < 10)
                        name = item.FirstName + " " + item.LastName + "\t";
                    classListTextBox.Text += item.StudentID + "\t\t" + name + "\t\t" + item.Age + Environment.NewLine;
                }
                //adding Test names to Combo Box
                foreach (var item in lectureList)
                {
                    testNameComboBox.Items.Add(item.TestName.ToString());
                }
            }
            catch (Exception)
            {

            }
        }
        private void QueryTablesStudent_TestAndStudentAndTest()
        {
            //method that pulls in the data from my tables in the database

            //this query loads all the students that have done the test, with their details and their mark for that specific test 
            //based on the combo box selection
            string sqlInput = "Select S.LastName, S.StudentNumber, S.FirstName, S.Age, T.TestName, ST.TestMark From Student_Test ST join Test T on ST.TestName = T.TestName join Student S on ST.StudentNumber = S.StudentNumber join Lecturer L on T.LecturerID = L.LecturerID where T.TestName ='" + testName + "';";
            SqlConnection cnn;
            cnn = new SqlConnection(Properties.Settings.Default.con1);
            cnn.Open();
            SqlCommand command;
            SqlDataReader dataReader;
            command = new SqlCommand(sqlInput, cnn);
            dataReader = command.ExecuteReader();
            int count = 0;
            //goes through the table and retrieves each record 
            classListTextBox.Text = "";
            StudentList.Clear();
            while (dataReader.Read())
            {
                try
                {
                    StudentList.Add(new StudentClass { LastName = Convert.ToString(dataReader.GetValue(0)), StudentID = Convert.ToString(dataReader.GetValue(1)), FirstName = Convert.ToString(dataReader.GetValue(2)), Age = Convert.ToInt32(dataReader.GetValue(3)), Test = Convert.ToString(dataReader.GetValue(4)), Mark = Convert.ToDouble(dataReader.GetValue(5)) });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                count++;
            }
            cnn.Close();
        }
    }
}
