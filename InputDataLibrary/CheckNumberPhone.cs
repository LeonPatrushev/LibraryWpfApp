using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InputDataLibrary
{
    public class CheckNumberPhone
    {
        /// <summary>
        /// Метод проверяющий строку с номером телефона на корректность.
        /// </summary>
        /// <param name="numberPhone">
        /// Строка с номером телефона.
        /// </param>
        /// <returns>
        /// В случае полного совпадения с критериями синтаксиса номера телефона, выдает истину, иначе ложь.
        /// </returns>
        public bool CheckFullNumberPhone(string numberPhone)
        {
            string validNumberPhone = @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$";
            if (!Regex.IsMatch(numberPhone, validNumberPhone, RegexOptions.IgnoreCase))
            {
                return false;
            }
            return true;
        }
    }
}
