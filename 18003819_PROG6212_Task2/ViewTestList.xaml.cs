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
    /// A form for Lecturers to publish or unpublish a test for the students to take/not take.
    /// The form also contains a button to take the lecturers to a form where they are able to see the students attempts
    /// </summary>
    public partial class ViewTestList : Window
    {
        string sqlInput, lecturerID;
        List<string> testNameList = new List<string>();
        List<bool> publishedBoolList = new List<bool>();

        SqlConnection cnn;

        public ViewTestList(string LecturerID)
        {
            InitializeComponent();
            lecturerID = LecturerID;
            viewTestList.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void ViewTestList_Loaded(object sender, RoutedEventArgs e)
        {
            //for each test created, a combo box is created when the form loads
            //when the lecturer clicks or unclicks a test, an action is performed
            QueryDatabaseTestTable();
            //dynamically creating checkbox's
            for (int i = 0; i < testNameList.Count; i++)
            {
                CheckBox cb = new CheckBox();
                cb.Content = testNameList[i];
                cb.Foreground = Brushes.White;
                panel.Children.Add(cb);
                if (publishedBoolList[i] == true)
                    cb.IsChecked = true;
                else
                    cb.IsChecked = false;                
                //on click method that relates to a certain button (passing through cb, which is the specific checkbox)
                cb.Click += delegate { checkingMethod(cb); };
            }
        }
        private void QueryDatabaseTestTable()
        {
            //method that pulls in the data from my tables in the database
            string sqlInput = "select * from Test;";
            SqlConnection cnn;
            cnn = new SqlConnection(Properties.Settings.Default.con1);
            cnn.Open();

            SqlCommand command;
            SqlDataReader dataReader;

            command = new SqlCommand(sqlInput, cnn);

            dataReader = command.ExecuteReader();

            int count = 0;
            testNameList.Clear();
            bool published = false;

            //goes through the table and retrieves each record 
            while (dataReader.Read())
            {
                try
                {
                    testNameList.Add(Convert.ToString(dataReader.GetValue(2)));
                    if(Convert.ToInt16(dataReader.GetValue(3)).Equals(0))
                    {
                        publishedBoolList.Add(published);
                    }
                    else
                    {
                        published = true;
                        publishedBoolList.Add(published);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
                count++;

            }
            cnn.Close();
        }
        private void checkingMethod(CheckBox cb)
        {
            //method that checks if the test is published and is clicked it will be unpublished, 
            //and if the test is unpublished and is clicked it will be published
            if (cb.IsChecked == true)
            {
                sqlInput = "update Test set Publish = 1 where TestName = '" + cb.Content + "';";
                cnn = new SqlConnection(Properties.Settings.Default.con1);
                cnn.Open();

                SqlCommand command;

                command = new SqlCommand(sqlInput, cnn);

                command.ExecuteNonQuery();

                cnn.Close();
                MessageBox.Show("Test Published");
            }
            if (cb.IsChecked == false)
            {
 
                sqlInput = "update Test set Publish = 0 where TestName = '" + cb.Content + "';";
                cnn = new SqlConnection(Properties.Settings.Default.con1);
                cnn.Open();

                SqlCommand command;

                command = new SqlCommand(sqlInput, cnn);

                command.ExecuteNonQuery();

                cnn.Close();
                MessageBox.Show("Test unublished");
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LecturerViewMarks lvm = new LecturerViewMarks(lecturerID);
            lvm.Show();
            this.Hide();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LectureMenu lm = new LectureMenu(lecturerID);
            lm.Show();
            this.Hide();
        }
    }
}
