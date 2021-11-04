namespace Homework_13.Model.bankModel
{
    public sealed class Bank
    {
        #region Поля
        public long currentCreditID;

        public long currentDebitID;

        public long currentDepositID;

        public long currentClientID;

        public long currentUserID;
        #endregion

        #region Свойства
        public long CurrentCreditID
        { get => currentCreditID; set => currentCreditID = value; }


        public long CurrentDebitID
        { get => currentDebitID; set => currentDebitID = value; }

        public long CurrentDepositID
        { get => currentDepositID; set => currentDepositID = value; }

        public long CurrentClientID
        { get => currentClientID; set => currentClientID = value; }

        public long CurrentUserID
        { get => currentUserID; set => currentUserID = value; }
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
        static Bank thisBank = null;

        /// <summary>
        /// свойство возращающее экземпляр или 
        /// создающее его при отсутствии
        /// </summary>
        public static Bank ThisBank
        {
            get
            {
                if (thisBank == null) thisBank = new();
                return thisBank;
            }
        }
        #endregion
    }
}
