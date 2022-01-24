using System.Windows;
using BankModelLibrary;
using BankModelLibrary.BankServices;
using Homework_13.View.Windows;
using Homework_13.ViewModel;
using static Homework_13.ViewModel.ParameterChangingInputVM;


namespace Homework_13.Service.Command
{
    class ParameterChanger : Command
    {
        /// <summary>
        /// Флаг повышения параметра
        /// </summary>
        public bool Increase { get; set; } = false;
        /// <summary>
        /// Флаг понижения параметра
        /// </summary>
        public bool Decrease { get; set; } = false;
        /// <summary>
        /// число для оперирования
        /// </summary>
        private int _number;

        /// <summary>
        /// Выполняет парсинг
        /// </summary>
        /// <param name="input">текст для парсинга</param>
        /// <returns></returns>
        private bool Check(string input)
        {
            if (int.TryParse(input, out int tmp))
            {
                if (tmp >= 1 & tmp <= 10)
                {
                    _number = tmp;
                    return true;
                }
                else
                {
                    //ShowError("");
                    OnEventAction($"Введено недопустимое число! " +
                        $"Число должно быть в диапазоне от 1 до 10", false, true);
                    OnHistoryEventAction($"Попытка изменения репутации " +
                        $"на недопустимое число ({tmp})");

                    return false;
                }
            }
            else
            {
                //ShowError("Введено недопустимое значение! Введите число в диапазоне от 1 до 10");
                OnEventAction($"Введено недопустимое значение! " +
                    $"Введите число в диапазоне от 1 до 10", false, true);
                OnHistoryEventAction($"Попытка изменения репутации, обнаружено " +
                    $"несоответствие типов, переданное значение : {input}");

                return false;
            }
        }
        public override bool CanExecute(object parameter) => 
            (parameter as Window) != null;

        /// <summary>
        /// Инициализирует проверки и выполняет изменение 
        /// параметра, в соответствии с выбранным флагом
        /// </summary>
        /// <param name="parameter">окно ParameterChangingInput</param>
        public override void Execute(object parameter)
        {
            EventAction += HudViewer.ShowHudWindow;
            HistoryEventAction += LogWriter.WriteToLog;

            if (!CanExecute(parameter)) return;
            if (ClientListViewModel.SelectedClient == null)
            {
                //ShowError("Клиент не выбран");
                OnEventAction($"Клиент не выбран", false, true);
                OnHistoryEventAction($"Попытка изменения репутации " +
                    $"без указания конечного клиента");

                return;
            }

            Client client = ClientListViewModel.SelectedClient;
            ParameterChangingInput window = 
                parameter as ParameterChangingInput;

            if (Check(window.NumberField.Text))
            {
                if (incr) 
                { 
                    new ReputationIncreaser(client, _number).Execute();
                    OnEventAction($"Репутация клиента {client.Name} повышена", true, false);
                    OnHistoryEventAction($"Репутация клиента {client} повышена на {_number} пункт(ов)");
                }
                incr = false;

                if (decr)
                {
                    new ReputationDecreaser(client, _number).Execute();
                    OnEventAction($"Репутация клиента {client.Name} понижена", true, false);
                    OnHistoryEventAction($"Репутация клиента {client} понижена на {_number} пункт(ов)");
                }
                decr = false;

                window.Close();
            }
        }
    }
}
