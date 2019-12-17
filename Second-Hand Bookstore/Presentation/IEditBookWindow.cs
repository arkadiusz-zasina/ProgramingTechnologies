using System.ComponentModel;
using System.Windows;

namespace Presentation
{
    public interface IEditBookWindow
    {
        void InitializeComponent();
        void Show();
        void Close();
        object DataContext { get; set; }
    }
}