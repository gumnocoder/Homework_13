using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework_13.Model.bankModel;
using Homework_13.Service.Command;
using static Homework_13.ViewModel.ClientListViewModel;
using static Homework_13.Model.bankModel.DepositMaker<
    Homework_13.Model.bankModel.BankAccount>;

namespace Homework_13.ViewModel
{
    class DepositMakerViewModel
    {

/*        public DepositMakerViewModel() 
        {
            account = SelectedAccount;
        }
*/
        private int _amount = 0;

        public BankAccount account;
        public int Amount 
        {
            get => _amount;
            set => _amount = value;
/*            {
                if (value.GetType() == typeof(string))
                {
                    int.TryParse(value.ToString(), out _amount);
                }
            }*/
        }

        private RelayCommand _makeDeposit;

        public RelayCommand MakeDeposit => _makeDeposit ??= new(MakeDepositForClientsAccount);

        private void MakeDepositForClientsAccount(object s)
        {
            MakeDeposit(SelectedClient, SelectedAccount, Amount);
        }

        private RelayCommand _selectAccountType;

        public RelayCommand SelectAccountType =>
            _selectAccountType ??= new(SelectType);
        
        private void SelectType(object account)
        {

        }

    }
}
