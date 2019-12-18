namespace Presentation
{
    public interface IEventLogsWindow
    {
        void InitializeComponent();
        void Close();
        void Show();
        object DataContext { get; set; }
    }
}