namespace Presentation
{
    public interface IEditClientWindow
    {
        void InitializeComponent();
        void Close();
        void Show();
        object DataContext { get; set; }
    }
}