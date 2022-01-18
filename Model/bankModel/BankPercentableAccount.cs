using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Homework_13.Model.bankModel.interfaces;

namespace Homework_13.Model.bankModel
{
    /// <summary>
    /// Базовая логика для процентных счетов
    /// </summary>
    abstract class BankPercentableAccount : BankAccount, IExpiring, IPercentContainer
    {
        private DateTime _nextPaymentDay, _activationDate;
        private int _expiration;
        private double _percent;

        /// <summary>
        /// Дата окончания расчётного периода
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
        /// Истечение срока договора
        /// </summary>
        public int Expiration 
        {
            get =>  _expiration;
            set 
            {
                _expiration = value; 
                OnPropertyChanged(); 
            }
        }

        /// <summary>
        /// Дата активации счёта
        /// </summary>
        public DateTime ActivationDate
        {
            get => _activationDate;
            set
            {
                _activationDate = value; 
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

        public override void AddLinkToAccountInBank() { }

        /// <summary>
        /// выполняет вычисление даты окончания расчетного периода
        /// </summary>
        public virtual void CalculateNextPaymentDay()
        {
            NextPaymentDay = NextPaymentDay.AddDays(
                DateTime.DaysInMonth(
                    NextPaymentDay.Year,
                    NextPaymentDay.Month));
        }

        /// <summary>
        /// Выполняет проверку на окончание расчётного периода
        /// в случае подтверждения окончания периода запускает 
        /// сценарий плановых ежемесячных процедур
        /// </summary>
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

        /// <summary>
        /// Асинхронно запускает группу методов выполняющих 
        /// плановые ежемесячные операции по депозитному счёту
        /// </summary>
        public async void DateComparer() =>
            await Task.Run(() => ComprareDateToNextPaymentDay());

        /// <summary>
        /// Начисляет проценты за прошедший месяц
        /// </summary>
        public void AddPercents() =>
            AccountAmount += (Convert.ToInt64(
                Math.Round((double)(AccountAmount * Percent / 100 / 12))));

        public virtual void TurnMonth() { }

        /// <summary>
        /// Проверяет истечение срока договора
        /// </summary>
        /// <returns></returns>
        public bool Expired() => Expiration == 0;

        public override void SetId() { }
    }
}
