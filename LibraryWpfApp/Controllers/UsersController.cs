using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryWpfApp.Models;

namespace LibraryWpfApp.Controllers
{
    class UsersController
    {
        Core db = new Core();
        public int[] LoginUser(string login, string pass)
        {
            int[] idUser = new int[2];
            try
            {
                

                Users objectUser = db.context.Users.Where(x => x.email == login && x.pass == pass).First();

                if (objectUser == null)
                {
                    return idUser;
                }
                else
                {
                    idUser[0] = objectUser.id_role;
                    idUser[1] = objectUser.id_user;
                    return idUser;
                }
            }
            catch  
            {
                return idUser;
            }

        }
    }
}
