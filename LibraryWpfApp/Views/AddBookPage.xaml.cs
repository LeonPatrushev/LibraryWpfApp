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
using InputDataLibrary;
using LibraryWpfApp.Classes;
using Microsoft.Win32;
using System.IO;

namespace LibraryWpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для AddBookPage.xaml
    /// </summary>
    public partial class AddBookPage : Page
    {
        Core db;
        FileReader objectFileReader;
        string fileNameBBK;
        string fileNameLanguage;
        string fileNameGenre;
        string defaultBBk;
        string defaultGenre;
        string defaultLanguage;
        byte[] bookImage;
        List<Bookbinding> dataBookbindings;
        CheckISBN objectCheckISBN;
        /// <summary>
        /// Страница добавления книги.
        /// </summary>
        public AddBookPage()
        {
            InitializeComponent();
            db = new Core();
            objectCheckISBN = new CheckISBN();
            objectFileReader = new FileReader();
            dataBookbindings = new List<Bookbinding>();
            bookImage = null;
            fileNameBBK = "BBK.csv";
            fileNameLanguage = "Languages.csv";
            fileNameGenre = "Genre.csv";
            defaultBBk = null;
            defaultGenre = null;
            defaultLanguage = null;

            PreviewBookImage.Source = new BitmapImage(new Uri(@"/Resources/Image/Interface/CoverStub.png", UriKind.RelativeOrAbsolute));

            ClassificationBBKComboBox.ItemsSource = objectFileReader.CSVReader(fileNameBBK, defaultBBk);
            ClassificationBBKComboBox.SelectedIndex = 0;

            GenreComboBox.ItemsSource = objectFileReader.CSVReader(fileNameGenre, defaultGenre);
            GenreComboBox.SelectedIndex = 0;

            LanguageComboBox.ItemsSource = objectFileReader.CSVReader(fileNameLanguage, defaultLanguage);
            LanguageComboBox.SelectedIndex = 0;

            dataBookbindings = db.context.Bookbinding.ToList();
            for (int i = 1; i <= dataBookbindings.Count(); i++)
            {
                TypeBookbindingComboBox.Items.Add(dataBookbindings.Where(x => x.id_bookbinding == i).First().type_bookbinding);
            }
            TypeBookbindingComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Создание книги и добавление информации в базу данных.
        /// </summary>
        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text == String.Empty || AuthorTextBox.Text == String.Empty)
            {
                MessageBox.Show("Заполните обязательные поля 'Название' и 'Автор'");
            }
            else
            {
                if (CipherISBNTextBox.Text != String.Empty)
                {
                    try
                    {
                        if (objectCheckISBN.CheckFullISBN(CipherISBNTextBox.Text) == true)
                        {

                            if(AvailableStatusRadioButton.IsChecked == true)
                            {
                                Books objectBooks = new Books()
                                {
                                    name = NameTextBox.Text,
                                    author = AuthorTextBox.Text,
                                    cipher_ISBN = CipherISBNTextBox.Text,
                                    classification_BBK = ClassificationBBKComboBox.Text,
                                    publishing = PublishingTextBox.Text,
                                    place_of_publication = PlaceOfPublicationTextBox.Text,
                                    year_of_publication = Convert.ToDateTime(YearOfPublicationDatePicker.SelectedDate),
                                    number_of_pages = NumberOfPagesTextBox.Text,
                                    genre = GenreComboBox.Text,
                                    id_bookbinding = TypeBookbindingComboBox.SelectedIndex + 1,
                                    language = LanguageComboBox.Text,
                                    id_status = 1,
                                    description = DescriptionTextBox.Text,
                                    book_image = bookImage,
                                };
                                db.context.Books.Add(objectBooks);
                                db.context.SaveChanges();
                                this.NavigationService.Navigate(new ViewingBooksPage());
                            }
                            if(UnvailableStatusRadioButton.IsChecked == true)
                            {
                                Books objectBooks = new Books()
                                {
                                    name = NameTextBox.Text,
                                    author = AuthorTextBox.Text,
                                    cipher_ISBN = CipherISBNTextBox.Text,
                                    classification_BBK = ClassificationBBKComboBox.Text,
                                    publishing = PublishingTextBox.Text,
                                    place_of_publication = PlaceOfPublicationTextBox.Text,
                                    year_of_publication = Convert.ToDateTime(YearOfPublicationDatePicker.SelectedDate),
                                    number_of_pages = NumberOfPagesTextBox.Text,
                                    genre = GenreComboBox.Text,
                                    id_bookbinding = TypeBookbindingComboBox.SelectedIndex + 1,
                                    language = LanguageComboBox.Text,
                                    id_status = 1,
                                    description = DescriptionTextBox.Text,
                                    book_image = bookImage,
                                };
                                db.context.Books.Add(objectBooks);
                                db.context.SaveChanges();
                                this.NavigationService.Navigate(new ViewingBooksPage());
                            }
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
                        Books objectBooks = new Books()
                        {
                            name = NameTextBox.Text,
                            author = AuthorTextBox.Text,
                            cipher_ISBN = CipherISBNTextBox.Text,
                            classification_BBK = ClassificationBBKComboBox.Text,
                            publishing = PublishingTextBox.Text,
                            place_of_publication = PlaceOfPublicationTextBox.Text,
                            year_of_publication = Convert.ToDateTime(YearOfPublicationDatePicker.SelectedDate),
                            number_of_pages = NumberOfPagesTextBox.Text,
                            genre = GenreComboBox.Text,
                            id_bookbinding = TypeBookbindingComboBox.SelectedIndex + 1,
                            language = LanguageComboBox.Text,
                            id_status = 1,
                            description = DescriptionTextBox.Text,
                            book_image = bookImage,
                        };
                        db.context.Books.Add(objectBooks);
                        db.context.SaveChanges();
                        this.NavigationService.Navigate(new ViewingBooksPage());
                    }
                    if (UnvailableStatusRadioButton.IsChecked == true)
                    {
                        Books objectBooks = new Books()
                        {
                            name = NameTextBox.Text,
                            author = AuthorTextBox.Text,
                            cipher_ISBN = CipherISBNTextBox.Text,
                            classification_BBK = ClassificationBBKComboBox.Text,
                            publishing = PublishingTextBox.Text,
                            place_of_publication = PlaceOfPublicationTextBox.Text,
                            year_of_publication = Convert.ToDateTime(YearOfPublicationDatePicker.SelectedDate),
                            number_of_pages = NumberOfPagesTextBox.Text,
                            genre = GenreComboBox.Text,
                            id_bookbinding = TypeBookbindingComboBox.SelectedIndex + 1,
                            language = LanguageComboBox.Text,
                            id_status = 1,
                            description = DescriptionTextBox.Text,
                            book_image = bookImage,
                        };
                        db.context.Books.Add(objectBooks);
                        db.context.SaveChanges();
                        this.NavigationService.Navigate(new ViewingBooksPage());
                    }
                }
            }
        }

        /// <summary>
        /// Удаление обложки.
        /// </summary>
        private void DelImageButton_Click(object sender, RoutedEventArgs e)
        {
            bookImage = null;
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

        /// <summary>
        /// Загрузка обложки.
        /// </summary>
        private void UploadImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            if (fileDialog.ShowDialog() == true)
            {
                byte[] convertImageToByte = File.ReadAllBytes(fileDialog.FileName);
                bookImage = convertImageToByte;

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
    }
}
