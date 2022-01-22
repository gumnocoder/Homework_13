using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Homework_13.Model.bankModel.interfaces;
using Homework_13.Service;
using static Homework_13.Model.bankModel.Bank;
using static Homework_13.Model.bankModel.TimeChecker;

namespace Homework_13.Model.bankModel
{
    /// <summary>
    /// Депозитный банковский счёт
    /// </summary>
    class BankDepositAccount : BankPercentableAccount
    {
        #region Конструкторы

        /// <summary>
        /// Основной конструктор
        /// </summary>
        /// <param name="client">Клиент владелец депозитного счета</param>
        /// <param name="DepositStartAmount">Стартовая сумма депозита</param>
        /// <param name="PersonalPercent">Процент по вкладу</param>
        /// <param name="Expiration">Срок вклада</param>
        public BankDepositAccount(
            Client client, 
            long DepositStartAmount,
            double PersonalPercent,
            int Expiration)
        {
            if (!client.DepositIsActive)
            {
                TimeCheck.OnTimerSignal += this.DateComparer;
                AccountAmount += DepositStartAmount;
                SetId();
                client.DepositIsActive = true;
                Percent = PersonalPercent;
                ActivationDate = DateTime.UtcNow;
                NextPaymentDay = ActivationDate.AddDays(
                    DateTime.DaysInMonth(
                        ActivationDate.Year,
                        ActivationDate.Month));
                this.Expiration = ++Expiration;
                AddLinkToAccountInBank();
                client.DepositAccountID = ID;
                client.ClientsDepositAccount = 
                    (BankDepositAccount)client.ba<BankDepositAccount>(
                        ref ThisBank.deposits, 
                        client.DepositAccountID);
                Debug.WriteLine($"Expired at: {this.Expiration}");
                new ReputationIncreaser(client, 3);
                _clientID = client.ID;
                Debug.WriteLine(this);
            }
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public BankDepositAccount() { }
        #endregion

        #region Поля
        private long _clientID;
        private const int _minExpiration = 12;
        #endregion


        #region Методы
        /// <summary>
        /// Назначает ID
        /// </summary>
        public override void SetId() => 
            ID = ++ThisBank.CurrentDepositID;

        /// <summary>
        /// выполняет ряд методов по начислению процентов,
        /// или закрывает счёт и переводит средства на дебетовый 
        /// счёт если срок депозита вышел
        /// </summary>
        public override void TurnMonth()
        {
            AddPercents();
            if (!Expired())
            {
                --Expiration;
                CalculateNextPaymentDay();
            }
            else
            {
                new AcccountDepositHandler(
                    SearchEngine.SearchByID(
                        ClientList<Client>.ClientsList,
                        _clientID), this).OnExpired();
            }
        }

        /// <summary>
        /// Добавляет экземпляр депозитного счёта 
        /// в соответствующий список в синглтоне ThisBank
        /// </summary>
        public override void AddLinkToAccountInBank()
        {
            ThisBank.Deposits.Add(this);
            Debug.WriteLine(this);
        }


        /// <summary>
        /// Возвращает информацию о 
        /// депозите в строковом формате
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"депозитный счёт " +
                $"{AccountAmount}$ " +
                $"ID - {ID} " +
                $"{ActivationDate} " +
                $"{Percent}%";
        }

        ~BankDepositAccount() { Debug.WriteLine("вызван деструтор класса BankDepositAccount"); }
        #endregion
    }
}
