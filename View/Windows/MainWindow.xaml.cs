using System.Windows.Input;

namespace Homework_13.View.Windows
{
    public partial class MainWindow
    {
        public MainWindow() =>InitializeComponent();

        private void Window_MouseLeftButtonDown(
            object sender, 
            MouseButtonEventArgs e) => DragMove();
    }
}
