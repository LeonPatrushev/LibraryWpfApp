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
    /// Логика взаимодействия для GiveBookReaderPage.xaml
    /// </summary>
    public partial class GiveBookReaderPage : Page
    {
        Core db;
        int idLibraryCard;
        Grid selectGrid;
        Books selectBook;
        /// <summary>
        /// Страница выдачи книги читателю.
        /// </summary>
        /// <param name="idReader">
        /// id читателя.
        /// </param>
        public GiveBookReaderPage(int idReader)
        {
            InitializeComponent();
            db = new Core();
            idLibraryCard = idReader;
            SearchBookListView.ItemsSource = db.context.Books.Where(x =>x.id_status == 1).ToList();
            IssueDateTextBlock.Text = Convert.ToString(DateTime.Now.ToShortDateString());
        }

        /// <summary>
        /// Выбор книги и сохранение информации о ней.
        /// </summary>
        private void BookInfoGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            selectGrid = sender as Grid;
            selectBook = selectGrid.DataContext as Books;
        }

        /// <summary>
        /// Выдача книги читателю.
        /// </summary>
        private void GiveBookButton_Click(object sender, RoutedEventArgs e)
        {
            if(selectBook != null)
            {
                Orders objectOrders = new Orders()
                {
                    id_book = selectBook.id_book,
                    id_library_card = idLibraryCard,
                    issue_date = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
                    return_date = Convert.ToDateTime(ReturnDatePicker.SelectedDate.Value.ToShortDateString()),
                };
                db.context.Books.Where(x => x.id_book == selectBook.id_book).First().id_status = 2;
                db.context.Orders.Add(objectOrders);
                db.context.SaveChanges();
                this.NavigationService.Navigate(new ViewingReadersPage());
            }
            else
            {
                MessageBox.Show("Выберите книгу");
            }
        }
    }
}
