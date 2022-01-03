using System.Windows;
using System.Windows.Controls;

namespace Homework_13.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    class PasswordHelper
    {
        /// <summary>
        /// Статический конструктор
        /// </summary>
        static PasswordHelper()
        {
            PasswordProperty =
                DependencyProperty.RegisterAttached(
                    "Password",
                    typeof(string),
                    typeof(PasswordHelper),
                    new FrameworkPropertyMetadata("", OnPasswordPropertyChanged));

            IsUpdatingProperty =
                DependencyProperty.RegisterAttached(
                    "IsUpdating",
                    typeof(bool),
                    typeof(PasswordHelper));

            AttachProperty =
                DependencyProperty.RegisterAttached(
                    "Attach",
                    typeof(bool),
                    typeof(PasswordHelper),
                    new PropertyMetadata(false, Attach));
        }

        #region Поля

        /// <summary>
        /// Пароль
        /// </summary>
        public static readonly DependencyProperty PasswordProperty;
        /// <summary>
        /// Обновление пароля
        /// </summary>
        public static readonly DependencyProperty IsUpdatingProperty;
        public static readonly DependencyProperty AttachProperty;

        #endregion

        #region Методы

        private static void OnPasswordPropertyChanged(
            DependencyObject d, 
            DependencyPropertyChangedEventArgs e)
        {
            PasswordBox PassField = d as PasswordBox;

            PassField.PasswordChanged -= PasswordChanged;

            if (!GetIsUpdating(PassField))
            {
                PassField.Password = e.NewValue.ToString();
            }

            PassField.PasswordChanged += PasswordChanged;
        }

        /// <summary>
        /// Уведомляет об изменении текста в PasswordBox и назначает этот текст свойству Password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox PassField = sender as PasswordBox;
            SetIsUpdating(PassField, true);
            SetPassword(PassField, PassField.Password);
            SetIsUpdating(PassField, false);
        }

        /// <summary>
        /// Сеттер для свойства Password
        /// </summary>
        /// <param name="passwordBox"></param>
        /// <param name="password"></param>
        public static void SetPassword(PasswordBox passwordBox, string password)
        {
            passwordBox.SetValue(PasswordProperty, password);
        }

        /// <summary>
        /// Сеттер для свойства IsUpdatingProperty, уведомляет об обновлении объекта
        /// </summary>
        /// <param name="passwordBox"></param>
        /// <param name="v"></param>
        public static void SetIsUpdating(PasswordBox passwordBox, bool v) =>
            passwordBox.SetValue(IsUpdatingProperty, v);

        /// <summary>
        /// Геттер для свойства IsUpdatingProperty, проверяет обновляется ли объект
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static bool GetIsUpdating(DependencyObject d) => 
            (bool)d.GetValue(IsUpdatingProperty);

        public static void Attach(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox PassField = d as PasswordBox;

            if (PassField == null)
                return;

            if ((bool)e.OldValue)
            {
                PassField.PasswordChanged -= PasswordChanged;
            }

            if ((bool)e.NewValue)
            {
                PassField.PasswordChanged += PasswordChanged;
            }
        }

        /// <summary>
        /// Сеттер для свойства AtachProperty
        /// </summary>
        /// <param name="d"></param>
        /// <param name="v"></param>
        public static void SetAttach(DependencyObject d, bool v) => 
            d.SetValue(AttachProperty, v);
        
        /// <summary>
        /// Геттер для свойства AtachProperty
        /// </summary>
        /// <param name="d"></param>
        public static void GetAttach(DependencyObject d) => 
            d.GetValue(AttachProperty);

        #endregion
    }
}
