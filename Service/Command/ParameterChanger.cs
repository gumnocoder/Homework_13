﻿using System;
using System.Windows;
using Homework_13.Model;
using Homework_13.Model.bankModel;
using Homework_13.View.Windows;
using Homework_13.ViewModel;
using static Homework_13.ViewModel.ParameterChangingInputVM;

namespace Homework_13.Service.Command
{
    class ParameterChanger : Command
    {
        public bool Increase { get; set; } = false;
        public bool Decrease { get; set; } = false;

        private int _number;
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
                    MessageBox.Show(
                        "Введено недопустимое число! Число должно быть в диапазоне от 1 до 10",
                        "Недопустимое значение",
                        MessageBoxButton.OK);
                    return false;
                }
            }
            else
            {
                MessageBox.Show(
                    "Введено недопустимое значение! Введите число в диапазоне от 1 до 10",
                    "Недопустимое значение",
                    MessageBoxButton.OK);
                return false;
            }
        }
        public override bool CanExecute(object parameter) => 
            (parameter as Window) != null;

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter)) return;
            if (ClientListViewModel.SelectedClient == null)
            {
                MessageBox.Show(
                    "Клиент не выбран!",
                    "Отсутствие данных",
                    MessageBoxButton.OK);
                return;
            }

            Client client = ClientListViewModel.SelectedClient;
            ParameterChangingInput window = parameter as ParameterChangingInput;

            if (Check(window.NumberField.Text))
            {
                if (incr) { ReputationIncreaser incr = new(client, _number); incr.Execute(); }
                incr = false;

                if (decr) { new ReputationDecreaser(client, _number).Execute();/* ReputationDecreaser decr = new(client, _number); decr.Execute();*/ }
                decr = false;

                window.Close();
            }
        }
    }
}
