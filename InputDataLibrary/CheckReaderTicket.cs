using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputDataLibrary
{
    /// <summary>
    /// Проверка читательского билета.
    /// </summary>
    public class CheckReaderTicket
    {
        /// <summary>
        /// Проверка кода читатльского билета.
        /// </summary>
        /// <param name="ReaderTicket">
        /// Код читательского билета.
        /// </param>
        /// <returns>
        /// В случае полного совпадения с критериями кода читательского билета, выдать истину, иначе ложь.
        /// </returns>
        public bool CheckFullReaderTicket(string ReaderTicket)
        {
            ReaderTicket = ReaderTicket.ToUpper();
            string accessRights = ReaderTicket.Substring(0, 1);
            string digitPart = ReaderTicket.Substring(1);

            if (CheckAccessRights(accessRights) == false)
            {
                throw new Exception("Неправильный код доступа");
            }

            foreach (var num in digitPart)
            {
                if (!Char.IsDigit(num))
                {
                    throw new Exception("Использованы некорректные символы");
                }
            }

            return true;
        }

        /// <summary>
        /// Проверка кода доступа на допустимы варианты.
        /// </summary>
        /// <param name="accessRights">
        /// Код доступа.
        /// </param>
        /// <returns>
        /// В случае полного совпадения с критериями кода доступа, выдать истину, иначе ложь.
        /// </returns>
        public bool CheckAccessRights(string accessRights)
        {
            if (accessRights == "А" || accessRights == "Ч" || accessRights == "О")
            {
                return true;
            }

            return false;
        }
    }
}
