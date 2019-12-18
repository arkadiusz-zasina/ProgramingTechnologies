using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Presentation
{
    /// <summary>
    /// Logika interakcji dla klasy EventLogsWindow.xaml
    /// </summary>
    public partial class EventLogsWindow : Window, IEventLogsWindow
    {
        public EventLogsWindow()
        {
            InitializeComponent();
            Closing += OnWindowClosing;
        }
        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
    }
}
