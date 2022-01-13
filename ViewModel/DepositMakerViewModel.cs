namespace Homework_13.ViewModel
{
    /// <summary>
    /// Логика взаимодействия для DepositMakerView
    /// </summary>
    class DepositMakerViewModel
    {
        private int _amount = 0;

        /// <summary>
        /// Вносимая сумма
        /// </summary>
        public int Amount 
        {
            get => _amount;
            set => _amount = value;
        }
    }
}
