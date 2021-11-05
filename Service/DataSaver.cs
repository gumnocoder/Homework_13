using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;

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
