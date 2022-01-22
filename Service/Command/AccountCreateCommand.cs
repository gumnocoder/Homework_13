using System.Windows;
using Homework_13.Model.bankModel;
using Homework_13.View.Windows;
using Homework_13.ViewModel;
using static Homework_13.ViewModel.ClientListViewModel;
using static Homework_13.Service.InformationDialogService;
using static Homework_13.Service.Command.AccountOpener;

namespace Homework_13.Service.Command
{
    /// <summary>
    /// Открывает счёт
    /// </summary>
    class AccountCreateCommand : Command
    {
        #region Поля
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
        #endregion

        #region Методы

        public override bool CanExecute(object parameter)
        {
            return parameter as AccountOpening != null;
        }
            

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
                        if (AccountOpeningViewModel.Deposit)
                        {
                            if (percent > DepositPercentSetter.maxPercent)
                            {
                                percent = DepositPercentSetter.maxPercent;
                                ShowError($"Выбран процент больше максимального! " +
                                    $"Установлено максимально допустимое значение.");
                            }
                        }
                        else if (!AccountOpeningViewModel.Deposit)
                        {
                            if (percent < CreditPercentSetter.minPercent)
                            {
                                percent = CreditPercentSetter.minPercent;
                                ShowError($"Выбран процент ниже минимального, " +
                                    $"установлено минимальное значение");
                            }
                        }
                        return true;
                    }
                    else
                    {
                        percent = AccountOpeningViewModel.PersonalPercent;
                        ShowError("Для поля процент установлено расчётное значение");
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
                        else ShowError("Введите сумму кредита!");
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
                            ShowError($"Срок кредита меньше минимального, " +
                                $"установлено минимальное значение.");
                        }
                        return true; 
                    }
                    else ShowError("Невозможно распознать значение в поле 'срок'");
                }
            }
            else ShowError("Заполнены не все поля!");
            return false;
        }
        public override void Execute(object parameter)
        {
            AccountOpening window = parameter as AccountOpening;
            EventAction += HudViewer.ShowHudWindow;
            /// Открывает депозитный счёт
            if (AccountOpeningViewModel.Deposit && !SelectedClient.DepositIsActive)
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
                            ShowInformation("Депозитный счёт успешно открыт!", "Успешно");
                            OnEventAction($"Открыт {a.ToString()}");
                            window.Close();
                        }
                    }
                }
            }
            /// открывает кредитный счёт
            else if (!AccountOpeningViewModel.Deposit && !SelectedClient.CreditIsActive)
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
                            ShowInformation("Кредит успешно выдан!", "Успешно");
                            OnEventAction($"Открыт {a.ToString()}");
                            window.Close();
                        }
                    }
                }
            }
        }

        #endregion
    }
}
