using System.Collections.ObjectModel;
using System.IO;
using Homework_13.Model;
using Newtonsoft.Json;
using static Homework_13.Model.ClientList<Homework_13.Model.Client>;
using static Homework_13.Model.UserList<Homework_13.Model.User>;
using static Homework_13.Model.bankModel.Bank;
using Homework_13.Model.bankModel;

namespace Homework_13.Service
{
    /// <summary>
    /// Выполняет сериализацию  обьектов в json
    /// </summary>
    /// <typeparam name="T">объект подлежащий сериализации</typeparam>
    public static class DataSaver<T>
    {
        public static void DataSaverChain()
        {
            DataSaver<User>.JsonSeralize(UserList<User>.UsersList, UsersPath);
            DataSaver<Client> .JsonSeralize(ClientList<Client>.ClientsList, ClientsPath);
            DataSaver<BankAccount>.JsonSeralize(ThisBank.Credits, ThisBank.CreditsPath);
            DataSaver<BankAccount>.JsonSeralize(ThisBank.Debits, ThisBank.DebitsPath);
            DataSaver<BankAccount>.JsonSeralize(ThisBank.Deposits, ThisBank.DepositsPath);
            new BankSettingsSaver(ThisBank);
        }

        /// <summary>
        /// удаляет файл перед сохранением, во избежание ошибок
        /// </summary>
        /// <param name="outputFile"></param>
        private static void DeleteIfExists(string outputFile)
        {
            //string pathToFile = Path.Combine(Environment.CurrentDirectory + @"\" + outputFile);
            if (File.Exists(outputFile))
            {
                File.Delete(outputFile);
            }
        }

        /// <summary>
        /// выполняет сериализацию
        /// </summary>
        /// <param name="serializibleObject">сериализуемый обьект</param>
        /// <param name="path">выходной файл</param>
        public static void JsonSeralize(ObservableCollection<T> serializibleObject, string path)
        {
            DeleteIfExists(path);
            string json = JsonConvert.SerializeObject(serializibleObject);
            File.WriteAllText(path, json);
        }
    }

}
