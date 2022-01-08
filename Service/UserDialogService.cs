using System.Linq;
using System.Windows;
using Homework_13.Model;
using Homework_13.Service.Interfaces;
using Homework_13.View;
using Homework_13.View.UserControls;
using Homework_13.View.Windows;
using Homework_13.ViewModel;

namespace Homework_13.Service
{
    /// <summary>
    /// открывает запрашиваемые окна в режиме диалога
    /// </summary>
    class UserDialogService : IUserDialogService, IInformationDialogService
    {
        /// <summary>
        /// родительское окно
        /// </summary>
        private static Window _owner = 
            Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);

        /// <summary>
        /// Диалоговое окно для подтверждения операции
        /// </summary>
        /// <param name="Message">Текст описывающий операцию</param>
        /// <param name="Tittle">Заголовок</param>
        /// <param name="Choice">Выбор</param>
        /// <returns></returns>
        public bool Confirm(string Message, string Tittle, bool Choice = false) =>
            MessageBox.Show(Message, Tittle, MessageBoxButton.YesNo) == MessageBoxResult.Yes;

        /// <summary>
        /// Запускает методы для выбранного класса,
        /// индивидуально для Client или User
        /// в противном случае запускает метод содержащий инструкции 
        /// для других классов в качестве параметра
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public bool Edit(object o)
        {
            switch (o)
            {
                case Client client:
                    return EditSelectedClient(client);
                case User user:
                    return LoginFormOpen(user);
                case object window:
                    return GenericWindowOpenerMethod(window);
            }
            return true;
        }

        /// <summary>
        /// метод содержащий инструкции которые выполняются 
        /// в случае если в качестве параметра к методу edit 
        /// были переданы не классы User или Client
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private static bool GenericWindowOpenerMethod(object o)
        {
            var dlg = new Window();

            switch (o)
            {
                case ParameterChangingInputVM:
                    dlg = new ParameterChangingInput();
                    break;
                /// создание пользователя
                case UserCreationFormViewModel:
                    dlg = new UserCreationForm();
                    break;
                /// создание клиета
                case ClientCreationFormViewModel:
                    dlg = new ClientCreationForm();
                    break;
                /// вывод списка пользователей
                case UserListViewModel:
                    dlg = new UserListView();
                    break;
                /// создание вывод списка клиентов
                case ClientListViewModel:
                    dlg = new ClientListView();
                    break;
                case ClientInformationViewModel:
                    dlg = new ClientInfromation();
                    break;
            }

            dlg.Owner = _owner;
            dlg.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            if (dlg.ShowDialog() != true) return false;
            return true;
        }

        /// <summary>
        /// зарускает окно редактирования пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool EditUser(User user)
        {
            UserEditingForm dlg = new();
            dlg.DataContext = new UserEditingFormViewModel(user);
            dlg.Owner = _owner;
            dlg.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            if (dlg.ShowDialog() != true) return false;

            return true;
        }

        /// <summary>
        /// запускает окно редактирования клиента
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        private static bool EditSelectedClient(Client client)
        {
            ClientEditingForm dlg = new();
            dlg.DataContext = new ClientEditingFormViewModel(client);
            dlg.Owner = _owner;
            dlg.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            if (dlg.ShowDialog() != true) return false;

            return true;
        }
        
        /// <summary>
        /// вывод сообщение об ошибке
        /// </summary>
        /// <param name="Message">текст</param>
        public void ShowError(string Message)
        {
            MessageBox.Show(
                Message, 
                "Ошибка!",
                MessageBoxButton.OK);
        }

        /// <summary>
        /// показывает оповещение
        /// </summary>
        /// <param name="Message">текст</param>
        /// <param name="Tittle">заголовок</param>
        public void ShowInformation(string Message, string Tittle)
        {
            MessageBox.Show(
                Message,
                Tittle,
                MessageBoxButton.OK);
        }

        /// <summary>
        /// открывает форму авторизации
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        private static bool LoginFormOpen(User u)
        {
            var dlg = new LoginFormWindow();

            dlg.Owner = _owner;
            dlg.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            if (dlg.ShowDialog() != true) return false;
            return true;
        }
    }
}
