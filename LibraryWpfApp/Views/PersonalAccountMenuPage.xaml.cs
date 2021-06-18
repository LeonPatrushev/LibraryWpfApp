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

namespace LibraryWpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для PersonalAccountMenuPage.xaml
    /// </summary>
    public partial class PersonalAccountMenuPage : Page
    {
        Core db;
        int idLibraryCard;
        /// <summary>
        /// Страница личного кабинета.
        /// </summary>
        public PersonalAccountMenuPage()
        {
            InitializeComponent();
            db = new Core();

            if(Properties.Settings.Default.idRoleUsers == 1 || Properties.Settings.Default.idRoleUsers == 2)
            {
                RentedBooksReaderBorder.Visibility = Visibility.Collapsed;
                CompleteRentedBooksReaderBorder.Visibility = Visibility.Collapsed;
                CodeLibraryCardUserTextBlock.Visibility = Visibility.Collapsed;
                InfoUserBorder.SetValue(Grid.ColumnProperty, 1);
                this.DataContext = db.context.Users.Where(x => x.id_user == Properties.Settings.Default.idUsers).First();
            }
            else
            {

                idLibraryCard = Convert.ToInt32(db.context.Users.Where(x => x.id_user == Properties.Settings.Default.idUsers).First().id_library_card);

                List<Orders> listOrders = db.context.Orders.Where(x => x.id_library_card == idLibraryCard).ToList();
                List<Completed_orders> listCompletedOrders = db.context.Completed_orders.Where(x => x.id_library_card == idLibraryCard).ToList();

                this.DataContext = db.context.Library_card.Where(x => x.id_library_card == idLibraryCard).First();

                RentedBooksReaderListView.ItemsSource = db.context.Orders.Where(x => x.id_library_card == idLibraryCard).ToList();
                CompleteRentedBooksReaderListView.ItemsSource = db.context.Completed_orders.Where(x => x.id_library_card == idLibraryCard).ToList();

                if(!listOrders.Any())
                {
                    RentedBooksReaderListView.Visibility = Visibility.Collapsed;
                }
                if(!listCompletedOrders.Any())
                {
                    CompleteRentedBooksReaderListView.Visibility = Visibility.Collapsed;
                }
            }
            
        }

        /// <summary>
        /// Переход на страницу изменения пароля.
        /// </summary>
        private void EditPassUserDataButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new EditPassUserPage());
        }

        /// <summary>
        /// Переход на страницу изменения Email.
        /// </summary>
        private void EditEmailUserDataButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new EditEmailUserPage());
        }

    }
}
