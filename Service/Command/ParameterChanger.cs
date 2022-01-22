using System.Windows;
using Homework_13.Model;
using Homework_13.Model.bankModel;
using Homework_13.View.Windows;
using Homework_13.ViewModel;
using static Homework_13.Service.InformationDialogService;
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
                    ShowError("Введено недопустимое число! Число должно быть в диапазоне от 1 до 10");
                    return false;
                }
            }
            else
            {
                ShowError("Введено недопустимое значение! Введите число в диапазоне от 1 до 10");
                return false;
            }
        }
        public override bool CanExecute(object parameter) => 
            (parameter as Window) != null;

        /// <summary>
        /// Инициализирует проверки и выполняет изменение параметра, в соответствии с выбранным флагом
        /// </summary>
        /// <param name="parameter">окно ParameterChangingInput</param>
        public override void Execute(object parameter)
        {
            EventAction += HudViewer.ShowHudWindow;
            if (!CanExecute(parameter)) return;
            if (ClientListViewModel.SelectedClient == null)
            {
                ShowError("Клиент не выбран");
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
                    OnEventAction($"репутация клиента {client} повышена на {_number} пункт(ов)", true, false);
                }
                incr = false;

                if (decr)
                {
                    new ReputationDecreaser(client, _number).Execute();
                    OnEventAction($"репутация клиента {client} понижена на {_number} пункт(ов)", true, false);
                }
                decr = false;

                window.Close();
            }
        }
    }
}
