using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework_13.Model.bankModel.interfaces;

namespace Homework_13.Model.bankModel
{
    /// <summary>
    /// Базовая логика для процентных счетов
    /// </summary>
    abstract class BankPercentableAccount : BankAccount, IExpiring
    {
        private DateTime _nextPaymentDay, _activationDate;
        private int _expiration;


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

        public override void AddLinkToAccountInBank()
        {
            throw new NotImplementedException();
        }

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
        /// Проверяет истечение срока договора
        /// </summary>
        /// <returns></returns>
        public bool Expired() => Expiration == 0;

        public override void SetId()
        {
            throw new NotImplementedException();
        }
    }
}
