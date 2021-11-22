﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Homework_13.View.Windows;
using Homework_13.ViewModel;

namespace Homework_13.Service.Command
{
    class PersonsEditor : Command
    {
        //public bool? DialogResult { get; set; }

        public override bool CanExecute(object parameter)
        {
            if (parameter is ClientEditingForm) return true;
            return false;
        }

        public override void Execute(object parameter)
        {
            var window = (Window)parameter;
            //window.DialogResult = DialogResult;
            ClientEditingForm dlg = parameter as ClientEditingForm;
            /// в случае успешной авторизации закрывает окно
            if (CanExecute(parameter))
            {
                if (dlg.NameField.Text != string.Empty)
                    ClientListViewModel.SelectedClient.Name = dlg.NameField.Text;
                if (dlg.TypesList.Text != string.Empty)
                    ClientListViewModel.SelectedClient.Type = dlg.TypesList.Text;

                window.Close();
            }
        }
    }
}
