using System.Windows;
using System.Windows.Controls;

namespace Homework_13.ViewModel
{
    class PasswordHelper
    {
        public static readonly
    DependencyProperty PasswordProperty =
    DependencyProperty.RegisterAttached("Password",
        typeof(string),
        typeof(PasswordHelper),
        new FrameworkPropertyMetadata(string.Empty, OnPasswordPropertyChanged));

        public static readonly
            DependencyProperty IsUpdatingProperty =
            DependencyProperty.RegisterAttached("IsUpdating",
                typeof(bool),
                typeof(PasswordHelper));

        public static readonly
            DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached("Attach",
                typeof(bool),
                typeof(PasswordHelper),
                new PropertyMetadata(false, Attach));

        private static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox PassField = d as PasswordBox;
            PassField.PasswordChanged -= PasswordChanged;
            if (!(bool)GetIsUpdating(PassField))
            {
                PassField.Password = (e.NewValue).ToString();
            }
            PassField.PasswordChanged += PasswordChanged;
        }

        public static bool GetIsUpdating(DependencyObject d)
        {
            return (bool)d.GetValue(IsUpdatingProperty);
        }

        public static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox PassField = sender as PasswordBox;
            SetIsUpdating(PassField, true);
            SetPassword(PassField, PassField.Password);
            SetIsUpdating(PassField, false);
        }

        public static void SetPassword(PasswordBox passwordBox, string password)
        {
            passwordBox.SetValue(PasswordProperty, password);
        }

        public static void SetIsUpdating(PasswordBox passwordBox, bool v)
        {
            passwordBox.SetValue(IsUpdatingProperty, v);
        }

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

        public static void SetAttach(DependencyObject d, bool v)
        {
            d.SetValue(AttachProperty, v);
        }

        public static void GetAttach(DependencyObject d)
        {
            d.GetValue(AttachProperty);
        }
    }
}
