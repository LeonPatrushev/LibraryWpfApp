using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWpfApp.Classes
{
    class CreatingPassword
    {
        public string GeneratingRandomPassword()
        {
            string validSymbol = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string password ="";
            Random rnd = new Random();
            for (int i = 0; i < 8; i++)
            {
                password += validSymbol[rnd.Next(validSymbol.Length)];
            }
            return password;
        }
    }
}
