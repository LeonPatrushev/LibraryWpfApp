using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InputDataLibrary
{
    public class CheckEmail
    {
        /// <summary>
        /// Метод проверяющий строку с Email на корректность.
        /// </summary>
        /// <param name="email">
        /// Строка с Email.
        /// </param>
        /// <returns>
        /// В случае полного совпадения с критериями синтаксиса Email, выдает истину, иначе ложь.
        /// </returns>
        public bool CheckFullEmail(string email)
        {
            string validEmail = @"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$";
            if (!Regex.IsMatch(email, validEmail, RegexOptions.IgnoreCase))
            {
                return false;
            }
            return true;
        }
    }
}
