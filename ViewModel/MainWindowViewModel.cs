using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Homework_13.Service.Command;

namespace Homework_13.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
        private string _tittle;
        public string Tittle
        {
            get => _tittle;
            set { _tittle = value; OnPropertyChanged(); }
        }

        public MainWindowViewModel()
        {
            Tittle = "Банк";
        }

        private SimpleCommand _appExit;
        public SimpleCommand AppExit
        {
            get => _appExit ??= new SimpleCommand(() => ExitBtnClick());
        }

        private void ExitBtnClick() => Environment.Exit(0);
    }
}
