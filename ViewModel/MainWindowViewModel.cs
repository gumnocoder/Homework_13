using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_13.ViewModel
{
    class MainWindowViewModel
    {
        private string _tittle;
        public string Tittle
        {
            get => _tittle;
            set => _tittle = value;
        }

        public MainWindowViewModel()
        {
            Tittle = "Банк";
        }
    }
}
