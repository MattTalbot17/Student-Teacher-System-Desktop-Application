using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace _18003819_PROG6212_Task2
{
    class TestCreator
    {
        public class TestEventArgs : EventArgs
        {
            public TestClass TestClass { get; set; }
        }
        //Declare delegate
        public delegate void TestCreatedEventHandler(object source, TestEventArgs args);

        //Define event
        public event TestCreatedEventHandler TestCreated;

        //raise event
        public void Creator(TestClass testClass)
        {
            MessageBox.Show("Checking if this test has been created before...");
            Thread.Sleep(2000);

            OnTestCreated(testClass);
        }
        protected virtual void OnTestCreated(TestClass testClass)
        {
            if (TestCreated != null)
                TestCreated(this, new TestEventArgs() { TestClass = testClass });
        }
    }
}
