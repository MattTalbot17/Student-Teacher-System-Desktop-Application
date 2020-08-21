using EncyrptionDLL;
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
    /// A form for the Lecturer to update their password
    /// </summary>
    public partial class updateLProfile : Window
    {
        string lectureID, password, sqlInput;

        Class1 EncryptionDll = new Class1();

        SqlConnection cnn;
        SqlCommand command;
        SqlDataReader dataReader;
        public updateLProfile(string LectureID)
        {
            InitializeComponent();
            lectureID = LectureID;
            UpdateLProfile.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            LectureMenu lectureMenu = new LectureMenu(lectureID);
            lectureMenu.Show();
            this.Hide();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //check to see if it matches students password in database
            if (EncryptionDll.EncryptData(currentPTextBox.Text, "ffhhgfgh").Equals(QueryDatabaseStudents()))
            {
                if (newPTextBox.Text != " ")
                {
                    //method that updates the password in the database
                    sqlInput = "update Lecturer set L_Password = '" + EncryptionDll.EncryptData(newPTextBox.Text, "ffhhgfgh") + "' where LecturerID = '" + lectureID + "';";
                    cnn = new SqlConnection(Properties.Settings.Default.con1);
                    cnn.Open();

                    SqlCommand command;

                    command = new SqlCommand(sqlInput, cnn);

                    command.ExecuteNonQuery();

                    cnn.Close();
                    MessageBox.Show("Updates Made");
                }
                else
                    MessageBox.Show("Please Provide a valid password");
            }
            else
                MessageBox.Show("Invalid Password. Please Try Again");
        }
        public string QueryDatabaseStudents()
        {
            //method that pulls in the data from my tables in the database
            sqlInput = "select L_Password from Lecturer where LecturerID = '" + lectureID + "';";
            cnn = new SqlConnection(Properties.Settings.Default.con1);
            cnn.Open();

            command = new SqlCommand(sqlInput, cnn);

            dataReader = command.ExecuteReader();

            int count = 0;
            //goes through the table and retrieves each record 
            while (dataReader.Read())
            {
                try
                {
                    password = Convert.ToString(dataReader.GetValue(0));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
                count++;
            }
            cnn.Close();
            return password;
        }
    }
}
