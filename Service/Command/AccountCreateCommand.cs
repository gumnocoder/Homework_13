using Homework_13.View.Windows;
using Homework_13.ViewModel;
using static Homework_13.Service.InformationDialogService;
using BankModelLibrary;
using BankModelLibrary.BankServices;
using BankModelLibrary.BaseBankModels;
using static Homework_13.ViewModel.ClientListViewModel;

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
                        if (AccountOpeningViewModel.Deposit)
                        {
                            if (percent > DepositPercentSetter.maxPercent)
                            {
                                OnEventAction($"Выбран процент больше максимального! " +
                                    $"Установлено максимально допустимое значение", false, true);
                                OnHistoryEventAction($"Попытка открытия вклада под процент превышающий " +
                                    $"максимальный. Введенные данные {percent}%, " +
                                    $"установлено значение {DepositPercentSetter.maxPercent}");

                                percent = DepositPercentSetter.maxPercent;
                            }
                        }
                        else if (!AccountOpeningViewModel.Deposit)
                        {
                            if (percent < CreditPercentSetter.minPercent)
                            {
                                percent = CreditPercentSetter.minPercent;
                                ShowError($"Выбран процент ниже минимального, " +
                                    $"установлено минимальное значение");
                                OnEventAction($"Для поля процент установлено расчётное значение", false, true);
                                OnHistoryEventAction($"Для поля процент установлено расчётное значение");
                            }
                        }
                        return true;
                    }
                    else
                    {
                        percent = AccountOpeningViewModel.PersonalPercent;
                        //ShowError("Для поля процент установлено расчётное значение");
                        OnEventAction($"Для поля процент установлено расчётное значение", false, true);
                        OnHistoryEventAction($"Для поля процент установлено расчётное значение");
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
                        {
                            //ShowError("Введите сумму кредита!");
                            OnEventAction($"Введите сумму кредита", false, true);
                            OnHistoryEventAction($"Попытка выдачи кредита без указания суммы");
                        }
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
                            //ShowError($"Срок кредита меньше минимального, " +
                             //   $"установлено минимальное значение.");
                            OnEventAction($"Срок кредита меньше минимального " +
                                $"установлено минимальное значение", false, true);
                            OnHistoryEventAction($"Попытка выдачи кредита на срок меньше " +
                                $"минимального, установлено минимальное значение");
                        }
                        return true;
                    }
                    else
                    {
                        //ShowError("Невозможно распознать значение в поле 'срок'");
                        OnEventAction($"Невозможно распознать значение в поле 'срок'", false, true);
                        OnHistoryEventAction($"ошибка открытия счёта, отсутствие данных в поле срок");
                    }
                }
            }
            else
            {
                //ShowError("Заполнены не все поля!");
                OnEventAction($"Заполнены не все поля", false, true);
                OnHistoryEventAction($"ошибка открытия счёта, отсутствие данных в обязательных полях");
            }
            return false;
        }
        public override void Execute(object parameter)
        {
            AccountOpening window = parameter as AccountOpening;
            EventAction += HudViewer.ShowHudWindow;
            HistoryEventAction += LogWriter.WriteToLog;

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
                            //ShowInformation("Депозитный счёт успешно открыт!", "Успешно");

                            OnEventAction($"Депозитный счёт успешно открыт", true, false);
                            OnHistoryEventAction($"Открыт {a.ToString()}");

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

                            //ShowInformation("Кредит успешно выдан!", "Успешно");
                            OnEventAction($"Кредит успешно выдан!", true, false);
                            OnHistoryEventAction($"Открыт {a.ToString()}");

                            window.Close();
                        }
                    }
                }
            }
        }

        #endregion
    }
}
