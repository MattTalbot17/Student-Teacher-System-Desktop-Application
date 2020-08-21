using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static _18003819_PROG6212_Task2.TestCreator;

namespace _18003819_PROG6212_Task2
{
    class MessageService
    {
        //subscriber class that recieves the objects values and prints them out when called
        public void OnTestCreated(object source, TestEventArgs e)
        {
            MessageBox.Show("No Results Found, " + e.TestClass.TestName + " has been Created");

        }
    }
}
