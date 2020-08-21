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
    /// Form that allows a lecturer to create a test, edit a test and add questions to that test
    /// </summary>

    public partial class CreateTest : Window
    {
        //global declarations of class
        //declaring lists to be used in multiple methods and for temporary storage
        List<String> TestIDList = new List<string>();
        List<String> questionIDList = new List<string>();
        List<String> testNameList = new List<string>();
        List<String> questionNameList = new List<string>();
        string lecturerID, testID, questionID;
        bool check = false;

        //declaring connection to SQL server
        SqlCommand command;
        SqlDataAdapter adapter = new SqlDataAdapter();
        String sql = "";
        SqlConnection cnn;
        public CreateTest(string LecturerID)
        {
            //when the form loads it saves the lecturer Id that has logged on
            lecturerID = LecturerID;
            InitializeComponent();
            createTest.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        private void AddTestButton_Click(object sender, RoutedEventArgs e)
        {
            //method to add a test to database

            //method call to check and add a test ID 
            AddTestID();

            //code to check if Test exists in database
            Boolean check = true;
            if (testNameTextBx.Text != "")
            {
                foreach (var item in testNameList)
                {
                    if (testNameTextBx.Text == item)
                    {
                        check = false;
                    }
                }
                if (check == false)
                {
                    MessageBox.Show("Test Already exists");
                }
                else
                {
                    try
                    {
                        ////
                        //Delegate And Event to show test has been created and to instruct user to add questions
                        //declaring object, Publisher and Subscriber
                        ////

                        var testClass = new TestClass(testID, lecturerID, testNameTextBx.Text.ToString());
                        var testCreator = new TestCreator();//publisher
                        var messageService = new MessageService();//subscriber

                        //making a subscription
                        testCreator.TestCreated += messageService.OnTestCreated;

                        //displaying message from event
                        testCreator.Creator(testClass);

                        //inserting values to database
                        cnn = new SqlConnection(Properties.Settings.Default.con1);
                        cnn.Open();

                        sql = "Insert into Test(TestID,LecturerID, TestName, Publish) values ('" + testID + "', '" + lecturerID + "', '" + testNameTextBx.Text.ToString() + "', '" + 1 + "');";


                        command = new SqlCommand(sql, cnn);
                        adapter.InsertCommand = new SqlCommand(sql, cnn);
                        adapter.InsertCommand.ExecuteNonQuery();
                        command.Dispose();
                        cnn.Close();

                        check = true;

                        testNameTextBx.IsEnabled = false;
                        backButton.Margin = new Thickness(57, 550, 0, 0);
                        Height = 650;
                        Width = 550;
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            else
                MessageBox.Show("Please enter in a Test name", "Test Error", MessageBoxButton.OK);
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            //goes back to the Lecturers menu
            LectureMenu lecturerMenu = new LectureMenu(lecturerID);
            lecturerMenu.Show();
            this.Hide();
        }
        private void AddQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            //Method to add questions to a database
            bool exists = false;

            if (questionNameList.Count == 0)
            {
                exists = true;
                addQuestionToDatabase();
            }
            //adds Question to  Database
            for (int i = 0; i < questionNameList.Count; i++)
            {
                if (!questionTextBox.Text.Equals(questionNameList[i]))
                {
                    exists = true;
                    addQuestionToDatabase();
                    break;
                }
            }
            if (exists == false)
                MessageBox.Show("Question already exists");
        }
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            //once lecturer is finished creating a test and adding Questions this button takes them to their menu
            LectureMenu lecturerMenu = new LectureMenu(lecturerID);
            lecturerMenu.Show();
            this.Hide();
        }
        private void addQuestionToDatabase()
        {
            //method called to check last Question ID and alter it to add another

            AddQuestionID();
            if (questionTextBox.Text != "" && answer1TextBox.Text != "" && answer2TextBox.Text != "" && answer3TextBox.Text != "" && answer4TextBox.Text != "" && correctAnswerTextBox.Text != "")
            {
                if (correctAnswerTextBox.Text == answer1TextBox.Text || correctAnswerTextBox.Text == answer2TextBox.Text || correctAnswerTextBox.Text == answer3TextBox.Text || correctAnswerTextBox.Text == answer4TextBox.Text)
                {
                    try
                    {
                        //pushes values to database
                        cnn = new SqlConnection(Properties.Settings.Default.con1);
                        cnn.Open();

                        sql = "Insert into Question_Answer(QuestionID,TestID, Question, Answer1, Answer2,Answer3,Answer4,CorrectAnswer)" +
                            " values ('" + questionID + "', '" + testID + "', '" + questionTextBox.Text + "', '" + answer1TextBox.Text + "', '" + answer2TextBox.Text + "', '" + answer3TextBox.Text + "', '" + answer4TextBox.Text + "', '" + correctAnswerTextBox.Text + "');";


                        command = new SqlCommand(sql, cnn);
                        adapter.InsertCommand = new SqlCommand(sql, cnn);
                        adapter.InsertCommand.ExecuteNonQuery();

                        command.Dispose();
                        cnn.Close();

                        MessageBox.Show("Question has been added", "Success");
                        questionTextBox.Text = "";
                        answer1TextBox.Text = "";
                        answer2TextBox.Text = "";
                        answer3TextBox.Text = "";
                        answer4TextBox.Text = "";
                        correctAnswerTextBox.Text = "";
                        check = true;
                    }
                    catch (Exception r)
                    {
                        MessageBox.Show(r.ToString());
                    }
                }
                else
                    MessageBox.Show("Correct Answer does not match any Answer options", "Answer Error", MessageBoxButton.OK);
            }
 
            if (check == false)
            {
                MessageBox.Show("Empty Field, please fill in all fields", "Question Error", MessageBoxButton.OK);
            }



        }
        private void QueryDatabaseTestTable()
        {
            //method that pulls in the data from my tables in the database
            string sqlInput = "select TestID, TestName from Test;";
            SqlConnection cnn;
            cnn = new SqlConnection(Properties.Settings.Default.con1);
            cnn.Open();
            SqlCommand command;
            SqlDataReader dataReader;
            command = new SqlCommand(sqlInput, cnn);
            dataReader = command.ExecuteReader();
            int count = 0;
            TestIDList.Clear();
            testNameList.Clear();
            //goes through the table and retrieves each record 
            while (dataReader.Read())
            {
                try
                {
                    TestIDList.Add(Convert.ToString(dataReader.GetValue(0)));
                    testNameList.Add(Convert.ToString(dataReader.GetValue(1)));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
                count++;
            }
            cnn.Close();
        }
        private void QueryDatabaseQuestion_AnswerTable()
        {
            //method that pulls in the data from my tables in the database
            string sqlInput = "select QuestionID,Question from Question_Answer;";
            SqlConnection cnn;
            cnn = new SqlConnection(Properties.Settings.Default.con1);
            cnn.Open();

            SqlCommand command;
            SqlDataReader dataReader;

            command = new SqlCommand(sqlInput, cnn);

            dataReader = command.ExecuteReader();

            int count = 0;
            questionIDList.Clear();

            //goes through the table and retrieves each record 
            while (dataReader.Read())
            {
                try
                {
                    questionIDList.Add(Convert.ToString(dataReader.GetValue(0)));
                    questionNameList.Add(Convert.ToString(dataReader.GetValue(1)));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
                count++;

            }
            cnn.Close();
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            //method to edit a test
            //the test name inserted is compared to the tests that exist, if found, the lecturer is able to add a new question to it
            QueryDatabaseTestTable();
            bool exists = false;
            for (int i = 0; i < testNameList.Count; i++)
            {
                if (testNameTextBx.Text.Equals(testNameList[i]))
                {
                    exists = true;
                    testID = TestIDList[i];
                    testNameTextBx.IsEnabled = false;
                    backButton.Margin = new Thickness(57, 550, 0, 0);
                    Height = 650;
                    Width = 550;
                }
            }
            if (exists == false)
                MessageBox.Show("Test Does not exist");

        }
        private void AddTestID()
        {
            //method to alter test Id in order to add new Test
            QueryDatabaseTestTable();
            int count = 1;

            if (TestIDList.Count == 0)
            {
                testID = "TE001";
            }
            else
            {
                foreach (var item in TestIDList)
                {
                    if (count != Int32.Parse(item.ToString().Substring(2, 3)))
                    {
                        if (count < 10)
                        {
                            testID = "TE00" + count;
                        }
                        else if (count < 100 && count > 10)
                        {
                            testID = "TE0" + count;
                        }
                        else
                        {
                            testID = "TE" + count;
                        }
                    }
                    else
                    {
                        count++;
                        if (count > TestIDList.Count)
                        {
                            if (count < 10)
                            {
                                testID = "TE00" + count;
                            }
                            else if (count < 100 && count >= 10)
                            {
                                testID = "TE0" + count;
                            }
                            else
                            {
                                testID = "TE" + count;
                            }
                        }
                    }
                }
            }
        }
        private void AddQuestionID()
        {
            //method to alter Question Id in order to add new Question
            QueryDatabaseQuestion_AnswerTable();
            int count = 1;

            if (questionIDList.Count == 0)
            {
                questionID = "Q0001";
            }
            else
            {
                foreach (var item in questionIDList)
                {
                    if (count != Int32.Parse(item.ToString().Substring(1, 4)))
                    {
                        if (count < 10)
                        {
                            questionID = "Q000" + count;
                        }
                        else if (count < 100 && count > 10)
                        {
                            questionID = "Q00" + count;
                        }
                        else if (count < 1000 && count > 100)
                        {
                            questionID = "Q0" + count;
                        }
                        else if (count > 1000)
                            questionID = "Q" + count;
                    }
                    else
                    {
                        count++;
                        if (count > questionIDList.Count)
                        {
                            if (count < 10)
                            {
                                questionID = "Q000" + count;
                            }
                            else if (count < 100 && count >= 10)
                            {
                                questionID = "Q00" + count;
                            }
                            else if (count < 1000 && count >= 100)
                            {
                                questionID = "Q0" + count;
                            }
                            else if (count >= 1000)
                                questionID = "Q" + count;
                        }
                    }
                }
            }
        }
    }
}
