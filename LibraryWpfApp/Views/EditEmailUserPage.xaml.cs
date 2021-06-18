using LibraryWpfApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
using InputDataLibrary;

namespace LibraryWpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для EditEmailUserPage.xaml
    /// </summary>
    public partial class EditEmailUserPage : Page
    {
        Core db;
        Users dataUsers;
        CheckEmail objectCheckEmail;
        /// <summary>
        /// Страница изменения Email.
        /// </summary>
        public EditEmailUserPage()
        {
            InitializeComponent();

            db = new Core();
            dataUsers = new Users();
            objectCheckEmail = new CheckEmail();

            dataUsers = db.context.Users.Where(x => x.id_user == Properties.Settings.Default.idUsers).First();

            MailAddress from = new MailAddress("libraryapppatrushev@gmail.com", "Администрация библиотеки");
            MailAddress to = new MailAddress(dataUsers.email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Смена данных для входа в библиотеке";
            m.Body = "<h4>Попытка изменения Email на вашем аккаутне</h4>"
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
        /// Изменение Email.
        /// </summary>
        private void EditEmailUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (objectCheckEmail.CheckFullEmail(NewEmailTextBox.Text) == true && PassTextBox.Text == dataUsers.pass)
            {
                db.context.Users.Where(x => x.id_user == dataUsers.id_user).First().email = NewEmailTextBox.Text;
                db.context.SaveChanges();

                MailAddress from = new MailAddress("libraryapppatrushev@gmail.com", "Администрация библиотеки");
                MailAddress to = new MailAddress(NewEmailTextBox.Text);
                MailMessage m = new MailMessage(from, to);
                m.Subject = "Смена данных для входа в библиотеке";
                m.Body = "<h4>Email для входа в аккаунт успешно изменен</h4>"
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
            else if(objectCheckEmail.CheckFullEmail(NewEmailTextBox.Text) == false)
            {
                MessageBox.Show("Введен некорректный Email");
            }
            else if(PassTextBox.Text == String.Empty)
            {
                MessageBox.Show("Не введен пароль");
            }
        }
    }
}
