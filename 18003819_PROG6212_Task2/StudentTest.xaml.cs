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
    /// Interaction logic for StudentMenu.xaml
    /// </summary>
    public partial class StudentTest : Window
    {
        //Global variables

        //declarations to be used in more than one method
        string studentNumber, testName, result, studentTestID;
        int questionCounter = 0, score = 0;
        double studentsPercentage;


        //Lists for Test Names, Student Answers and a list of object class "Answers Class"
        List<string> testNameList = new List<string>();
        List<string> studentAnswer = new List<string>();
        List<string> StudentTestIDList = new List<string>();
        List<AnswersClass> answersClasses = new List<AnswersClass>();
        List<StudentClass> TestMarkList = new List<StudentClass>();
        List<TestClass> testClassList = new List<TestClass>();

        //Declarations to connect to SQL Server
        String sql = "";
        SqlConnection cnn;
        SqlCommand command;
        SqlDataAdapter adapter = new SqlDataAdapter();

        public StudentTest(string StudentNumber)
        {
            studentNumber = StudentNumber;
            InitializeComponent();
            testNameComboBox.IsReadOnly = true;
            studentTest.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //When the students menu loads, the test names from the database are loaded into the combo box for the student to select
            QueryDatabaseTestTable();
            backButton.Visibility = Visibility.Hidden;

            for (int i = 0; i < testNameList.Count; i++)
            {
                testNameComboBox.Items.Add(testNameList[i].ToString());
            }
        }
        private void TestNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //method to check if the user has completed a test they have selected. if they have display their mark along with a 
            //message, if they havent load the questions
            QueryCompletedTests();
            bool completed = false;
            for (int i = 0; i < TestMarkList.Count; i++)
            {
                if (testNameComboBox.SelectedItem.ToString().Equals(TestMarkList[i].Test))
                {
                    completed = true;
                    MessageBox.Show("You have already completed this test,\nYour Mark Was: " + TestMarkList[i].Mark + "%");
                    break;
                }
            }
            if (completed == false)
            {
                //When a test is selected it loads the questions and possible answers for that sepcific test
                testName = testNameComboBox.SelectedItem.ToString();
                //calls another method to load the first question
                loadQuestions(questionCounter);
                testNameComboBox.IsEnabled = false;
                Height = 352;
                Width = 484;
            }
        }
        public void loadQuestions(int nextQ)
        {
            //method to load questions 

            //questionCounter is retrieved after each question has been answered and submitted or  the user has moved to the previous Question
            nextQ = questionCounter;
            //method caled to load questions and answers
            QueryDatabaseQuestion_AnswerTable();
            bool check = false;
            try
            {
                if (testClassList[nextQ].Question != null)
                {
                    check = true;
                    //populates the question label and Possible answer radio buttons
                    questionNumberLabel.Content = Convert.ToString(nextQ + 1);
                    questionTBALabel.Content = testClassList[nextQ].Question;
                    optionAButton.Content = testClassList[nextQ].Answer1;
                    optionBButton.Content = testClassList[nextQ].Answer2;
                    optionCButton.Content = testClassList[nextQ].Answer3;
                    optionDButton.Content = testClassList[nextQ].Answer4;
                }
                if (nextQ > 0)
                    backButton.Visibility =  Visibility.Visible;
                else
                    backButton.Visibility = Visibility.Hidden;
            }
            catch
            {

            }
            if (check == false)
            {
               //once the user clicks submit on the last question a message box will pop up asking if they want to see their results or edit their answers
                MessageBoxResult result = MessageBox.Show("Would you like to View your Grades (Yes)\n or Edit Answers (NO)", "Test Finished", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                    displayResults();
            }
        }
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            //method for what happens when the user has chosen an answer and submitted it

            //method call to check if one of the buttons have been checked
            if (buttonCheck() == false)
            {
                MessageBox.Show("Please Select an Answer");
            }
            else
            {
                //method call to reset the radio buttons to unchecked
                ResetRadioButtons();
                // method call to compare users answer to answer in database
                compareAnswers(questionCounter);
                questionCounter++;
                //method call to load the next question
                loadQuestions(questionCounter);
            }
        }
        private bool buttonCheck()
        {
            //int counter = questionCounter + 1;

            bool isChecked = false;
            //method to check which button is checked and to add the text of the radio button to a list of answers
            if (optionAButton.IsChecked == true)
            {
                //studentAnswer[questionCounter] = optionAButton.Text;
                studentAnswer.Add(optionAButton.Content.ToString());
                isChecked = true;
            }
            else if (optionBButton.IsChecked == true)
            {
                //studentAnswer[questionCounter] = optionBButton.Text;
                studentAnswer.Add(optionBButton.Content.ToString());
                isChecked = true;
            }
            else if (optionCButton.IsChecked == true)
            {
                studentAnswer.Add(optionCButton.Content.ToString());
                isChecked = true;
            }
            else if (optionDButton.IsChecked == true)
            {
                studentAnswer.Add(optionDButton.Content.ToString());
                isChecked = true;
            }
            else
            {
                isChecked = false;
            }
            return isChecked;
        }
        private void ResetRadioButtons()
        {
            //method to reset radio buttons
            optionAButton.IsChecked = false;
            optionBButton.IsChecked = false;
            optionCButton.IsChecked = false;
            optionDButton.IsChecked = false;
        }
        private void compareAnswers(int qNumb)
        {
            //method to compare users results to the correct answer from the database
            qNumb = questionCounter;
            for (int i = questionCounter; i < testClassList.Count;)
            {
                if (testClassList[i].CorrectAnswer == studentAnswer[i].ToString())
                {
                    //if the users answer matches the correct answer the result is correct
                    score++;
                    result = "Correct";
                    AnswersClass temp = new AnswersClass(studentAnswer[questionCounter].ToString(), testClassList[i].CorrectAnswer, result);
                    answersClasses.Insert(qNumb, temp);
                    break;
                }
                else
                {
                    //if the users answer does not match the correct answer from the database then the result is incorrect
                    result = "Incorrect";
                    AnswersClass temp = new AnswersClass(studentAnswer[questionCounter].ToString(), testClassList[i].CorrectAnswer, result);
                    answersClasses.Insert(qNumb, temp);
                    break;
                }
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            //method for if the user wants to go to the previous question to change their answer

            //resets the buttons
            ResetRadioButtons();

            //alters the question number to go back by one
            questionCounter = questionCounter - 1;

            //check to see if their answer was correct that their score would go done one
            if (answersClasses[questionCounter].Result == "Correct")
                score--;

            //removes the previous object and answer from the Lists
            answersClasses.RemoveAt(questionCounter);
            studentAnswer.RemoveAt(questionCounter);

            //load questions with altered question number 
            loadQuestions(questionCounter);
        }
        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            //method called when user clicks the finish button during the test
            displayResults();
        }
        private void displayResults()
        {
            //what happens when the user clicks the finish button or they've finished the test
            pushToStudentAnswerTable();
            //code to display the Memo of the test taken
            resultTextBox.Visibility = Visibility.Visible;
            questionLabel.Visibility = Visibility.Hidden;
            questionNumberLabel.Visibility = Visibility.Hidden;
            optionAButton.Visibility = Visibility.Hidden;
            optionBButton.Visibility = Visibility.Hidden;
            optionCButton.Visibility = Visibility.Hidden;
            optionDButton.Visibility = Visibility.Hidden;
            backButton.Visibility = Visibility.Hidden;
            submitButton.Visibility = Visibility.Hidden;
            finishButton.Visibility = Visibility.Hidden;
            questionTBALabel.Visibility = Visibility.Hidden;
            Width = 600;
            resultTextBox.Text += "Question: " + "\tStudents Answer:" + "\t\tCorrect Answer:" + "\t\tResult: " + Environment.NewLine;
            try
            {
                for (int i = 0; i <= questionCounter; i++)
                {
                    if (answersClasses[i].StudentsAnswer.Length < 16)
                        answersClasses[i].StudentsAnswer = answersClasses[i].StudentsAnswer + "\t\t";
                    if (answersClasses[i].CorrectAnswer.Length < 16)
                        answersClasses[i].CorrectAnswer = answersClasses[i].CorrectAnswer + "\t\t";
                    resultTextBox.Text += (i + 1).ToString() + "\t\t" + answersClasses[i].StudentsAnswer + "\t" + answersClasses[i].CorrectAnswer + "\t" + answersClasses[i].Result + Environment.NewLine;
                }
            }
            catch
            {

            }
            //calculates the percentage for the user
            studentsPercentage = (100 * score) / questionCounter;
            resultTextBox.Text += "\n\nScore: " + score + "/" + questionCounter + "\tPercentage (%): " + studentsPercentage;
            //code to send the users data to the database
            pushToDatabase();
        }
        private void pushToDatabase()
        {
            //method used to send the data to the SQL database

            //method called to get last ID and to alter the next ID to be inserted
            AddStudentTestID();
            try
            {
                cnn = new SqlConnection(Properties.Settings.Default.con1);
                cnn.Open();


                sql = "Insert into Student_Test(StudentTestID,TestName, StudentNumber, TestMark) values ('" + studentTestID + "', '" + this.testName + "', '" + studentNumber + "','" + Convert.ToString(studentsPercentage) + "');";


                command = new SqlCommand(sql, cnn);

                adapter.InsertCommand = new SqlCommand(sql, cnn);
                adapter.InsertCommand.ExecuteNonQuery();

                command.Dispose();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void AddStudentTestID()
        {
            //method to alter the ID

            //method call to get the last ID from the database
            QueryDatabaseStudent_TestTable();
            int count = 1;
            if (StudentTestIDList.Count == 0)
                studentTestID = "ST001";
            else
            {
                foreach (var item in StudentTestIDList)
                {
                    //breaks string into a substring of string and Integer
                    if (count != Int32.Parse(item.ToString().Substring(2, 3)))
                    {
                        if (count < 10)
                        {
                            studentTestID = "ST00" + count;
                        }
                        else if (count < 100 && count > 10)
                        {
                            studentTestID = "ST0" + count;
                        }
                        else
                        {
                            studentTestID = "ST" + count;
                        }
                    }
                    else
                    {
                        count++;
                        if (count > StudentTestIDList.Count)
                        {
                            if (count < 10)
                            {
                                studentTestID = "ST00" + count;
                            }
                            else if (count < 100 && count >= 10)
                            {
                                studentTestID = "ST0" + count;
                            }
                            else
                            {
                                studentTestID = "ST" + count;
                            }
                        }
                    }
                }
            }
        }
        private void QueryDatabaseStudent_TestTable()
        {
            //method that pulls in the data from my tables in the database
            string sqlInput = "select StudentTestID from Student_Test;";
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
                    StudentTestIDList.Add(Convert.ToString(dataReader.GetValue(0)));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
                count++;

            }
            cnn.Close();
        }
        private void BackMenuButton_Click(object sender, RoutedEventArgs e)
        {
            StudentMenu studentMenu = new StudentMenu(studentNumber);
            studentMenu.Show();
            this.Hide();
        }
        private void QueryDatabaseTestTable()
        {
            //method that pulls in the data from my tables in the database
            //Publish variable in the datase is describing whether the test is available for the srudents to take or not
            string sqlInput = "select * from Test where Publish = 1;";
            SqlConnection cnn;
            cnn = new SqlConnection(Properties.Settings.Default.con1);
            cnn.Open();

            SqlCommand command;
            SqlDataReader dataReader;

            command = new SqlCommand(sqlInput, cnn);

            dataReader = command.ExecuteReader();

            int count = 0;
            testNameList.Clear();

            //goes through the table and retrieves each record 
            while (dataReader.Read())
            {
                try
                {
                    testNameList.Add(Convert.ToString(dataReader.GetValue(2)));
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
            string sqlInput = "select QuestionID, Question, Answer1, Answer2, Answer3, Answer4, CorrectAnswer from Question_Answer, Test where Test.TestID = Question_Answer.TestID AND TestName = '" + testName + "';";
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
        private void QueryCompletedTests()
        {
            //method that pulls in the data from my tables in the database
            string sqlInput = "select T.TestName, TestMark from Student_Test ST join Test T on T.TestName = ST.TestName where ST.StudentNumber = '" + studentNumber + "';";
            SqlConnection cnn;
            cnn = new SqlConnection(Properties.Settings.Default.con1);
            cnn.Open();

            SqlCommand command;
            SqlDataReader dataReader;

            command = new SqlCommand(sqlInput, cnn);

            dataReader = command.ExecuteReader();

            int count = 0;
            testNameList.Clear();

            //goes through the table and retrieves each record 
            while (dataReader.Read())
            {
                try
                {
                    TestMarkList.Add(new StudentClass { Test = Convert.ToString(dataReader.GetValue(0)), Mark = Convert.ToDouble(dataReader.GetValue(1)) });
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
                count++;

            }
            cnn.Close();
        }
        private void pushToStudentAnswerTable()
        {
            //method used to send the data to the SQL database

            //method called to get last ID and to alter the next ID to be inserted
            try
            {
                cnn = new SqlConnection(Properties.Settings.Default.con1);
                cnn.Open();
                for (int i = 0; i < studentAnswer.Count; i++)
                {
                    sql = "Insert into StudentAnswer(StudentNumber, QuestionID, Answer) values ('" + studentNumber + "','" + testClassList[i].QuestionID + "','" + studentAnswer[i] +"');";

                    command = new SqlCommand(sql, cnn);

                    adapter.InsertCommand = new SqlCommand(sql, cnn);
                    adapter.InsertCommand.ExecuteNonQuery();

                    command.Dispose();

                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
