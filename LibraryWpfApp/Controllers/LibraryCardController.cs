using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryWpfApp.Models;

namespace LibraryWpfApp.Controllers
{
    class LibraryCardController
    {
        Core db = new Core();
        public string GetCodeReadersTicket(string typeRidersTicket)
        {
            string codeReadersTicket = "";
            if(typeRidersTicket == "Только абонемент")
            {
                codeReadersTicket = "А";
            }
            if(typeRidersTicket == "Только читательный зал")
            {
                codeReadersTicket = "Ч";
            }
            if (typeRidersTicket == "Общий(читательный зал и абонемент)")
            {
                codeReadersTicket = "О";
            }

            List<Library_card> arrLibraryCard = db.context.Library_card.ToList();
            if (arrLibraryCard.Count() != 0)
            {
                codeReadersTicket += Convert.ToInt32(arrLibraryCard.Last().id_library_card)+1;
            }
            else
            {
                codeReadersTicket += 0;
            }

            codeReadersTicket += Convert.ToString(DateTime.Now.Year).Substring(2,2);

            return codeReadersTicket;
        }

        public string ChangeCodeReadersTicket(string typeRidersTicket, string codeReadersTicket)
        {
            string prefix = "";

            codeReadersTicket = codeReadersTicket.Remove(0, 1);

            if (typeRidersTicket == "Только абонемент")
            {
                prefix = "А";
            }
            if (typeRidersTicket == "Только читательный зал")
            {
                prefix = "Ч";
            }
            if (typeRidersTicket == "Общий(читательный зал и абонемент)")
            {
                prefix = "О";
            }

            codeReadersTicket = prefix + codeReadersTicket;

            return codeReadersTicket;
        }

        public List<Library_card> SearchReader(List<Library_card> library_Cards, string searchLine)
        {
            List<Library_card> returnData = new List<Library_card>();

            if (library_Cards.Where(x => x.name.ToLower().Contains(searchLine.ToLower())).Any())
            {
                returnData = library_Cards.Where(x => x.name.ToLower().Contains(searchLine.ToLower())).ToList();
                return returnData;
            }
            if (library_Cards.Where(x => x.surname.ToLower().Contains(searchLine.ToLower())).Any())
            {
                returnData = library_Cards.Where(x => x.surname.ToLower().Contains(searchLine.ToLower())).ToList();
                return returnData;
            }
            if (library_Cards.Where(x => x.patronymic.ToLower().Contains(searchLine.ToLower())).Any())
            {
                returnData = library_Cards.Where(x => x.patronymic.ToLower().Contains(searchLine.ToLower())).ToList();
                return returnData;
            }
            if (library_Cards.Where(x => x.code_library_card.ToLower().Contains(searchLine.ToLower())).Any())
            {
                returnData = library_Cards.Where(x => x.code_library_card.ToLower().Contains(searchLine.ToLower())).ToList();
                return returnData;
            }
            return returnData;
        }
    }
}
