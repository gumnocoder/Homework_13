using System.Diagnostics;

namespace Homework_13.Model.bankModel
{
    public abstract class BankAccount
    {
        #region Поля

        long _id;
        bool _isActive = true;
        long _accountAmount = 0;

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
        { get => _accountAmount; set => _accountAmount = value; }

        #endregion

        #region Методы

        /// <summary>
        /// назначает ID
        /// </summary>
        public abstract void SetId();

        /// <summary>
        /// Добавить положительную сумму на баланс
        /// </summary>
        /// <param name="value"></param>
        public void AddMoneyToAccount(long value)
        {
            if (value > 0)
            {
                AccountAmount += value;
                Debug.WriteLine($"client add {value}$ to account. account condition: {this}");
            }
        }
        public override string ToString()
        {
            return $"type: {GetType()}, " +
                $" ID - {_id}, " +
                $"Amount - {AccountAmount}";
        }
        #endregion
    }
}
