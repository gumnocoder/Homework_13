namespace Homework_13.Model.bankModel.interfaces
{
    interface IExpiring
    {
        int Expiration { get; set; }

        bool Expired();
    }
}
