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
using Excel = Microsoft.Office.Interop.Excel;

namespace LibraryWpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для ViewingReadersPage.xaml
    /// </summary>
    public partial class ViewingReadersPage : Page
    {
        Core db = new Core();
        Grid selectedGrid;
        Library_card selectLibraryCard;
        List<Library_card> library_Cards;
        LibraryCardController objectLibraryCardController = new LibraryCardController();
        Button selectButton;
        Orders selectOrder;
        /// <summary>
        /// Страница просмотра читателей.
        /// </summary>
        public ViewingReadersPage()
        {
            InitializeComponent();
            library_Cards = db.context.Library_card.ToList();
            SearchReadersListView.ItemsSource = library_Cards;
            RentedBooksReaderListView.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Выбор читателя и отображение информации о нём.
        /// </summary>
        private void ReadersInfoGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            selectedGrid = sender as Grid;
            selectLibraryCard = selectedGrid.DataContext as Library_card;
            NameReadersTextBlock.Text = selectLibraryCard.name;
            SurnameReadersTextBlock.Text = selectLibraryCard.surname;
            PatronymicReadersTextBlock.Text = selectLibraryCard.patronymic;
            RentedBooksReaderListView.ItemsSource = selectLibraryCard.Orders.ToList();
            RentedBooksReaderListView.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Переход на страницу редактирования читателя.
        /// </summary>
        private void EditReaderButton_Click(object sender, RoutedEventArgs e)
        {
            if(selectLibraryCard != null)
            {
                this.NavigationService.Navigate(new EditReadersPage(selectLibraryCard.id_library_card));
            }
            else
            {
                MessageBox.Show("Выберите читателя");
            }
        }

        /// <summary>
        /// Переход на страницу добавления нового читателя.
        /// </summary>
        private void AddReadersButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RegistrationReadersTicketPage());
        }

        /// <summary>
        /// Вывод найденых читателей с помошью поиска.
        /// </summary>
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchReadersListView.ItemsSource = objectLibraryCardController.SearchReader(library_Cards,SearchTextBox.Text);
        }

        /// <summary>
        /// Вывод найденых читателей с помошью поиска.
        /// </summary>
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchReadersListView.ItemsSource = objectLibraryCardController.SearchReader(library_Cards, SearchTextBox.Text);
        }

        /// <summary>
        /// Переход на страницу выдачи книги читателю.
        /// </summary>
        private void GiveBookReader_Click(object sender, RoutedEventArgs e)
        {
            if (selectLibraryCard != null)
            {
                this.NavigationService.Navigate(new GiveBookReaderPage(selectLibraryCard.id_library_card));
            }
            else
            {
                MessageBox.Show("Выберите читателя");
            }
        }

        /// <summary>
        /// Подтверждение возврата книги выбранным читателем.
        /// </summary>
        private void ConfirmRefundButton_Click(object sender, RoutedEventArgs e)
        {
            selectButton = sender as Button;
            selectOrder = selectButton.DataContext as Orders;
            Completed_orders objectCompletedOrders = new Completed_orders()
            {
                id_book = db.context.Orders.Where(x => x.id_book == selectOrder.id_book).First().id_book,
                id_library_card = db.context.Orders.Where(x => x.id_book == selectOrder.id_book).First().id_library_card,
                issue_date = db.context.Orders.Where(x => x.id_book == selectOrder.id_book).First().issue_date,
                return_date = db.context.Orders.Where(x => x.id_book == selectOrder.id_book).First().return_date,
            };
            if (selectOrder != null)
            {
                MessageBoxResult result = MessageBox.Show("Подтвердить получение ?", "Подтвердить?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    db.context.Orders.Remove(selectOrder);
                    db.context.Completed_orders.Add(objectCompletedOrders);
                    db.context.Books.Where(x => x.id_book == selectOrder.id_book).First().id_status = 1;
                    db.context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Создание xlsx-файла и запись в него информации о выбранном читателе.
        /// </summary>
        private void PrintInfoReadersButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectLibraryCard != null)
            {
                var aplication = new Excel.Application();
                aplication.Visible = true;
                aplication.SheetsInNewWorkbook = 1;
                Excel.Workbook workbook = aplication.Workbooks.Add(Type.Missing);
                Excel.Worksheet worksheet = workbook.ActiveSheet;

                worksheet.Name = "ReaderInfo";

                worksheet.Cells[1][1] = "Имя";
                worksheet.Cells[2][1] = "Фамилия";
                worksheet.Cells[3][1] = "Отчество";
                worksheet.Cells[4][1] = "Дата рождения";
                worksheet.Cells[5][1] = "Адрес проживания";
                worksheet.Cells[6][1] = "Место работы/учебы";
                worksheet.Cells[7][1] = "Номер телефона";

                worksheet.Cells[1][2] = db.context.Library_card.Where(x => x.id_library_card == selectLibraryCard.id_library_card).First().name;
                worksheet.Cells[2][2] = db.context.Library_card.Where(x => x.id_library_card == selectLibraryCard.id_library_card).First().surname;
                worksheet.Cells[3][2] = db.context.Library_card.Where(x => x.id_library_card == selectLibraryCard.id_library_card).First().patronymic;
                worksheet.Cells[4][2] = db.context.Library_card.Where(x => x.id_library_card == selectLibraryCard.id_library_card).First().birth_date;
                worksheet.Cells[5][2] = db.context.Library_card.Where(x => x.id_library_card == selectLibraryCard.id_library_card).First().addres;
                worksheet.Cells[6][2] = db.context.Library_card.Where(x => x.id_library_card == selectLibraryCard.id_library_card).First().place_of_work;
                worksheet.Cells[7][2] = db.context.Library_card.Where(x => x.id_library_card == selectLibraryCard.id_library_card).First().telephone;

                aplication.Application.ActiveWorkbook.SaveAs("ReaderInfo.xlsx");
            }
            else
            {
                MessageBox.Show("Выберите читателя");
            }
        }
    }
}
