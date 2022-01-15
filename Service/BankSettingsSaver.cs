using System.IO;
using Homework_13.Model.bankModel;

namespace Homework_13.Service
{
    class BankSettingsSaver
    {
        private readonly string creditIdFile = "credid.ini";
        private readonly string debitIdFile = "debid.ini";
        private readonly string userIdFile = "usid.ini";
        private readonly string clientIdFile = "clid.ini";
        private readonly string depositIdFile = "depid.ini";

        /// <summary>
        /// Записывает в файл порядковый номер параметра банка
        /// </summary>
        /// <param name="outputFile">файл сохранения</param>
        /// <param name="inputNum">входящий параметр</param>
        public void WriteBankSettingsToFile(string outputFile, ref long inputNum)
        {
            if (!File.Exists(outputFile))
            { using (FileStream fs = new(outputFile, FileMode.OpenOrCreate))
                { File.Create(outputFile); } }

            using (FileStream fs = new FileStream(outputFile, FileMode.Open, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(inputNum.ToString());
                }
            }
        }

        /// <summary>
        /// выполняет сохранение всех параметров банка
        /// </summary>
        /// <param name="bank">экземпляр банка</param>
        public BankSettingsSaver(Bank bank)
        {
            WriteBankSettingsToFile(creditIdFile, ref bank.currentCreditID);
            WriteBankSettingsToFile(debitIdFile, ref bank.currentDebitID);
            WriteBankSettingsToFile(userIdFile, ref bank.currentUserID);
            WriteBankSettingsToFile(clientIdFile, ref bank.currentClientID);
            WriteBankSettingsToFile(depositIdFile, ref bank.currentDepositID);
        }
    }
}
