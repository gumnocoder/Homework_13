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

        #region Свойства

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Рейтинг
        /// </summary>
        public string Reputation { get; set; }

        /// <summary>
        /// Возвращает индентификатор дебетового счёта в string
        /// </summary>
        public string DebitID 
        { get => SelectedClient.DebitIsActive ? SelectedClient.DebitAccountID.ToString() : string.Empty; }

        /// <summary>
        /// Возвращает индентификатор депозитного счёта в string
        /// </summary>
        public string DepositID 
        { get => SelectedClient.DepositIsActive ? SelectedClient.DepositAccountID.ToString() : string.Empty; }

        /// <summary>
        /// Возвращает индентификатор кредитного счёта в string
        /// </summary>
        public string CreditID
        { get => SelectedClient.CreditIsActive ? SelectedClient.CreditAccountID.ToString() : string.Empty; }

        /// <summary>
        /// Конвертирует процент депозита и возвращает строку
        /// </summary>
        public string DepositPercent
        {
            get
            {
                if (SelectedClient.DepositIsActive)
                    return string.Format("{0:f1}%", SelectedClient.ClientsDepositAccount.Percent);
                return string.Empty;
            }
        }

        /// <summary>
        /// Конвертирует процент кредита и возвращает строку
        /// </summary>
        public string CreditPercent
        {
            get
            {
                if (SelectedClient.CreditIsActive)
                    return string.Format("{0:f1}%", SelectedClient.ClientsCreditAccount.Percent);
                return string.Empty;
            }
        }

        /// <summary>
        /// Активность дебетового счёта в строковом формате
        /// </summary>
        public string DebitActivity
        {
            get
            {
                if (SelectedClient.DebitIsActive) return "Активен";
                return "Не активен";
            }
        }

        /// <summary>
        /// Активность кредитного счёта в строковом формате
        /// </summary>
        public string CreditActivity
        {
            get
            {
                if (SelectedClient.CreditIsActive) return "Активен";
                return "Не активен";
            }
        }

        /// <summary>
        /// Активность депозитного счёта в строковом формате
        /// </summary>
        public string DepositActivity
        {
            get
            {
                if (SelectedClient.DepositIsActive) return "Активен";
                return "Не активен";
            }
        }

        /// <summary>
        /// предоставляет доступ к SelectedClient из ClientListViewModel
        /// </summary>
        public Client Selected { get => SelectedClient; }

        #endregion
    }
}
