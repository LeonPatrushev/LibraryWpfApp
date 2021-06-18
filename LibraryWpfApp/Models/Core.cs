using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWpfApp.Models
{
    /// <summary>
    /// Подключение к LibraryDataBaseEntities.
    /// </summary>
    public class Core
    {
        public LibraryDataBaseEntities context = new LibraryDataBaseEntities();
    }
}
