using System;
using Homework_13.Model.bankModel.interfaces;
using static Homework_13.Model.bankModel.Bank;

namespace Homework_13.Model.bankModel
{
    class BankDepositAccount : BankAccount, IPercentContainer, IExpiring
    {
        #region Конструкторы
        public BankDepositAccount(Client client, long DepositStartAmount)
        {
            if (!client.DepositIsActive)
            {
                new ReputationIncreaser(client, 3);
                AccountAmount += DepositStartAmount;
                SetId();
                client.DepositIsActive = true;
                client.ClientsDepositAccount = this;
                Percent = SetPercent(client);
                _activationDate = DateTime.Now;
            }
        }

        public BankDepositAccount() { }
        #endregion

        #region Поля
        private double _percent;
        private int _expiration;
        private const double  _maxPercent = 12;
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
            set { if (value <= _maxPercent) _percent = value;
                else _percent = 5; 
            } 
        }
        /// <summary>
        /// Срок истечения вклада
        /// </summary>
        public int Expiration 
        { 
            get => _expiration; 
            set 
            {
                if (value < _minExpiration) _expiration = 12;
                else
                {
                    if (_expiration == 0) { _expiration = 0; return; }
                    _expiration = value;
                }
            }
        }
        /// <summary>
        /// Дата открытия счёта
        /// </summary>
        public DateTime ActivationDate { get => _activationDate; }

        /// <summary>
        /// Остаток единиц исчисления до конца срока истечения вклада 
        /// </summary>
        public int ExpirationDuration { get => GetTotalMonthsCount(); }

        #endregion

        #region Методы
        /// <summary>
        /// Назначает ID
        /// </summary>
        public override void SetId() => ID = ++ThisBank.CurrentDepositID;

        /// <summary>
        /// Проверяет истёк ли срок вклада
        /// </summary>
        /// <returns></returns>
        public bool Expired() => GetTotalMonthsCount() == 0;

        /// <summary>
        /// Назначает процент по вкладу
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public double SetPercent(Client client)
        {
            if (client.Reputation == 10) return _maxPercent;
            double clientReputation = (double)client.Reputation;
            return _maxPercent / 10.0 * clientReputation;
        }

        /// <summary>
        /// Возвращает остаток дней до истечения срока вклада
        /// </summary>
        /// <returns></returns>
        public int GetTotalMonthsCount()
        {
            DateTime now = DateTime.Now;
            DateTime start = ActivationDate;

            return (int)(((now.Year - start.Year) * 12) +
                now.Month - start.Month +
                (start.Day >= now.Day - 1 ? 0 : -1) +
                ((start.Day == 1 && DateTime.DaysInMonth(now.Year, now.Month) == now.Day) ? 1 : 0));
        }
        #endregion
    }
}
