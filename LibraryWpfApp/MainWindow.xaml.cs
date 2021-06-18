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
using LibraryWpfApp.Views;

namespace LibraryWpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Главное окно.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new MainPage());
            PersonalAccountButton.Visibility = Visibility.Collapsed;
            MainMenuButton.Visibility = Visibility.Collapsed;
        }

        //Счетчик состояния окна
        int windowSize = 1;

        /// <summary>
        /// Перемещение окна прогрммы.
        /// </summary>
        private void ToolBarGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /// <summary>
        /// Закрытие окна программы.
        /// </summary>
        private void ExitEllipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Изменение размера окна программы.
        /// </summary>
        private void RollUpEllipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(windowSize == 1)
            {
                windowSize = 2;
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                windowSize = 1;
                this.WindowState = WindowState.Normal;
            }
        }

        /// <summary>
        /// Сворачивание окна программы.
        /// </summary>
        private void FullscreenEllipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Обновление Frame.
        /// </summary>
        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (!MainFrame.CanGoBack)
            {
                BackArrowImage.Visibility = Visibility.Collapsed;
            }
            else
            {
                BackArrowImage.Visibility = Visibility.Visible;
            }
            if(Properties.Settings.Default.idUsers == 0)
            {
                LoginImage.Visibility = Visibility.Visible;
                LogoutImage.Visibility = Visibility.Collapsed;
            }
            else
            {
                LoginImage.Visibility = Visibility.Collapsed;
                LogoutImage.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Переход на страницу входа в аккаунт.
        /// </summary>
        private void LoginImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new LoginPage());
        }

        /// <summary>
        /// Показ кнопок меню.
        /// </summary>
        private void LogoImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PersonalAccountButton.Visibility = Visibility.Visible;
            MainMenuButton.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Сброс Default переменых, при закрытии приложения.
        /// </summary>
        private void Window_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.idUsers = 0;
            Properties.Settings.Default.idRoleUsers = 0;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Выход из аккаунта.
        /// </summary>
        private void LogoutImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Properties.Settings.Default.idUsers = 0;
            Properties.Settings.Default.idRoleUsers = 0;
            Properties.Settings.Default.Save();
            MainFrame.Navigate(new LoginPage());
        }

        /// <summary>
        /// Скрытие кнопок меню.
        /// </summary>
        private void MenuPanelGrid_MouseLeave(object sender, MouseEventArgs e)
        {
                PersonalAccountButton.Visibility = Visibility.Collapsed;
                MainMenuButton.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Переход на страницу аккаунта.
        /// </summary>
        private void PersonalAccountButton_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.idUsers == 0)
            {
                MainFrame.Navigate(new LoginPage());
            }
            else
            {
                MainFrame.Navigate(new PersonalAccountMenuPage());
            }
        }

        /// <summary>
        /// Переход на страницу главного меню.
        /// </summary>
        private void MainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new MainPage());
        }

        /// <summary>
        /// Переход на предыдущую страницу. 
        /// </summary>
        private void BackArrowImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
            }
        }
    }
}
