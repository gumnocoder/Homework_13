using System;
using System.Diagnostics;
using Homework_13.Model.Interfaces;
using static Homework_13.Model.bankModel.Bank;

namespace Homework_13.Model.bankModel
{
    /// <summary>
    /// Класс выполняющий характерные функции для депозитных счетов
    /// в виде реализации паттерна "Команда"
    /// </summary>
    /// <typeparam name="T">Депозитный счет</typeparam>
    class AcccountDepositHandler : ICommandAction
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="client">Клиент-владелец депозитного счета</param>
        /// <param name="account">счет</param>
        public AcccountDepositHandler(Client client, BankDepositAccount account)
        {
            _account = account;
            _client = client;
        }

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
        /// Выполняет расчёт процентов на сумму остатка
        /// </summary>
        public long CalculatePercentage()
        {
            return
                Convert.ToInt64(
                    Math.Round(
                        (double)(_account.AccountAmount * _account.Percent / 100)));
        }

        /// <summary>
        /// Выполняет пересчёт суммы при окончании месяца
        /// </summary>
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

        /// <summary>
        /// Выполнить при окончании срока вклада - 
        /// переводит накопления на дебетовый счёт 
        /// (создаёт при необходимости) и закрывает депозитный счёт
        /// </summary>
        void OnExpired()
        {
            if (_account.Expiration == 0)
            {
                if (!_client.DebitIsActive)  _client.DebitIsActive = true; 
                BankDebitAccount a = new(_client, _account.AccountAmount);

                _account.AccountAmount = 0;
                foreach (var e in ThisBank.Deposits)
                {
                    if (e == _account)
                    {
                        ThisBank.Deposits.Remove(e);
                        break;
                    }
                }
                _client.ClientsDepositAccount = null;
                _account.IsActive = false;
                new ReputationIncreaser(_client, 5).Execute();
                Execute();
            }
        }

        /// <summary>
        /// расширяет депозитный счёт
        /// </summary>
        /// <param name="ExtensionAmount"></param>
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
    }
}
