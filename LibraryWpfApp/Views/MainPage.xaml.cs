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

namespace LibraryWpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        /// <summary>
        /// Страница главного меню.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            if(Properties.Settings.Default.idRoleUsers == 0 || Properties.Settings.Default.idRoleUsers >= 3)
            {
                BookManegmentButton.Visibility = Visibility.Collapsed;
                ReadersManagmentButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                ViewingBooksButton.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Переход на страницу просмотра книг.
        /// </summary>
        private void ViewingBooksButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewingBooksPage());
        }

        /// <summary>
        /// Переход на страницу просмотра и редактирования книг.
        /// </summary>
        private void BookManegmentButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewingBooksPage());
        }

        /// <summary>
        /// Переход на страницу просмотра и редактирования читателей.
        /// </summary>
        private void ReadersManagmentButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewingReadersPage());
        }
    }
}
