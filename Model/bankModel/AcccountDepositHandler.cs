using System;
using System.Diagnostics;
using Homework_13.Model.Interfaces;

namespace Homework_13.Model.bankModel
{
    /// <summary>
    /// Класс выполняющий характерные функции для депозитных счетов
    /// в виде реализации паттерна "Команда"
    /// </summary>
    /// <typeparam name="T">Депозитный счет</typeparam>
    class AcccountDepositHandler<T> : ICommandAction
        where T : BankDepositAccount
    {
        #region Поля
        private readonly BankDepositAccount _account;
        private readonly Client _client;
        private bool _executed = false;
        private const long _minDepositExtension = 1000;
        #endregion

        #region Свойства
        public bool Executed
        {
            get => _executed;
            set => _executed = value;
        }
        #endregion

        #region Методы
        /// <summary>
        /// вынести за пределы класса
        /// </summary>
        public long CalculatePercentage()
        {
            return
                Convert.ToInt64(
                    Math.Round(
                        (double)(_account.AccountAmount * _account.Percent / 100)));
        }
        public void SkipMonth()
        {
            --_client.ClientsDepositAccount.Expiration;
            _account.AccountAmount += CalculatePercentage();
            Debug.WriteLine($"" +
                $"Month turned, {_client}\n" +
                $"has added {CalculatePercentage()}\n" +
                $"on deposit account\n" +
                $"account condition {_account}\n" +
                $"expires: {_account.Expiration}");
        }

        void OnExpired()
        {
            if (_account.Expiration == 0)
            {
                _account.AccountAmount = 0;
                _account.IsActive = false;
                new ReputationIncreaser(_client, 5).Execute();
                Execute();
            }
        }
        void DepositExtension(long ExtensionAmount)
        {
            if (ExtensionAmount > _minDepositExtension)
            {
                _account.AccountAmount += ExtensionAmount;
                new ReputationIncreaser(_client);
            }
        }

        public void Execute()
        {
            Executed = true;
        }
        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="client">Клиент-владелец счета</param>
        /// <param name="account">счет</param>
        public AcccountDepositHandler(Client client, T account)
        {
            _account = account;
            _client = client;
        }
    }
}
