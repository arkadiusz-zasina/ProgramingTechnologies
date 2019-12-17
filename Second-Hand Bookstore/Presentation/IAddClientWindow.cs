namespace Presentation
{
    public interface IAddClientWindow
    {
        void InitializeComponent();
        void Show();
        void Close();
        object DataContext { get; set; }
    }
}