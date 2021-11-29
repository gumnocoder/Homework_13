namespace Homework_13.Service.Interfaces
{
    interface IInformationDialogService
    {
        void ShowError(string Message);

        void ShowInformation(string Message, string Tittle);
    }
}
