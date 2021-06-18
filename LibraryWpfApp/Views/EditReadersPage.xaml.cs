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
    /// Логика взаимодействия для EditreadersPage.xaml
    /// </summary>
    public partial class EditReadersPage : Page
    {
        int idLibraryCard;
        Core db;
        Library_card objectLibrary_Card;
        Users objectUsers;
        Orders objectOrders;
        Completed_orders objectCompletedOrders;
        /// <summary>
        /// Страница редактирования данных о читателе.
        /// </summary>
        /// <param name="idReader">
        /// Id выбранного читателя.
        /// </param>
        public EditReadersPage(int idReader)
        {
            InitializeComponent();
            db = new Core();
            idLibraryCard = idReader;
           
            this.DataContext=db.context.Library_card.Where(x => x.id_library_card == idLibraryCard).ToList();

            if (db.context.Library_card.Where(x => x.id_library_card == idLibraryCard).First().code_library_card[0] == 'А')
            {
                TypeReadersTicketComboBox.SelectedIndex = 0;
            }
            else if (db.context.Library_card.Where(x => x.id_library_card == idLibraryCard).First().code_library_card[0] == 'Ч')
            {
                TypeReadersTicketComboBox.SelectedIndex = 1;
            }
            else if (db.context.Library_card.Where(x => x.id_library_card == idLibraryCard).First().code_library_card[0] == 'О')
            {
                TypeReadersTicketComboBox.SelectedIndex = 2;
            }
        }

        /// <summary>
        /// Редактирование информации о читательском билете.
        /// </summary>
        private void EditReadersTicketButton_Click(object sender, RoutedEventArgs e)
        {
            string codeLibraryCard = db.context.Library_card.Where(x => x.id_library_card == idLibraryCard).First().code_library_card;
            LibraryCardController objectLibraryCardController = new LibraryCardController();
            db.context.Library_card.Where(x =>x.id_library_card == idLibraryCard).First().code_library_card = objectLibraryCardController.ChangeCodeReadersTicket(TypeReadersTicketComboBox.Text, codeLibraryCard);
            db.context.SaveChanges();
        }

        /// <summary>
        /// Удаление читательского билета и привязанного к нему аккаунта для входа в систему.
        /// </summary>
        private void DelReadersTicketButton_Click(object sender, RoutedEventArgs e)
        {
            objectLibrary_Card = db.context.Library_card.Where(x => x.id_library_card == idLibraryCard).First();
            objectUsers = db.context.Users.Where(x => x.id_library_card == idLibraryCard).First();
            if(db.context.Orders.Where(x => x.id_library_card == idLibraryCard).Count() != 0)
            {
                objectOrders = db.context.Orders.Where(x => x.id_library_card == idLibraryCard).First();
            }
            if(db.context.Completed_orders.Where(x => x.id_library_card == idLibraryCard).Count() != 0)
            {
                objectCompletedOrders = db.context.Completed_orders.Where(x => x.id_library_card == idLibraryCard).First();
            }

            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить пользователя?", "Удалить?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (db.context.Orders.Where(x => x.id_library_card == idLibraryCard).Count() != 0)
                {
                    db.context.Orders.Remove(objectOrders);
                }
                if (db.context.Completed_orders.Where(x => x.id_library_card == idLibraryCard).Count() != 0)
                {
                    db.context.Completed_orders.Remove(objectCompletedOrders);
                }
                db.context.Users.Remove(objectUsers);
                db.context.Library_card.Remove(objectLibrary_Card);
                db.context.SaveChanges();
            }
        }
    }
}
