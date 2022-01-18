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
    class BankDepositAccount : BankAccount, IPercentContainer, IExpiring
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
                _activationDate = DateTime.UtcNow;
                NextPaymentDay = _activationDate.AddDays(
                    DateTime.DaysInMonth(
                        _activationDate.Year,
                        _activationDate.Month));
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
        private double _percent;
        private int _expiration;
        private long _clientID;
        private const int _minExpiration = 12;
        private DateTime _activationDate;
        private DateTime _nextPaymentDay;
        #endregion

        #region Свойства
        /// <summary>
        /// дата следующего начисления процентов
        /// </summary>
        public DateTime NextPaymentDay
        {
            get => _nextPaymentDay;
            set
            {
                _nextPaymentDay = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Процент по вкладу
        /// </summary>
        public double Percent 
        { 
            get => _percent;
            set => _percent = value;
        }
        /// <summary>
        /// Срок истечения вклада
        /// </summary>
        public int Expiration 
        { 
            get => _expiration;
            set => _expiration = value;
        }
        /// <summary>
        /// Дата открытия счёта
        /// </summary>
        public DateTime ActivationDate 
        { 
            get => _activationDate; 
            set => _activationDate = value; 
        }

        #endregion

        #region Методы
        /// <summary>
        /// Назначает ID
        /// </summary>
        public override void SetId() => 
            ID = ++ThisBank.CurrentDepositID;

        /// <summary>
        /// Проверяет истёк ли срок вклада
        /// </summary>
        /// <returns></returns>
        public bool Expired() => Expiration == 0;

        /// <summary>
        /// выполняет ряд методов по начислению процентов,
        /// или закрывает счёт и переводит средства на дебетовый 
        /// счёт если срок депозита вышел
        /// </summary>
        public void TurnMonth()
        {
            AddPercents();
            if (!Expired())
            {
                --Expiration;
                CalculateNextPaymentDay();
                TurnMonth();
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
        /// Начисляет проценты за прошедший месяц
        /// </summary>
        public void AddPercents()
        {
            AccountAmount += (Convert.ToInt64(
                Math.Round((double)(AccountAmount * Percent / 100 / 12))));
        }

        /// <summary>
        /// Расчитывает дату следующего начисления процентов
        /// </summary>
        public void CalculateNextPaymentDay()
        {
            NextPaymentDay = NextPaymentDay.AddDays(
                DateTime.DaysInMonth(
                    NextPaymentDay.Year,
                    NextPaymentDay.Month));
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

        public void ComprareDateToNextPaymentDay()
        {
            if (DateTime.UtcNow.Year == NextPaymentDay.Year &&
                DateTime.UtcNow.Month == NextPaymentDay.Month &&
                DateTime.UtcNow.Day == NextPaymentDay.Day)
            {
                Debug.WriteLine("is a payment day!");
                TurnMonth();
            }
            else Debug.WriteLine($"not yet! at least {(int)(((NextPaymentDay - DateTime.UtcNow).Days))} days");
        }

        public async void DateComparer() =>
            await Task.Run(() => ComprareDateToNextPaymentDay());


        /// <summary>
        /// Возвращает информацию о 
        /// депозите в строковом формате
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{(typeof(BankDepositAccount).ToString())} " +
                $"{AccountAmount}$ " +
                $"ID - {ID} " +
                $"{ActivationDate} " +
                $"{Percent}%";
        }

        #endregion
    }
}
