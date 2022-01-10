namespace Homework_13.Service.Interfaces
{
    interface IUserDialogService
    {
        bool StartDialogScenario(object o);

        bool Confirm(string Message, string Tittle, bool Choice = false);
    }
}
