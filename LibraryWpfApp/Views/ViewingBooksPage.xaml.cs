using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibraryWpfApp.Models;
using LibraryWpfApp.Controllers;
using Excel = Microsoft.Office.Interop.Excel;

namespace LibraryWpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для ViewingBooksPage.xaml
    /// </summary>
    public partial class ViewingBooksPage : Page
    {
        Core db = new Core();
        List<Books> arrayBooks;
        Grid selectGrid;
        BooksController objectBooksController = new BooksController();
        Books selectedBook;
        /// <summary>
        /// Страница просмотра книг.
        /// </summary>
        public ViewingBooksPage()
        {
            InitializeComponent();
            arrayBooks = db.context.Books.ToList();
            SearchBookListView.ItemsSource = arrayBooks;
            PreviewBook.Visibility = Visibility.Hidden;
            if (Properties.Settings.Default.idUsers == 0 || Properties.Settings.Default.idUsers == 3)
            {
                ButtonsMenuBorder.Visibility = Visibility.Collapsed;
                PreviewBookBorder.CornerRadius = new CornerRadius(10);
                SelectBookBorder.SetValue(Grid.ColumnSpanProperty, 2);
                AddBookButton.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Вывод найденых книг с помошью поиска.
        /// </summary>
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchBookListView.ItemsSource = objectBooksController.SearchBook(arrayBooks, SearchTextBox.Text);
        }

        /// <summary>
        /// Выбор книги и отображение информации о ней.
        /// </summary>
        private void BookInfoGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            selectGrid = sender as Grid;
            selectedBook = selectGrid.DataContext as Books;
            PreviewBook.Visibility = Visibility.Visible;

            if(selectedBook.book_image != null)
            {
                byte[] imageSource = selectedBook.book_image;
                MemoryStream byteStream = new MemoryStream(imageSource);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = byteStream;
                image.EndInit();
                PreviewBookImage.Source = image;
            }
            else
            {
                PreviewBookImage.Source = new BitmapImage(new Uri(@"/Resources/Image/Interface/CoverStub.png", UriKind.RelativeOrAbsolute));
            }
            DescriptionBookTextBlock.Text = arrayBooks.Where(x => x.name == selectedBook.name).First().description;
            NameBookTextBlock.Text = arrayBooks.Where(x => x.name == selectedBook.name).First().name;
            AvailabilityBookTextBlock.Text = db.context.Status_book.Where(x => x.id_status == selectedBook.id_status).First().status;
        }

        /// <summary>
        /// Создание xlsx-файла и запись в него информации о книге.
        /// </summary>
        private void PrintInfoBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedBook != null)
            {
                var aplication = new Excel.Application();
                aplication.Visible = true;
                aplication.SheetsInNewWorkbook = 1;
                Excel.Workbook workbook = aplication.Workbooks.Add(Type.Missing);
                Excel.Worksheet worksheet = workbook.ActiveSheet;

                worksheet.Name = "BookInfo";

                worksheet.Cells[1][1] = "Название";
                worksheet.Cells[2][1] = "Автор";
                worksheet.Cells[3][1] = "Код ISBN";
                worksheet.Cells[4][1] = "Классификация ББК";
                worksheet.Cells[5][1] = "Издательство";
                worksheet.Cells[6][1] = "Место издательства";
                worksheet.Cells[7][1] = "Год издательства";
                worksheet.Cells[8][1] = "Количество страниц";
                worksheet.Cells[9][1] = "Жанр";
                worksheet.Cells[10][1] = "Переплет";
                worksheet.Cells[11][1] = "Язык";
                worksheet.Cells[12][1] = "Статус наличия";
                worksheet.Cells[13][1] = "Описание";

                worksheet.Cells[1][2] = db.context.Books.Where(x => x.id_book == selectedBook.id_book).First().name;
                worksheet.Cells[2][2] = db.context.Books.Where(x => x.id_book == selectedBook.id_book).First().author;
                worksheet.Cells[3][2] = db.context.Books.Where(x => x.id_book == selectedBook.id_book).First().cipher_ISBN;
                worksheet.Cells[4][2] = db.context.Books.Where(x => x.id_book == selectedBook.id_book).First().classification_BBK;
                worksheet.Cells[5][2] = db.context.Books.Where(x => x.id_book == selectedBook.id_book).First().publishing;
                worksheet.Cells[6][2] = db.context.Books.Where(x => x.id_book == selectedBook.id_book).First().place_of_publication;
                worksheet.Cells[7][2] = db.context.Books.Where(x => x.id_book == selectedBook.id_book).First().year_of_publication;
                worksheet.Cells[8][2] = db.context.Books.Where(x => x.id_book == selectedBook.id_book).First().number_of_pages;
                worksheet.Cells[9][2] = db.context.Books.Where(x => x.id_book == selectedBook.id_book).First().genre;
                worksheet.Cells[10][2] = db.context.Bookbinding.Where(x => x.id_bookbinding == selectedBook.id_bookbinding).First().type_bookbinding;
                worksheet.Cells[11][2] = db.context.Books.Where(x => x.id_book == selectedBook.id_book).First().language;
                worksheet.Cells[12][2] = db.context.Status_book.Where(x => x.id_status == selectedBook.id_status).First().status;
                worksheet.Cells[13][2] = db.context.Books.Where(x => x.id_book == selectedBook.id_book).First().description;

                aplication.Application.ActiveWorkbook.SaveAs("BookInfo.xlsx");
            }
            else
            {
                MessageBox.Show("Выберите читателя");
            }
        }

        /// <summary>
        /// Переход на страницу редактирования книги.
        /// </summary>
        private void EditBookButton_Click(object sender, RoutedEventArgs e)
        {
            if(selectedBook != null)
            {
                this.NavigationService.Navigate(new EditBooksPage(selectedBook.id_book));
            }
            else
            {
                MessageBox.Show("Выберите книгу");
            }
        }

        /// <summary>
        /// Вывод найденых книг с помошью поиска.
        /// </summary>
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBookListView.ItemsSource = objectBooksController.SearchBook(arrayBooks, SearchTextBox.Text);
        }

        /// <summary>
        /// Переход на страницу добавления новой книги.
        /// </summary>
        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddBookPage());
        }
    }
}
