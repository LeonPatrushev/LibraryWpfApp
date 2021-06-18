using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryWpfApp.Models;

namespace LibraryWpfApp.Controllers
{
    class BooksController
    {
        Core db = new Core();
        /// <summary>
        /// Метод поиска книги по иназванию/коду IBN/автору.
        /// </summary>
        /// <param name="arrayBooks">
        /// Список с инормацией о книгах.
        /// </param>
        /// <param name="searchLine">
        /// Строка с данными поика.
        /// </param>
        /// <returns>
        /// Список с информацией о книге удовлетворяющий поиску.
        /// </returns>
        public List<Books> SearchBook(List<Books> arrayBooks, string searchLine)
        {
            List<Books> returnData = new List<Books>();
            if (arrayBooks.Where(x => x.name.ToLower().Contains(searchLine.ToLower())).Any())
            {
                returnData = arrayBooks.Where(x => x.name.ToLower().Contains(searchLine.ToLower())).ToList();
                return returnData;
            }
            if (arrayBooks.Where(x => x.author.ToLower().Contains(searchLine.ToLower())).Any())
            {
                returnData = arrayBooks.Where(x => x.author.ToLower().Contains(searchLine.ToLower())).ToList();
                return returnData;
            }
            if (arrayBooks.Where(x => x.cipher_ISBN.ToLower().Contains(searchLine.ToLower())).Any())
            {
                returnData = arrayBooks.Where(x => x.cipher_ISBN.ToLower().Contains(searchLine.ToLower())).ToList();
                return returnData;
            }
            return returnData;
        }
    }
}
