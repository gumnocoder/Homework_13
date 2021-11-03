using System;
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

        private RelayCommand _appExit;
        public RelayCommand AppExit
        {
            get => _appExit ??= new(ExitBtnClick);
        }

        private void ExitBtnClick(object s) => Environment.Exit(0);
    }
}
