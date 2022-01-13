using System;
using System.Windows;
using Homework_13.Service.Interfaces;

namespace Homework_13.Service
{
    /// <summary>
    /// Класс отвечающий за показ уведомлений
    /// </summary>
    class InformationDialogService
    {
        /// <summary>
        /// Выводит сообщение об ошибке
        /// </summary>
        /// <param name="Message"></param>
        public static void ShowError(string Message)
        {
            MessageBox.Show(
                Message,
                "Ошибка!",
                MessageBoxButton.OK);
        }

        /// <summary>
        /// Выводит информационное сообщение
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="Tittle"></param>
        public static void ShowInformation(string Message, string Tittle)
        {
            MessageBox.Show(
                Message,
                Tittle,
                MessageBoxButton.OK);
        }
    }
}
