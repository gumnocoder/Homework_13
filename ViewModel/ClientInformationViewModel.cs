using System.Diagnostics;
using Homework_13.Model;
using static Homework_13.ViewModel.ClientListViewModel;

namespace Homework_13.ViewModel
{
    class ClientInformationViewModel
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ClientInformationViewModel()
        {
            if (SelectedClient != null)
            {
                ID = SelectedClient.ClientID.ToString();
                Reputation = SelectedClient.Reputation.ToString();
            }
            else Debug.WriteLine("SelectedClient было null");
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Рейтинг
        /// </summary>
        public string Reputation { get; set; }

        public string DebitID { get => SelectedClient.DebitAccountID.ToString(); }

        public string DepositID 
        { 
            get
            {
                return SelectedClient.DepositIsActive ? SelectedClient.DepositAccountID.ToString() : string.Empty;
            }
        }
        public string DepositPercent
        {
            get
            {
                if (SelectedClient.DepositIsActive)
                    return string.Format("{0:f1}%", SelectedClient.ClientsDepositAccount.Percent);
                return string.Empty;
            }
        }

        public string DebitActivity
        {
            get
            {
                if (SelectedClient.DebitIsActive) return "Активен";
                return "Не активен";
            }
        }

        public string CreditActivity
        {
            get
            {
                if (SelectedClient.CreditIsActive) return "Активен";
                return "Не активен";
            }
        }

        public string DepositActivity
        {
            get
            {
                if (SelectedClient.DepositIsActive) return "Активен";
                return "Не активен";
            }
        }

        public Client Selected { get => SelectedClient; }
    }
}
