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
                AccountAmount += DebitStartAmount;
                new ReputationIncreaser(client);
                SetId();
                client.DebitIsActive = true;
                client.ClientsDebitAccount = this;
            }
        }
    }
}
