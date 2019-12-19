using Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    class TestEditClientWindow : IEditClientWindow
    {
        public object DataContext 
        {
            get; set;
        }

        public void Close()
        {
            Console.WriteLine("Closing");
        }

        public void InitializeComponent()
        {
            Console.WriteLine("Initialize Component");
        }

        public void Show()
        {
            Console.WriteLine("Show something");
        }
    }
}
