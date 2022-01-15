using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using Homework_13.Model;
using Homework_13.Model.bankModel;
using Newtonsoft.Json;
using static Homework_13.Model.bankModel.Bank;

namespace Homework_13.Service
{
    public static class DataLoader<T>
    {

        /// <summary>
        /// Последовательная загрузка данных
        /// </summary>
        public static void LoadingChain()
        {
            new BankSettingsLoader(ThisBank);

            ObservableCollection<BankCreditAccount> creditsTmpList = new();
            ObservableCollection<BankDebitAccount> debitsTmpList = new();
            ObservableCollection<BankDepositAccount> depositsTmpList = new();

            DataLoader<BankCreditAccount>.LoadFromJson(creditsTmpList, ThisBank.CreditsPath);
            DataLoader<BankDebitAccount>.LoadFromJson(debitsTmpList, ThisBank.DebitsPath);
            DataLoader<BankDepositAccount>.LoadFromJson(depositsTmpList, ThisBank.DepositsPath);

            FillList(ThisBank.Credits, creditsTmpList);
            FillList(ThisBank.Debits, debitsTmpList);
            FillList(ThisBank.Deposits, depositsTmpList);

            DataLoader<Client>.LoadFromJson(
                ClientList<Client>.ClientsList, 
                ClientList<Client>.ClientsPath);
        }

        /// <summary>
        /// Заполняет конечную коллекцию элементами из промежуточной колеекции,
        /// например для конечных коллекций с обобщением от абстрактного класса
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="TargetList">конечная коллекция</param>
        /// <param name="InputList">временная коллекция</param>
        private static void FillList<U>(
            ObservableCollection<BankAccount> TargetList,
            ObservableCollection<U> InputList)
            where U : BankAccount
        {
            foreach (var e in InputList)
            {
                TargetList.Add(e);
                Debug.WriteLine($"{e} added to {TargetList}");
            }
            Debug.WriteLine($"load into {TargetList} complete");
        }

        /// <summary>
        /// Загружает данные из json в коллекции ObservableCollection
        /// </summary>
        /// <param name="targetList">целевая коллекция</param>
        /// <param name="inputFile">входящий файл</param>
        public static void LoadFromJson(
            ObservableCollection<T> targetList, 
            string inputFile)
        {
            if (File.Exists(inputFile))
            {
                ObservableCollection<T> tmp = new();
                using (FileStream fs = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
                {
                    string json = File.ReadAllText(inputFile);
                    tmp = JsonConvert.DeserializeObject<ObservableCollection<T>>(json);
                    foreach (T u in tmp) targetList.Add(u);
                }
            }
            else
            {
                Debug.WriteLine(File.Exists(inputFile));
                targetList = new ObservableCollection<T>();
            }
        }
    }
}
