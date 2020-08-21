using System;
using System.Collections.Generic;
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
    /// Interaction logic for StudentMenu.xaml
    /// </summary>
    public partial class StudentMenu : Window
    {
        string studentNumber;
        public StudentMenu(string StudentNumber)
        {
            InitializeComponent();
            studentNumber = StudentNumber;
            studentMenu.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        private void TakeTestButton_Click(object sender, RoutedEventArgs e)
        {
            //button click method to take the student to a form where they can take a test
            StudentTest st = new StudentTest(studentNumber);
            st.Show();
            this.Hide();
        }
        private void ViewMarksButton_Click(object sender, RoutedEventArgs e)
        {
            //opens a form with the studenst tests theyve taken and their results
            StudentResultsForm studentResultsForm = new StudentResultsForm(studentNumber);
            studentResultsForm.Show();
            this.Hide();
        }
        private void LogOffButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow LogIn = new MainWindow();
            LogIn.Show();
            this.Hide();

        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            //button click method for student to update their password
            UpdateProfile up = new UpdateProfile(studentNumber);
            up.Show();
            this.Hide();
        }
    }
}
