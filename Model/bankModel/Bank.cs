using System.Collections.ObjectModel;

namespace Homework_13.Model.bankModel
{
    public sealed class Bank
    {
        #region Поля
        /// <summary>
        /// Идентификатор экземпляра
        /// </summary>
        public long currentCreditID, currentDebitID, currentDepositID, currentClientID, currentUserID;

        /// <summary>
        /// Коллекция всех счетов банка, соответствующего типа
        /// </summary>
        public ObservableCollection<BankAccount> debits, credits, deposits;

        #endregion

        #region Свойства
        /// <summary>
        /// 
        /// </summary>
        public long CurrentCreditID
        { get => currentCreditID; set => currentCreditID = value; }
        /// <summary>
        /// 
        /// </summary>
        public long CurrentDebitID
        { get => currentDebitID; set => currentDebitID = value; }
        /// <summary>
        /// 
        /// </summary>
        public long CurrentDepositID
        { get => currentDepositID; set => currentDepositID = value; }
        /// <summary>
        /// 
        /// </summary>
        public long CurrentClientID
        { get => currentClientID; set => currentClientID = value; }
        /// <summary>
        /// 
        /// </summary>
        public long CurrentUserID
        { get => currentUserID; set => currentUserID = value; }
        /// <summary>
        /// Коллекция дебетовых счетов
        /// </summary>
        public ObservableCollection<BankAccount> Debits
        {
            get
            {
                if (debits == null) { debits = new(); }
                return debits;
            }
            set => debits = value;
        }
        /// <summary>
        /// Коллекция кредитных счетов
        /// </summary>
        public ObservableCollection<BankAccount> Credits
        {
            get
            {
                if (credits == null) { credits = new(); }
                return credits;
            }
            set => credits = value;
        }
        /// <summary>
        /// Коллекция депозитных счетов
        /// </summary>
        public ObservableCollection<BankAccount> Deposits
        {
            get
            {
                if (deposits == null) { deposits = new(); }
                return deposits;
            }
            set => deposits = value;
        }
        #endregion

        #region Синглтон Bank
        public string Name { get; set; }

        /// <summary>
        /// конструктор по умолчанию
        /// </summary>
        private Bank() { Name = "ЗАО 'Банк России'"; }

        /// <summary>
        /// указатель наличия одного экземпляра
        /// </summary>
        static Bank _thisBank = null;

        /// <summary>
        /// свойство возращающее экземпляр или 
        /// создающее его при отсутствии
        /// </summary>
        public static Bank ThisBank
        {
            get
            {
                if (_thisBank == null) _thisBank = new();
                return _thisBank;
            }
        }
        #endregion
    }
}
