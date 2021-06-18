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
    /// Логика взаимодействия для RegistrationReadersTicketPage.xaml
    /// </summary>
    public partial class RegistrationReadersTicketPage : Page
    {
        Core db = new Core();
        /// <summary>
        /// Страница создания нового читательского билета.
        /// </summary>
        public RegistrationReadersTicketPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Создание читательского билаета и передача данных на следующую страницу регистрации.
        /// </summary>
        private void CreatingReadersTicketButton_Click(object sender, RoutedEventArgs e)
        {
            if(TypeReadersTicketComboBox.Text == String.Empty)
            {
                MessageBox.Show("Выберите тип читательского билета");
            }
            else
            {
                LibraryCardController objectLibraryCardController = new LibraryCardController();
                
                string name = NameTextBox.Text;
                string surname = SurnameTextBox.Text;
                string patronymic = PatronymicTextBox.Text;
                DateTime birthDate = Convert.ToDateTime(BirthDatePicker.SelectedDate);
                string addres = AddresTextBox.Text;
                string placeOfWork = PlaceWorkTextBox.Text;
                string telephone = TelephoneTextBox.Text;
                string codeLibraryCard = objectLibraryCardController.GetCodeReadersTicket(TypeReadersTicketComboBox.Text);

                this.NavigationService.Navigate(new RegistrationUserPage(name, surname, patronymic, birthDate, addres, placeOfWork, telephone, codeLibraryCard));

            } 
        }
    }
}
