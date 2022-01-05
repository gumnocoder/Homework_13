﻿using System;
using System.Diagnostics;
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
                AddLinkToAccountInBank();
                Percent = DepositPercentSetter.SetDepositPercent(client);
                _activationDate = DateTime.Now;
            }
        }

        public BankDepositAccount(Client client, long DepositStartAmount, double PersonalPercent)
        {
            new ReputationIncreaser(client, 3);
            AccountAmount += DepositStartAmount;
            SetId();
            client.DepositIsActive = true;
            client.ClientsDepositAccount = this;
            AddLinkToAccountInBank();
            Percent = PersonalPercent;
            _activationDate = DateTime.Now;
        }

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

        public override void AddLinkToAccountInBank()
        {
            ThisBank.Deposits.Add(this);
            Debug.WriteLine(this);
        }
        #endregion
    }
}
