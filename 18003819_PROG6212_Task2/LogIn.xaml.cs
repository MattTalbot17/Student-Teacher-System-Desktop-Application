using EncyrptionDLL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Media;

namespace _18003819_PROG6212_Task2
{
    /// <summary>
    /// Form for Students and Lecturers to Log in or choose to register
    /// A DLL is used to encrypt the password input to try match passwords in database
    /// </summary>
    public partial class MainWindow : Window
    {
        //declaration for DLL
        //DLL decrypts the password from the database 
        Class1 EncryptionDll = new Class1();

        List<StudentClass> studentsList = new List<StudentClass>();
        List<LecturerClass> lecturesList = new List<LecturerClass>();
        String LectureID;
        public MainWindow()
        {
            InitializeComponent();
            logIn.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        private void LogInButton_Click_1(object sender, RoutedEventArgs e)
        {
            //method that loads the databases into lists and compares the users input to that from the database
            bool isCorrect = false;
            QueryDatabaseStudents();
            QueryDatabaseLecturers();
            try
            {
                //loop running through all lectures
                for (int i = 0; i < lecturesList.Count; i++)
                {
                    if (usernameTextBox.Text == lecturesList[i].LectureID && EncryptionDll.EncryptData(passwordTextBox.Text, "ffhhgfgh") == lecturesList[i].Password)
                    {
                        LectureID = lecturesList[i].LectureID;
                        LectureMenu lecturerMenu = new LectureMenu(LectureID);
                        this.Hide();
                        lecturerMenu.Show();
                        isCorrect = true;
                        break;
                    }
                }
                //loop running through all students
                for (int i = 0; i < studentsList.Count; i++)
                {
                    if (usernameTextBox.Text == studentsList[i].StudentID && EncryptionDll.EncryptData(passwordTextBox.Text, "ffhhgfgh") == studentsList[i].Password)
                    {

                        StudentMenu studentMenu = new StudentMenu(studentsList[i].StudentID);
                        studentMenu.Show();
                        this.Hide();
                        isCorrect = true;
                        break;
                    }
                }
            }
            catch (Exception)
            {

            }
            if (isCorrect == false)
            {
                MessageBox.Show("invalid username or password. Try again!", "LogIn Error", MessageBoxButton.OK);
            }
        }
        private void RegisterButton_Click_1(object sender, RoutedEventArgs e)
        {

            Register register = new Register();
            register.Show();
            this.Hide();
        }
        private void QueryDatabaseLecturers()
        {
            //method that pulls in the data from my tables in the database
            string sqlInput = "select * from Lecturer;";
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
                    lecturesList.Add(new LecturerClass
                    {
                        LectureID = Convert.ToString(dataReader.GetValue(0)),
                        FirstName = Convert.ToString(dataReader.GetValue(1)),
                        LastName = Convert.ToString(dataReader.GetValue(2)),
                        Password = Convert.ToString(dataReader.GetValue(3))
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
        private void QueryDatabaseStudents()
        {
            //method that pulls in the data from my tables in the database
            string sqlInput = "select * from Student;";
            SqlConnection cnn;
            cnn = new SqlConnection(Properties.Settings.Default.con1);
            cnn.Open();

            SqlCommand command;
            SqlDataReader dataReader;

            command = new SqlCommand(sqlInput, cnn);

            dataReader = command.ExecuteReader();

            int count = 0;
            studentsList.Clear();
            //goes through the table and retrieves each record 
            while (dataReader.Read())
            {
                try
                {
                    studentsList.Add(new StudentClass
                    {
                        StudentID = Convert.ToString(dataReader.GetValue(0)),
                        Age = Convert.ToInt32(dataReader.GetValue(1)),
                        FirstName = Convert.ToString(dataReader.GetValue(2)),
                        LastName = Convert.ToString(dataReader.GetValue(3)),
                        Password = Convert.ToString(dataReader.GetValue(4))
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
        private void StudentButton_Click(object sender, RoutedEventArgs e)
        {
            usernameTextBox.Text = "18003819";
            passwordTextBox.Text = "a";
        }
        private void LecturerButton_Click(object sender, RoutedEventArgs e)
        {
            usernameTextBox.Text = "LE001";
            passwordTextBox.Text = "b";
        }
        private void UsernameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            //once clicked, username textbox is cleared and font color is set to black
            if (usernameTextBox.Text == "Student Number/Lecture ID")
            {
                usernameTextBox.Clear();
                usernameTextBox.Foreground = Brushes.White;
            }
            //if no value is inside password textbox, the text reads "Passowrd" incase the user forgets
            if (passwordTextBox.Text == "")
            {
                passwordTextBox.Foreground = Brushes.White;
                passwordTextBox.Text = "Password";
            }
        }
        private void PasswordTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            //when clicked the textbox field is cleared and the characters inserted are replaced by "*" 
            passwordTextBox.Text = "";
            //passwordTextBox.PasswordChar = '*';
            passwordTextBox.Foreground = Brushes.White;
            if (usernameTextBox.Text == "")
            {
                usernameTextBox.Foreground = Brushes.White;
                usernameTextBox.Text = "Student Number/Lecture ID";
            }
        }
    }    
}
