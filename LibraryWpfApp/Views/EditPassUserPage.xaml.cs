using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibraryWpfApp.Models;
using LibraryWpfApp.Classes;
using System.Net.Mail;
using System.Net;

namespace LibraryWpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для EditPassUserPage.xaml
    /// </summary>
    public partial class EditPassUserPage : Page
    {
        Core db;
        CreatingPassword objectCreatingPassword;
        string codeForMail;
        Users dataUsers;
        /// <summary>
        /// Страница изменения пароля.
        /// </summary>
        public EditPassUserPage()
        {
            InitializeComponent();
            db = new Core();
            objectCreatingPassword = new CreatingPassword();
            dataUsers = new Users();
            dataUsers = db.context.Users.Where(x => x.id_user == Properties.Settings.Default.idUsers).First();

            codeForMail = objectCreatingPassword.GeneratingRandomPassword();

            MailAddress from = new MailAddress("libraryapppatrushev@gmail.com", "Администрация библиотеки");
            MailAddress to = new MailAddress(dataUsers.email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Смена данных для входа в библиотеке";
            m.Body = "<h4>Попытка изменения пароля на вашем аккаутне</h4>"
            + "<p><font-size: 18px>Код для подтверждения смены пароля:" + codeForMail + "</p>"
            + "<p><font-size: 18px>Если это были не вы, обратитесь в администрацию библиотеки</p>";
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("libraryapppatrushev@gmail.com", "LibraryAppPass");
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(m);
            }
            catch
            {

            }

        }

        /// <summary>
        /// Изменение пароля.
        /// </summary>
        private void EditPassUserButton_Click(object sender, RoutedEventArgs e)
        {
            if(NewPassTextBox.Text != String.Empty && RepeatNewPassTextBox.Text == NewPassTextBox.Text && CodeFromMailTextBox.Text == codeForMail)
            {
                db.context.Users.Where(x => x.id_user == Properties.Settings.Default.idUsers).First().pass = NewPassTextBox.Text;
                db.context.SaveChanges();

                MailAddress from = new MailAddress("libraryapppatrushev@gmail.com", "Администрация библиотеки");
                MailAddress to = new MailAddress(dataUsers.email);
                MailMessage m = new MailMessage(from, to);
                m.Subject = "Смена данных для входа в библиотеке";
                m.Body = "<h4>Пароль для входа в аккаунт успешно изменен</h4>"
                + "<p><font-size: 18px>Приятного чтения! :)</p>";
                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("libraryapppatrushev@gmail.com", "LibraryAppPass");
                smtp.EnableSsl = true;
                try
                {
                    smtp.Send(m);
                }
                catch
                {

                }

                this.NavigationService.Navigate(new PersonalAccountMenuPage());
            }
            else if(RepeatNewPassTextBox.Text != NewPassTextBox.Text)
            {
                MessageBox.Show("Пароли не совпадают");
            }
            else if (CodeFromMailTextBox.Text != codeForMail)
            {
                MessageBox.Show("Не верный код подтверждения с почты");
            }
            else
            {
                MessageBox.Show("Пароль не может быть пустым");
            }
        }
    }
}
