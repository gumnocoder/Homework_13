using System.Diagnostics;
using BankModelLibrary;

namespace Homework_13.Service
{
    public class BankSettingsLoader
    {
        private readonly string _creditIdFile = "credid.ini";
        private readonly string _debitIdFile = "debid.ini";
        private readonly string _userIdFile = "usid.ini";
        private readonly string _clientIdFile = "clid.ini";
        private readonly string _depositIdFile = "depid.ini";

        /// <summary>
        /// парсит файл и конвертирует значение в параметр типа long
        /// </summary>
        /// <param name="idGetter">ссылка на параметр банка</param>
        /// <param name="path">входящий файл</param>
        /// <param name="defaultValue">значение по умолчанию возвращается 
        /// в случае неудачного парсинга</param>
        public void SetId(ref long idGetter, string path, long defaultValue)
        {
            if (long.TryParse(FilesChecker.GetIniContent(path), out long tmp))
            {
                Debug.WriteLine("path parsed");
                idGetter = tmp;
            }
            else
            {
                Debug.WriteLine("path don`t parsed");
                idGetter = defaultValue;
            }
        }

        /// <summary>
        /// выполняет загрузку всех параметров банка
        /// </summary>
        /// <param name="bank">экземпляр банка, в частном случае синглтон ThisBank</param>
        public BankSettingsLoader(Bank bank)
        {
            SetId(ref bank.currentCreditID, _creditIdFile, (long)10000);
            SetId(ref bank.currentDebitID, _debitIdFile, (long)10000000);
            SetId(ref bank.currentClientID, _clientIdFile, (long)100000);
            SetId(ref bank.currentUserID, _userIdFile, (long)1);
            SetId(ref bank.currentDepositID, _depositIdFile, (long)1);
        }
    }
}
