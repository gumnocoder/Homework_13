using System;
using System.Diagnostics;
using Homework_13.Model.bankModel.interfaces;
using static Homework_13.Model.bankModel.Bank;

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
        private const int _minExpiration = 12;
        private DateTime _activationDate;
        #endregion

        #region Свойства
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

        /// <summary>
        /// Остаток единиц исчисления до конца 
        /// срока истечения вклада 
        /// </summary>
        public int ExpirationDuration 
        { get => GetTotalMonthsCount(); }

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
        public bool Expired() => GetTotalMonthsCount() == 0;


        /// <summary>
        /// Возвращает остаток дней до истечения срока вклада
        /// </summary>
        /// <returns></returns>
        public int GetTotalMonthsCount()
        {
            DateTime now = DateTime.UtcNow;
            DateTime start = ActivationDate;

            return (int)(((now.Year - start.Year) * 12) +
                now.Month - start.Month +
                (start.Day >= now.Day - 1 ? 0 : -1) +
                ((start.Day == 1 && DateTime.DaysInMonth(now.Year, now.Month) == now.Day) ? 1 : 0));
        }

        private DateTime _nextPaymentDay;

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
