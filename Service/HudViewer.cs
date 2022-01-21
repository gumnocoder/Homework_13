using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Homework_13.View.Windows;
using Homework_13.ViewModel;

namespace Homework_13.Service
{
    class HudViewer
    {
        public static async void ShowHudWindow(string message)
        {
            await Task.Delay(500);
            HudWindowView hudWindow = new() 
            { 
                DataContext = new HudWindowViewModel() { Message = message } 
            };

            hudWindow.Owner = 
                Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
            hudWindow.WindowStartupLocation = WindowStartupLocation.Manual;

            hudWindow.Show();
            await Task.Delay(4000);
            hudWindow.Close();
        }
    }
}
