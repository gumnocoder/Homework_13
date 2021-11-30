using Homework_13.Model;

namespace Homework_13.ViewModel
{
    class AccountOpeningViewModel : BaseViewModel
    {
        private bool _deposit = false;
        private bool _credit = false;
        private double _personalPercent;

        public bool Deposit
        {
            get => _deposit;
            set
            {
                _deposit = value;
                OnPropertyChanged();
            }
        }

        public bool Credit
        {
            get => _credit;
            set
            {
                _credit = value;
                OnPropertyChanged();
            }
        }

        public double PersonalPercent
        {
            get => _personalPercent;
            set
            {
                _personalPercent = value;
                OnPropertyChanged();
            }
        }
        public AccountOpeningViewModel(double Percent, bool Credit, bool Deposit)
        {
            this.Credit = Credit;
            this.Deposit = Deposit;
            PersonalPercent = Percent;
        }

        public AccountOpeningViewModel() { }
    }
}
