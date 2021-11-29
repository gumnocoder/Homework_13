namespace Homework_13.Service.Interfaces
{
    interface IUserDialogService
    {
        bool Edit(object o);

        bool Confirm(string Message, string Tittle, bool Choice = false);
    }
}
