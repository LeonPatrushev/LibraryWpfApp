using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputDataLibrary
{
    public class CheckEmptyString
    {
        /// <summary>
        /// Метод проверяющий строку на то, что она не пустая.
        /// </summary>
        /// <param name="lineString">
        /// Проверяемая строка.
        /// </param>
        /// <returns>
        /// В случае если строка не пустая, выдает истину, иначе ложь.
        /// </returns>
        public bool CheckFullEmptyString(string lineString)
        {
            if (lineString == String.Empty)
            {
                return false;
            }
            return true;
        }
    }
}
