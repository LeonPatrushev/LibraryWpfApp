using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;

namespace LibraryWpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для EditBooksPage.xaml
    /// </summary>
    public partial class EditBooksPage : Page
    {
        Core db;
        int idBook;
        FileReader objectFileReader;
        string fileNameBBK;
        string fileNameLanguage;
        string fileNameGenre;
        string defaultBBk;
        string defaultGenre;
        string defaultLanguage;
        List<Bookbinding> dataBookbindings;
        CheckISBN objectCheckISBN;
        /// <summary>
        /// Страница редактирования книги.
        /// </summary>
        /// <param name="idSelectBook">
        /// Id выбранной книги.
        /// </param>
        public EditBooksPage(int idSelectBook)
        {
            InitializeComponent();
            db = new Core();
            objectCheckISBN = new CheckISBN();
            objectFileReader = new FileReader();
            dataBookbindings = new List<Bookbinding>();
            idBook = idSelectBook;
            fileNameBBK = "BBK.csv";
            fileNameLanguage = "Languages.csv";
            fileNameGenre = "Genre.csv";
            this.DataContext = db.context.Books.Where(x =>x.id_book == idBook).First();

            if (db.context.Books.Where(x => x.id_book == idBook).First().book_image != null)
            {
                byte[] imageSource = db.context.Books.Where(x => x.id_book == idBook).First().book_image;
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

            defaultBBk = db.context.Books.Where(x => x.id_book == idBook).First().classification_BBK;
            ClassificationBBKComboBox.ItemsSource = objectFileReader.CSVReader(fileNameBBK, defaultBBk);
            ClassificationBBKComboBox.SelectedIndex = 0;

            defaultGenre = db.context.Books.Where(x => x.id_book == idBook).First().genre;
            GenreComboBox.ItemsSource = objectFileReader.CSVReader(fileNameGenre, defaultGenre);
            GenreComboBox.SelectedIndex = 0;

            defaultLanguage = db.context.Books.Where(x => x.id_book == idBook).First().language;
            LanguageComboBox.ItemsSource = objectFileReader.CSVReader(fileNameLanguage, defaultLanguage);
            LanguageComboBox.SelectedIndex = 0;

            dataBookbindings = db.context.Bookbinding.ToList();
            for(int i = 1; i <= dataBookbindings.Count(); i++)
            {
                TypeBookbindingComboBox.Items.Add(dataBookbindings.Where(x=>x.id_bookbinding == i).First().type_bookbinding);
            }
            TypeBookbindingComboBox.SelectedIndex = (int)db.context.Books.Where(x => x.id_book == idBook).First().id_bookbinding -1;

            if (db.context.Books.Where(x=>x.id_book == idBook).First().id_status == 1)
            {
                AvailableStatusRadioButton.IsChecked = true;
            }
            if(db.context.Books.Where(x => x.id_book == idBook).First().id_status == 2)
            {
                UnvailableStatusRadioButton.IsChecked = true;
            }
        }

        /// <summary>
        /// Редактирование информации о книге.
        /// </summary>
        private void EditBookButton_Click(object sender, RoutedEventArgs e)
        {
            if(NameTextBox.Text == String.Empty || AuthorTextBox.Text == String.Empty)
            {
                MessageBox.Show("Заполните обязательные поля 'Низвание' и 'Автор'");
            }
            else
            {
                if(CipherISBNTextBox.Text != String.Empty) 
                {
                    try
                    {
                        if(objectCheckISBN.CheckFullISBN(CipherISBNTextBox.Text) == true)
                        {
                            if (AvailableStatusRadioButton.IsChecked == true)
                            {
                                db.context.Books.Where(x => x.id_book == idBook).First().id_status = 1;
                            }
                            if (UnvailableStatusRadioButton.IsChecked == true)
                            {
                                db.context.Books.Where(x => x.id_book == idBook).First().id_status = 2;
                            }
                            db.context.Books.Where(x => x.id_book == idBook).First().classification_BBK = ClassificationBBKComboBox.Text;
                            db.context.Books.Where(x => x.id_book == idBook).First().genre = GenreComboBox.Text;
                            db.context.Books.Where(x => x.id_book == idBook).First().language = LanguageComboBox.Text;
                            db.context.Books.Where(x => x.id_book == idBook).First().id_bookbinding = TypeBookbindingComboBox.SelectedIndex + 1;
                            db.context.SaveChanges();
                            this.NavigationService.Navigate(new ViewingBooksPage());
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    if (AvailableStatusRadioButton.IsChecked == true)
                    {
                        db.context.Books.Where(x => x.id_book == idBook).First().id_status = 1;
                    }
                    if (UnvailableStatusRadioButton.IsChecked == true)
                    {
                        db.context.Books.Where(x => x.id_book == idBook).First().id_status = 2;
                    }
                    db.context.Books.Where(x => x.id_book == idBook).First().classification_BBK = ClassificationBBKComboBox.Text;
                    db.context.Books.Where(x => x.id_book == idBook).First().genre = GenreComboBox.Text;
                    db.context.Books.Where(x => x.id_book == idBook).First().language = LanguageComboBox.Text;
                    db.context.Books.Where(x => x.id_book == idBook).First().id_bookbinding = TypeBookbindingComboBox.SelectedIndex + 1;
                    db.context.SaveChanges();
                    this.NavigationService.Navigate(new ViewingBooksPage());
                }
            }
        }

        /// <summary>
        /// Удаление книги.
        /// </summary>
        private void DelBookButton_Click(object sender, RoutedEventArgs e)
        {
            Books objectBooks = db.context.Books.Where(x => x.id_book == idBook).First();
            List<Orders> dataOrders = db.context.Orders.Where(x => x.id_book == idBook).ToList();

            if (!dataOrders.Any())
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить книгу?", "Удалить?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    db.context.Books.Remove(objectBooks);
                    db.context.SaveChanges();
                    this.NavigationService.Navigate(new ViewingBooksPage());
                }
            }
            else
            {
                Orders objectOrders = db.context.Orders.Where(x => x.id_book == idBook).First();
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить книгу?", "Удалить?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    db.context.Orders.Remove(objectOrders);
                    db.context.Books.Remove(objectBooks);
                    db.context.SaveChanges();
                    this.NavigationService.Navigate(new ViewingBooksPage());
                }
            }
        }

        /// <summary>
        /// Загрузка обложки.
        /// </summary>
        private void UploadImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            if(fileDialog.ShowDialog() == true)
            {
                byte[] convertImageToByte = File.ReadAllBytes(fileDialog.FileName);
                db.context.Books.Where(x => x.id_book == idBook).First().book_image = convertImageToByte;

                if (convertImageToByte != null)
                {
                    byte[] imageSource = convertImageToByte;
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

                MessageBox.Show("Сохраните, что бы изменения вступили в силу");
            }
        }

        /// <summary>
        /// Удаление обложки.
        /// </summary>
        private void DelImageButton_Click(object sender, RoutedEventArgs e)
        {
            byte[] bookImage = null;
            db.context.Books.Where(x => x.id_book == idBook).First().book_image = bookImage;

            if (bookImage != null)
            {
                byte[] imageSource = bookImage;
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

            MessageBox.Show("Сохраните, что бы изменения вступили в силу");
        }
    }
}
