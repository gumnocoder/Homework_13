using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Homework_13.View.Windows;
using Homework_13.ViewModel;

namespace Homework_13.Service
{
    class HudViewer
    {
        public static async void ShowHudWindow(string message, bool Positive, bool Negative)
        {
            HudWindowView hudWindow = default;
            await Task.Delay(500);

            if (Positive)
            {
                hudWindow = new()
                {
                    DataContext = new HudWindowViewModel()
                    {
                        Message = message,
                        Positive = true,
                        Negative = false
                    }
                };
            }

            else if (Negative)
            {
                hudWindow = new()
                {
                    DataContext = new HudWindowViewModel()
                    {
                        Message = message,
                        Positive = false,
                        Negative = true
                    }
                };
            }

            else
            {
                hudWindow = new()
                {
                    DataContext = new HudWindowViewModel()
                    {
                        Message = message,
                        Positive = false,
                        Negative = false
                    }
                };
            }

            hudWindow.Owner = 
                Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
            hudWindow.WindowStartupLocation = WindowStartupLocation.Manual;

            hudWindow.Show();
            await Task.Delay(4000);
            hudWindow.Close();
        }
    }
}
