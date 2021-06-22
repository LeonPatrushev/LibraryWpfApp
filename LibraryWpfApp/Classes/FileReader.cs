using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWpfApp.Classes
{
    class FileReader
    {
        /// <summary>
        /// Метод считывающий информацию из csv-файла.
        /// </summary>
        /// <param name="fileNameCSV">
        /// Название файла.
        /// </param>
        /// <returns>
        /// Возвращает данные прочитанные из csv-файла.
        /// </returns>
        public List<string> CSVReader(string fileNameCSV, string defaultData)
        {
            List<string> returnData = new List<string>();

            returnData.Add(defaultData);
            returnData.Add("Неизвестно");

            string folderPath = Directory.GetCurrentDirectory();
            folderPath = folderPath.Replace("\\bin\\Debug", "\\Resources\\");
            using (StreamReader reader = new StreamReader(folderPath + fileNameCSV))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string dataContent = line;
                    returnData.Add(dataContent);
                }
                return returnData;
            }
        }
    }
}
