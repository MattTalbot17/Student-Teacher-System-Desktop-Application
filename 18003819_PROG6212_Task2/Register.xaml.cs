using EncyrptionDLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Form to allow a student/lecturer to register into the system and enter in their details to be stored in the database
    /// </summary>
    public partial class Register : Window
    {
        UserBindingClass ubc = new UserBindingClass();

        //declaration of DLL
        //DLL encrypts the password
        Class1 EncryptionDll = new Class1();

        StudentClass[] studentClassArray = new StudentClass[1000];
        LecturerClass[] lectureClassArray = new LecturerClass[1000];
        MainWindow LogIn = new MainWindow();
        SqlCommand command;
        SqlDataAdapter adapter = new SqlDataAdapter();
        String sql = "";
        SqlConnection cnn;
        public Register()
        {
            InitializeComponent();
            register.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.DataContext = ubc;
            //Register.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            fNameLabel.Visibility = Visibility.Hidden;
            lNameLabel.Visibility = Visibility.Hidden;
            numberLabel.Visibility = Visibility.Hidden;
            passwordLabel.Visibility = Visibility.Hidden;
            firstNameTextBox.Visibility = Visibility.Hidden;
            lastNameTextBox.Visibility = Visibility.Hidden;
            numberTextBox.Visibility = Visibility.Hidden;
            PasswordTextBox.Visibility = Visibility.Hidden;
            backButton.Visibility = Visibility.Hidden;
            confrimButton.Visibility = Visibility.Hidden;
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            //takes you back to the Log in page
            LogIn.Show();
            this.Hide();
        }
        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            //button users press to confirm their registration
            if (StudentRadioButton.IsChecked == true)
            {
                if (numberTextBox.Text != "" || firstNameTextBox.Text != "" || lastNameTextBox.Text != "" || PasswordTextBox.Text != "" || ageTextBox.Text != "")
                {
                    StudentClass studentClass = new StudentClass();
                    try
                    {
                        cnn = new SqlConnection(Properties.Settings.Default.con1);
                        cnn.Open();

                        sql = "Insert into Student(StudentNumber,Age, Firstname, LastName, S_Password) values ('" + ubc.id + "', '" + ubc.age + "', '" + ubc.firstName + "','" + ubc.surname + "','" + EncryptionDll.EncryptData(ubc.password, "ffhhgfgh") + "');";


                        command = new SqlCommand(sql, cnn);

                        adapter.InsertCommand = new SqlCommand(sql, cnn);
                        adapter.InsertCommand.ExecuteNonQuery();

                        command.Dispose();
                        cnn.Close();
                        MessageBox.Show("Student has been registered!");
                        this.Hide();
                        LogIn.Show();

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Unable to Add User");
                    }

                }
                else
                    MessageBox.Show("Missing Field Entry", "Registration Error", MessageBoxButton.OK);
            }

            else if (LecturerRadioButton.IsChecked == true)
            {
                if (numberTextBox.Text != "" || firstNameTextBox.Text != "" || lastNameTextBox.Text != "" || PasswordTextBox.Text != "")
                {
                    LecturerClass LectureClass = new LecturerClass();
                    try
                    {
                        cnn = new SqlConnection(Properties.Settings.Default.con1);
                        cnn.Open();

                        sql = "Insert into Lecturer(LecturerID,Firstname, LastName, L_Password) values ('" + ubc.id + "', '" + ubc.id + "','" + ubc.surname + "','" + EncryptionDll.EncryptData(ubc.password, "ffhhgfgh") + "');";


                        command = new SqlCommand(sql, cnn);

                        adapter.InsertCommand = new SqlCommand(sql, cnn);
                        adapter.InsertCommand.ExecuteNonQuery();

                        command.Dispose();
                        cnn.Close();
                        MessageBox.Show("Lecturer has been registered!");
                        this.Hide();
                        LogIn.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }
                else
                    MessageBox.Show("Missing Field Entry", "Registration Error", MessageBoxButton.OK);
            }
        }
        private void StudentRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            //if the Student Radio Button is selected these labels and textboxes are displayed
            fNameLabel.Visibility = Visibility.Visible;
            lNameLabel.Visibility = Visibility.Visible;
            numberLabel.Visibility = Visibility.Visible;
            passwordLabel.Visibility = Visibility.Visible;
            firstNameTextBox.Visibility = Visibility.Visible;
            lastNameTextBox.Visibility = Visibility.Visible;
            numberTextBox.Visibility = Visibility.Visible;
            PasswordTextBox.Visibility = Visibility.Visible;
            backButton.Visibility = Visibility.Visible;
            confrimButton.Visibility = Visibility.Visible;
            ageLabel.Visibility = Visibility.Visible;
            ageTextBox.Visibility = Visibility.Visible;
            backButton.Margin = new Thickness(181, 491, 0, 0);
            confrimButton.Margin = new Thickness(280, 491, 0, 0);
            numberLabel.Content = "Please Enter your Student Number";
            Height = 600;
        }
        private void LecturerRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            //if the Lecturer Radio Button is selected these labels and textboxes are displayed
            fNameLabel.Visibility = Visibility.Visible;
            lNameLabel.Visibility = Visibility.Visible;
            numberLabel.Visibility = Visibility.Visible;
            passwordLabel.Visibility = Visibility.Visible;
            firstNameTextBox.Visibility = Visibility.Visible;
            lastNameTextBox.Visibility = Visibility.Visible;
            numberTextBox.Visibility = Visibility.Visible;
            PasswordTextBox.Visibility = Visibility.Visible;
            backButton.Visibility = Visibility.Visible;
            confrimButton.Visibility = Visibility.Visible;
            ageLabel.Visibility = Visibility.Hidden;
            ageTextBox.Visibility = Visibility.Hidden;
            backButton.Margin = new Thickness(181, 435, 0, 0);
            confrimButton.Margin = new Thickness(280, 435, 0, 0);
            numberLabel.Content = "Please enter your Lecturer ID";
            Height = 600;
        }
    }

    //object class created for Binding
    public class UserBindingClass
    {
        private string firstNameValue, surnameValue, idValue, passwordValue;
        public int ageValue;

        public string firstName
        {
            get { return firstNameValue; }
            set { firstNameValue = value; }
        }

        public string surname
        {
            get { return surnameValue; }
            set { surnameValue = value; }
        }
        public string id
        {
            get { return idValue; }
            set { idValue = value; }
        }
        public string password
        {
            get { return passwordValue; }
            set { passwordValue = value; }
        }
        public int age
        {
            get { return ageValue; }
            set { if (value != ageValue) { ageValue = value; } }
        }
    }
}
