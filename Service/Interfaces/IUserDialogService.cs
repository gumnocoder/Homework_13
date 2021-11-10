namespace Homework_13.Service.Interfaces
{
    interface IUserDialogService
    {
        bool Edit(object o);

        void ShowError(string Message, string Tiitle);

        void ShowInformation(string Message, string Tiitle);

        bool Confirm(string Message, string Tittle, bool Choice = false);
    }
}
