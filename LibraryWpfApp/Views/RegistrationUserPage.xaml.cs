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
using InputDataLibrary;
using System.Net.Mail;
using System.Net;

namespace LibraryWpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для RegistrationUserPage.xaml
    /// </summary>
    public partial class RegistrationUserPage : Page
    {
        string nameUser;
        string surnameUser;
        string patronymicUser;
        DateTime birthDateUser;
        string addresUser;
        string placeOfWorkUser;
        string telephoneUser;
        string codeLibraryCardUser;
        Core db;
        CheckEmail objectCheckEmail;
        CreatingPassword objectCreatingPassword;
        string password;

        /// <summary>
        /// Страница регистрации аккаунта пользователя.
        /// </summary>
        /// <param name="name">
        /// Имя пользователя.
        /// </param>
        /// <param name="surname">
        /// Фамилия пользователя.
        /// </param>
        /// <param name="patronymic">
        /// Отчество пользователя.
        /// </param>
        /// <param name="birthDate">
        /// Дата рождения пользователя.
        /// </param>
        /// <param name="addres">
        /// Адрес пользователя.
        /// </param>
        /// <param name="placeOfWork">
        /// Место работы или учебы пользователя.
        /// </param>
        /// <param name="telephone">
        /// Номер телефона пользователя.
        /// </param>
        /// <param name="codeLibraryCard">
        /// Код читательского билета пользователя.
        /// </param>
        public RegistrationUserPage(string name, string surname, string patronymic, DateTime birthDate, string addres, string placeOfWork, string telephone, string codeLibraryCard)
        {
            InitializeComponent();
            db = new Core();
            objectCheckEmail = new CheckEmail();
            objectCreatingPassword = new CreatingPassword();
            nameUser = name;
            surnameUser = surname;
            patronymicUser = patronymic;
            birthDateUser = birthDate;
            addresUser = addres;
            placeOfWorkUser = placeOfWork;
            telephoneUser = telephone;
            codeLibraryCardUser = codeLibraryCard;
            if(Properties.Settings.Default.idRoleUsers != 1)
            {
                TypeUserTextBlock.Visibility = Visibility.Hidden;
                LibrarianRadioButton.Visibility = Visibility.Hidden;
                AdminRadioButton.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Создание аккаунта пользователя и сохранение в базе данных.
        /// </summary>
        private void CreatingUserButton_Click(object sender, RoutedEventArgs e)
        {

            if (objectCheckEmail.CheckFullEmail(EmailTextBox.Text) == true)
            {
                if(LibrarianRadioButton.IsChecked == true)
                {
                    password = objectCreatingPassword.GeneratingRandomPassword();

                    MailAddress from = new MailAddress("libraryapppatrushev@gmail.com", "Администрация библиотеки");
                    MailAddress to = new MailAddress(EmailTextBox.Text);
                    MailMessage m = new MailMessage(from, to);
                    m.Subject = "Приветствуем вас в нашей библиотеке";
                    m.Body = "<h4>Здравствуйте, " + nameUser + "</h4>"
                    + "<p><font-size: 18px>Ваши данные для входа в библиотеку</p>"
                    + "<p><font-size: 18px>Логин: " + EmailTextBox.Text + "</p>" + "<p><font-size = 18>Пароль: " + password + "</p>"
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

                    Users objectUsers = new Users()
                    {
                        id_role = 2,
                        email = EmailTextBox.Text,
                        pass = password,
                        name = nameUser,
                        surname = surnameUser,
                        patronymic = patronymicUser,
                        birth_date = birthDateUser,
                    };

                    db.context.Users.Add(objectUsers);
                    db.context.SaveChanges();

                    MessageBox.Show("Библиотекарь зарегистрирован, данные для входа отправленны на его Email.");
                    this.NavigationService.Navigate(new MainPage());
                }
                else if (AdminRadioButton.IsChecked == true)
                {
                    password = objectCreatingPassword.GeneratingRandomPassword();

                    MailAddress from = new MailAddress("libraryapppatrushev@gmail.com", "Администрация библиотеки");
                    MailAddress to = new MailAddress(EmailTextBox.Text);
                    MailMessage m = new MailMessage(from, to);
                    m.Subject = "Приветствуем вас в нашей библиотеке";
                    m.Body = "<h4>Здравствуйте, " + nameUser + "</h4>"
                    + "<p><font-size: 18px>Ваши данные для входа в библиотеку</p>"
                    + "<p><font-size: 18px>Логин: " + EmailTextBox.Text + "</p>" + "<p><font-size = 18>Пароль: " + password + "</p>"
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

                    Users objectUsers = new Users()
                    {
                        id_role = 1,
                        email = EmailTextBox.Text,
                        pass = password,
                        name = nameUser,
                        surname = surnameUser,
                        patronymic = patronymicUser,
                        birth_date = birthDateUser,
                    };

                    db.context.Users.Add(objectUsers);
                    db.context.SaveChanges();

                    MessageBox.Show("Администратор зарегистрирован, данные для входа отправленны на его Email.");
                    this.NavigationService.Navigate(new MainPage());
                }
                else
                {
                    password = objectCreatingPassword.GeneratingRandomPassword();

                    MailAddress from = new MailAddress("libraryapppatrushev@gmail.com", "Администрация библиотеки");
                    MailAddress to = new MailAddress(EmailTextBox.Text);
                    MailMessage m = new MailMessage(from, to);
                    m.Subject = "Приветствуем вас в нашей библиотеке";
                    m.Body = "<h4>Здравствуйте, " + nameUser + "</h4>"
                    + "<p><font-size: 18px>Ваши данные для входа в библиотеку</p>"
                    + "<p><font-size: 18px>Логин: " + EmailTextBox.Text + "</p>" + "<p><font-size = 18>Пароль: " + password + "</p>"
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

                    Library_card objectLibraryCard = new Library_card()
                    {
                        name = nameUser,
                        surname = surnameUser,
                        patronymic = patronymicUser,
                        birth_date = birthDateUser,
                        addres = addresUser,
                        place_of_work = placeOfWorkUser,
                        telephone = telephoneUser,
                        code_library_card = codeLibraryCardUser,
                    };

                    Users objectUsers = new Users()
                    {
                        id_role = 3,
                        id_library_card = objectLibraryCard.id_library_card,
                        email = EmailTextBox.Text,
                        pass = password,
                    };

                    db.context.Library_card.Add(objectLibraryCard);
                    db.context.Users.Add(objectUsers);
                    db.context.SaveChanges();

                    MessageBox.Show("Читатель зарегистрирован, данные для входа отправленны на его Email");
                    this.NavigationService.Navigate(new MainPage());
                }
            }
            else
            {
                MessageBox.Show("Введен некорректный Email");
            }
        }
    }
}
