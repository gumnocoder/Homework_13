using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using Homework_13.Model;
using Newtonsoft.Json;

namespace Homework_13.Service
{
    public static class DataLoader<T>
    {
        public static void LoadFromJson(ObservableCollection<T> targetList, string inputFile)
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
