using System;
using System.Diagnostics;
using Homework_13.ViewModel;

namespace Homework_13.Model.bankModel
{
    public abstract class BankAccount : BaseViewModel
    {
        #region Поля

        long _id, _accountAmount = 0;
        bool _isActive = true;

        #endregion

        #region Свойства

        /// <summary>
        /// ID счета
        /// </summary>
        public long ID
        { get => _id; set => _id = value; }

        /// <summary>
        /// Активность счета
        /// </summary>
        public bool IsActive
        { get => _isActive; set => _isActive = value; }

        /// <summary>
        /// Баланс счета
        /// </summary>
        public long AccountAmount
        { 
            get => _accountAmount;
            set
            {
                _accountAmount = value;
                OnPropertyChanged();
            }
         }

        #endregion

            #region Методы

            /// <summary>
            /// назначает ID
            /// </summary>
        public abstract void SetId();

        public abstract void AddLinkToAccountInBank();

        /// <summary>
        /// Добавить положительную сумму на баланс
        /// </summary>
        /// <param name="value"></param>
        public void AddMoneyToAccount(long value)
        {
            if (value > 0)
            {
                AccountAmount += value;
                Debug.WriteLine($"клиент внёс {value}$ на счёт. Текущее состояние счёта: {this}");
            }
            else
            {
                Debug.WriteLine($"На счету {this} недостаточно средств!");
            }
        }

        public void RemoveMoneyFromAccount(long value)
        {
            if (value <= AccountAmount)
            {
                AccountAmount -= value;
                Debug.WriteLine($"клиент снял {value}$ со счёта. Текущее состояние счёта: {this}");
            }
        }
        public override string ToString()
        {
            return $"Тип: {GetType()}, " +
                $" ID - {_id}, " +
                $"Состояние - {AccountAmount}$";
        }
        #endregion
    }
}
