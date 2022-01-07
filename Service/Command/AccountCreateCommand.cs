using System;
using System.Diagnostics;
using System.Windows;
using Homework_13.Model.bankModel;
using Homework_13.View.Windows;
using Homework_13.ViewModel;
using static Homework_13.Service.Command.AccountOpener;

namespace Homework_13.Service.Command
{
    class AccountCreateCommand : Command
    {
        /// <summary>
        /// процент
        /// </summary>
        double percent;
        /// <summary>
        /// сумма
        /// </summary>
        long amount;
        /// <summary>
        /// срок
        /// </summary>
        int expiration;

        public override bool CanExecute(object parameter) =>
            parameter as AccountOpening != null;

        /// <summary>
        /// выполняет парсинг
        /// </summary>
        /// <typeparam name="T">параметр соответствующего типа для суммы, процента и срока</typeparam>
        /// <param name="Text">поле, содержащее значение подлежащее парсингу</param>
        /// <returns></returns>
        private bool ParameterParsing<T>(string Text)
        {
            if (Text != string.Empty)
            {
                if ((typeof(T)) == typeof(double))
                {
                    if (double.TryParse(Text, out double tmp))
                    {
                        percent = tmp;
                        if (percent < CreditPercentSetter.minPercent)
                        {
                            percent = CreditPercentSetter.minPercent;
                            MessageBox.Show(
                            "Выбран процент ниже минимального, установлено минимальное значение",
                            "Ставка меньше минимальной!",
                            MessageBoxButton.OK);
                        }
                        return true;
                    }
                    else
                    {
                        percent = AccountOpeningViewModel.PersonalPercent;
                        MessageBox.Show(
                        "Для поля процент установлено расчётное значение", 
                        "Отсутствует значение",
                        MessageBoxButton.OK);
                        return true;
                    }
                }
                else if ((typeof(T)) == typeof(long))
                {
                    if (long.TryParse(Text, out long tmp))
                    {
                        amount = tmp;
                        return true;
                    }
                    else
                    {
                        if (!AccountOpeningViewModel.Deposit) { amount = 0; return true; }
                        else
                            MessageBox.Show(
                            "Введите сумму кредита!", "Отсутствует значение",
                            MessageBoxButton.OK);
                    }
                }
                else if ((typeof(T)) == typeof(int))
                {
                    if (int.TryParse(Text, out int tmp)) 
                    { 
                        expiration = tmp; 
                        if (expiration < BankCreditAccount.minExpiration)
                        {
                            expiration = BankCreditAccount.minExpiration;
                            MessageBox.Show(
                            "Срок кредита меньше минимального, установлено минимальное значение.", 
                            "Срок меньше минимального!",
                            MessageBoxButton.OK);
                        }
                        return true; 
                    }
                    else
                        MessageBox.Show(
                        "Невозможно распознать значение в поле 'срок'", 
                        "Отсутствует значение",
                        MessageBoxButton.OK);
                }
            }
            else 
                MessageBox.Show(
                    "Заполнены не все поля!", "Ошибка",
                    MessageBoxButton.OK);
            return false;
        }
        public override void Execute(object parameter)
        {
            AccountOpening window = parameter as AccountOpening;

            /// Открывает депозитный счёт
            if (AccountOpeningViewModel.Deposit)
            {
                if (ParameterParsing<double>(window.depPercent.Text))
                {
                    if (ParameterParsing<long>(window.startDep.Text))
                    {
                        if (ParameterParsing<int>(window.depExp.Text))
                        {
                            BankDepositAccount a = new(SelectedClient, amount, percent, expiration);
                            string info = $"Открыт депозитный счёт " +
                                $"{amount}$ " +
                                $"{percent}% " +
                                $"на срок: {expiration}, " +
                                $"для клиента {SelectedClient}";
                            MessageBox.Show(
                                info, "Депозитный счёт успешно открыт!",
                                MessageBoxButton.OK);
                            //SelectedClient = null;
                            window.Close();
                        }
                    }
                }
            }
            /// открывает кредитный счёт
            else
            {
                if (ParameterParsing<double>(window.credPercent.Text))
                {
                    if (ParameterParsing<long>(window.credAmount.Text))
                    {
                        if (ParameterParsing<int>(window.credExp.Text))
                        {
                            BankCreditAccount a = new(SelectedClient, percent, amount, expiration);
                            string info = $"Выдан кредит на " +
                                $"{amount}$ " +
                                $"под {percent}% " +
                                $"на срок: {expiration}, " +
                                $"для клиента {SelectedClient}";
                            MessageBox.Show(
                                info, "Кредит успешно выдан!",
                                MessageBoxButton.OK);
                            //SelectedClient = null;
                            window.Close();
                        }
                    }
                }
            }
        }
    }
}
