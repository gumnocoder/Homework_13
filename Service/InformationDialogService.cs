using System;
using System.Windows;
using Homework_13.Service.Interfaces;

namespace Homework_13.Service
{
    class InformationDialogService
    {
        public static void ShowError(string Message)
        {
            MessageBox.Show(
                Message,
                "Ошибка!",
                MessageBoxButton.OK);
        }

        public static void ShowInformation(string Message, string Tittle)
        {
            MessageBox.Show(
                Message,
                Tittle,
                MessageBoxButton.OK);
        }
    }
}
