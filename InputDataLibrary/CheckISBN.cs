using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace InputDataLibrary
{
    /// <summary>
    /// Проверка ISBN.
    /// </summary>
    public class CheckISBN
    {
        /// <summary>
        /// Проверка кода ISBN.
        /// </summary>
        /// <param name="isbn">
        /// Код ISBN.
        /// </param>
        /// <returns>
        /// В случае полного совпадения с критериями кода ISBN, выдает истину, иначе ложь.
        /// </returns>
        public bool CheckFullISBN(string isbn)
        {
            string validSymbo = "1234567890-";
            string prefix;
            string countryCode;
            string publisherCode;
            string publicationCode;
            string controlNumber;

            if (!(isbn.Length == 17||isbn.Length == 13))
            {
                throw new Exception("Некорректное количество символов в ISBN");
            }

            if (isbn.Contains(validSymbo))
            {
                throw new Exception("Использованы некорректные символы в ISBN");
            }

            if (isbn.Length == 17)
            {
                prefix = isbn.Substring(0, 4).Remove(3, 1);
                if(CheckPrefix(prefix) == false)
                {
                    throw new Exception("Некорректный префикс в ISBN");
                }
                isbn = isbn.Remove(0, 4);
            }

            countryCode = isbn.Split('-')[0];
            publisherCode = isbn.Split('-')[1];
            publicationCode = isbn.Split('-')[2];
            controlNumber = isbn.Split('-')[3];
            
            if (CheckCountryCode(countryCode) == null)
            {
                throw new Exception("Неправельный код страны в ISBN");
            }

            if (CheckControlNumber(isbn,controlNumber) == false)
            {
                throw new Exception("Неправильное контрольное число в ISBN");
            }

            return true;
        }

        /// <summary>
        /// Проверка кода страны.
        /// </summary>
        /// <param name="countryCode">
        /// Код страны из ISBN.
        /// </param>
        /// <returns>
        /// В случае, если код страны совпадает со списком, возвращает название страны, иначе пустую строку.
        /// </returns>
        public string CheckCountryCode(string countryCode)
        {
            string countryName;

            string folderPath = Directory.GetCurrentDirectory();
            folderPath = folderPath.Replace("\\bin\\Debug", "\\Resources\\");
            using (StreamReader reader = new StreamReader(folderPath+"ISBNRegGroups.csv"))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    if(countryCode == line.Split(';')[0])
                    {
                        countryName = line.Split(';')[1];
                        return countryName;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Проверка префикса на допустимое значение.
        /// </summary>
        /// <param name="prefix">
        /// Префикс из ISBN
        /// </param>
        /// <returns>
        /// Если префикс совпадает с возможными вариантами, выдает истину, иначе ложь.
        /// </returns>
        public bool CheckPrefix(string prefix)
        {
            if(!(prefix == "978" || prefix == "979"))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Проверка контрольной цифры
        /// </summary>
        /// <param name="isbn">
        /// Код ISBN
        /// </param>
        /// <param name="controlNumber">
        /// Контрольное число из ISBN
        /// </param>
        /// <returns>
        /// В случае совпадении вычислений с контрольным числом, выдает истину, иначе ложь.
        /// </returns>
        public bool CheckControlNumber(string isbn, string controlNumber)
        {
            isbn = isbn.Remove(isbn.Length - controlNumber.Length, controlNumber.Length).Replace("-", "");
            string weightSymbols = "123456789";
            int result = 0;

            for(int i = 0; i < weightSymbols.Length ; i++)
            {
                result += Convert.ToInt32(Char.GetNumericValue(isbn[i])) * Convert.ToInt32(Char.GetNumericValue(weightSymbols[i]));
            }

            result = result % 11;

            if(result > 9)
            {
                result = 0;
            }

            if (Convert.ToInt32(controlNumber) != result)
            {
                return false;
            }

            return true;
        }
    }
}
