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
using LibraryWpfApp.Controllers;

namespace LibraryWpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        LibraryDataBaseEntities context = new LibraryDataBaseEntities();
        /// <summary>
        /// Страница входа в аккаунт.
        /// </summary>
        public LoginPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Вход в аккаунт.
        /// </summary>
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            UsersController objectUsersController = new UsersController();
            int[] infoUser = objectUsersController.LoginUser(LoginTextBox.Text, PassTextBox.Text);
            if (infoUser[0] != 0)
            {
                Properties.Settings.Default.idUsers = infoUser[1];
                Properties.Settings.Default.idRoleUsers = infoUser[0];
                Properties.Settings.Default.Save();
                this.NavigationService.Navigate(new MainPage());
            }
            else
            {
                MessageBox.Show("Неправельный логин или пароль");
            }          
        }

    }
}
