using static Homework_13.Model.bankModel.Bank;

namespace Homework_13.Model.bankModel
{
    class BankDebitAccount : BankAccount
    {
        public override void SetId() => ID = ++ThisBank.CurrentDebitID;

        public BankDebitAccount(Client client, long DebitStartAmount = 0)
        {
            if (!client.DebitIsActive)
            {
                if (DebitStartAmount > 100) new ReputationIncreaser(client);
                AccountAmount += DebitStartAmount;
                SetId();
                client.DebitIsActive = true;
                client.ClientsDebitAccount = this;
            }
        }
    }
}
