using System.Collections.ObjectModel;
using System.Diagnostics;
using Homework_13.Model.bankModel;
using static Homework_13.Model.bankModel.Bank;

namespace Homework_13.Model
{
    class Client : Person
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Name">Имя</param>
        /// <param name="Type">Тип</param>
        /// <param name="Reputation">Репутация</param>
        public Client(string Name, string Type = "Частный клиент", int Reputation = 6)
        {
            SetID();
            this.Name = Name;
            this.Type = Type;
            this.Reputation = Reputation;
        }

        #region Поля
        /// <summary>
        /// флаги активности счетов
        /// </summary>
        private bool creditIsActive, debitIsActive, depositIsActive, accountsFreezed = false;
        /// <summary>
        /// Идентификатор счетов
        /// </summary>
        public long CreditAccountID, DepositAccountID, DebitAccountID;
        private BankCreditAccount _clientsCreditAccount;
        private BankDebitAccount _clientsDebitAccount;
        private BankDepositAccount _clientsDepositAccount;
        private string _type;
        private int _reputation;
        private long _clientId;
        #endregion

        #region Свойства

        /// <summary>
        /// Флаг наличия кредита
        /// </summary>
        public bool CreditIsActive
        {
            get => creditIsActive;
            set
            {
                creditIsActive = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Флаг наличия дебетового счёта
        /// </summary>
        public bool DebitIsActive
        {
            get => debitIsActive;
            set
            {
                debitIsActive = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Флаг наличия депозитного счёта
        /// </summary>
        public bool DepositIsActive
        {
            get => depositIsActive;
            set
            {
                depositIsActive = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Активность заморозки счетов 
        /// </summary>
        public bool AccountsFreezed
        {
            get => accountsFreezed;
            set
            {
                accountsFreezed = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Тип клиента
        /// </summary>
        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Рейтинг клиента
        /// </summary>
        public int Reputation
        {
            get => _reputation;
            set
            {
                if (value < 0) _reputation = 0;
                else if (value > 10) _reputation = 10;
                else _reputation = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Идентификатор клиента
        /// </summary>
        public long ClientID
        {
            get => _clientId;
            protected set
            {
                _clientId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Возвращает ссылку на дебетовый счёт
        /// </summary>
        public BankDebitAccount ClientsDebitAccount
        {
            get => _clientsDebitAccount;
            set
            {
                _clientsDebitAccount =
                    (BankDebitAccount)ba<BankDebitAccount>(
                        ref ThisBank.debits,
                        DebitAccountID);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Возвращает ссылку на кредитный счёт
        /// </summary>
        public BankCreditAccount ClientsCreditAccount
        {
            get => _clientsCreditAccount;
            set
            {
                _clientsCreditAccount = 
                    (BankCreditAccount)ba<BankCreditAccount>(
                        ref ThisBank.credits, 
                        CreditAccountID);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Возвращает ссылку на депозитный счёт
        /// </summary>
        public BankDepositAccount ClientsDepositAccount
        {
            get => _clientsDepositAccount;
            set
            {
                _clientsDepositAccount = (BankDepositAccount)ba<BankDepositAccount>(
                        ref ThisBank.deposits,
                        DepositAccountID);
                Debug.WriteLine($" deposit expired at: {this.ClientsDepositAccount.Expiration} from client");
                OnPropertyChanged();
            }
        }
        #endregion

        #region Методы
        /// <summary>
        /// Выполняет поиск в ThisBank.[Credits или Deposits или Debits]
        /// по соответствующему идентификатору счёта Client SelectedClient
        /// и возращает требуемый тип-наследник BankAccount в случае успеха
        /// или null в противном случае
        /// </summary>
        /// <typeparam name="T">Параметр типа наследника BankAccount</typeparam>
        /// <param name="list">Соответствующий типу список в синглтоне Bank</param>
        /// <param name="ID">Идентификатор счёта</param>
        /// <returns></returns>
        public BankAccount ba<T>(ref ObservableCollection<BankAccount> list, long ID) 
            where T : BankAccount
        {
            T tmp = default;
            if (typeof(T) == typeof(BankCreditAccount) && !CreditIsActive) return tmp;
            else if (typeof(T) == typeof(BankDepositAccount) && !DepositIsActive) return tmp;
            else if (typeof(T) == typeof(BankDebitAccount) && !DebitIsActive) return tmp;
            foreach (var e in list)
            {
                if (e.ID == ID)
                {
                    tmp = (T)e;
                }
            }
            return tmp;
        }

        /// <summary>
        /// Назначает идентификатор клиента
        /// </summary>
        public void SetID()
        {
            ClientID = ++ThisBank.currentClientID;
        }

        /// <summary>
        /// Строковое представление информации о клиенте
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{ClientID} {Name} {Reputation}";
        }

        #endregion
    }
}
