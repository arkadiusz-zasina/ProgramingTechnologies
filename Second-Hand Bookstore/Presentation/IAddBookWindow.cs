namespace Presentation
{
    public interface IAddBookWindow
    {
        void InitializeComponent();
        void Close();
        void Show();
        object DataContext { get; set; }
    }
}