using System.Diagnostics;
using static Homework_13.Model.bankModel.Bank;

namespace Homework_13.Model.bankModel
{
    /// <summary>
    /// Дебетовый банковский счёт
    /// </summary>
    class BankDebitAccount : BankAccount
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="client">Клиент-владеле счёта</param>
        /// <param name="DebitStartAmount">Сумма вносимая при открытии счёта, 0 по умолчанию</param>
        public BankDebitAccount(Client client, long DebitStartAmount = 0)
        {
            if (!client.DebitIsActive)
            {
                if (DebitStartAmount > 100) new ReputationIncreaser(client);
                AccountAmount += DebitStartAmount;
                SetId();
                client.DebitIsActive = true;
                AddLinkToAccountInBank();
                client.DebitAccountID = ID;
                client.ClientsDebitAccount =
                    (BankDebitAccount)client.ba<BankDebitAccount>(
                        ref ThisBank.debits,
                        client.DebitAccountID);
                Debug.WriteLine($"{this}");
            }
        }

        #region
        /// <summary>
        /// Назначает идентификатор
        /// </summary>
        public override void SetId() =>
            ID = ++ThisBank.CurrentDebitID;

        /// <summary>
        /// Добавляет экземпляр в коллекцию Debits
        /// </summary>
        public override void AddLinkToAccountInBank()
        {
            ThisBank.Debits.Add(this);
            Debug.WriteLine($"{this} added to bank");
        }

        /// <summary>
        /// Выводит информацию
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{(typeof(BankDebitAccount).ToString())} {AccountAmount}$ ID - {ID}";
        }
        #endregion
    }
}
