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
    /// a form for the student to update their password
    /// </summary>
    public partial class UpdateProfile : Window
    {
        string studentNumber, password, sqlInput;

        Class1 EncryptionDll = new Class1();

        SqlConnection cnn;
        SqlCommand command;
        SqlDataReader dataReader;

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            StudentMenu st = new StudentMenu(studentNumber);
            st.Show();
            this.Hide();
        }
        public UpdateProfile(string StudentNumber)
        {
            InitializeComponent();
            studentNumber = StudentNumber;
            updateProfile.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //check to see if it matches students password in database
            if (EncryptionDll.EncryptData(currentPTextBox.Text, "ffhhgfgh").Equals(QueryDatabaseStudents()))
            {
                if (newPTextBox.Text != " ")
                {
                    //method that updates the password in the database
                    sqlInput = "update Student set S_Password = '" + EncryptionDll.EncryptData(newPTextBox.Text, "ffhhgfgh") + "' where StudentNumber = '" + studentNumber + "';";
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
            sqlInput = "select S_Password from Student where StudentNumber = '" + studentNumber + "';";
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
