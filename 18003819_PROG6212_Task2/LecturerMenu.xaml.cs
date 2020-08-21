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
    public partial class LectureMenu : Window
    {
        /// <summary>
        /// Lecture menu to guide sLecturers on where to go
        /// </summary>
        string lecturerID;
        public LectureMenu(string LectureID)
        {
            //when the menu loads it carries through the Lecture ID
            lecturerID = LectureID;
            InitializeComponent();
            LecturerMenu.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void CreateTestButton_Click(object sender, RoutedEventArgs e)
        {
            //button to take you to the create test Screen
            CreateTest createTest = new CreateTest(lecturerID);
            createTest.Show();
            this.Hide();
        }
        private void ClassListButton_Click(object sender, RoutedEventArgs e)
        {
            //button to view the class list
            ClassListForm classListForm = new ClassListForm(lecturerID);
            classListForm.Show();
            this.Hide();
        }
        private void LogOffButton_Click(object sender, RoutedEventArgs e)
        {
            //button to  log off 
            MainWindow logIn = new MainWindow();
            logIn.Show();
            this.Hide();
        }
        private void UpdateProfileButton_Click(object sender, RoutedEventArgs e)
        {
            //button click to take the lecturer to a form where they are able to edit their password
            updateLProfile updateLProfile = new updateLProfile(lecturerID);
            updateLProfile.Show();
            this.Hide();
        }
        private void ViewTestButton_Click(object sender, RoutedEventArgs e)
        {
            //button click method for lecturer to view all their Tests created and to publish/unpublish tests
            ViewTestList vtl = new ViewTestList(lecturerID);
            vtl.Show();
            this.Hide();
        }
    }
}
