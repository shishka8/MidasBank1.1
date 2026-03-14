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
using System.Windows.Shapes;

namespace MidasBank
{
    /// <summary>
    /// Логика взаимодействия для AppWindow.xaml
    /// </summary>
    public partial class AppWindow : Window
    {
        public AppWindow()
        {
            InitializeComponent();
            MainContainer.Content = new ClientsPage();
            InitializeComponent();
            MainContainer.Content = new DashboardPage();
        }

        // Логика кнопки КЛИЕНТЫ
        private void BtnClients_Click(object sender, RoutedEventArgs e)
        {
            // MainContainer — это область в XAML, куда мы вставляем страницу
            MainContainer.Content = new ClientsPage();
        }

        // Логика кнопки СЧЕТА (пока пустая страница)
        private void BtnAccounts_Click(object sender, RoutedEventArgs e)
        {
            MainContainer.Content = new AccountsPage();
        }

        // Логика кнопки ТРАНЗАКЦИИ
        private void BtnTrans_Click(object sender, RoutedEventArgs e)
        {
            MainContainer.Content = new TransactionsPage();
        }

        // Логика выхода
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }
        private void BtnDashboard_Click(object sender, RoutedEventArgs e)
        {
            MainContainer.Content = new DashboardPage();
        }
        private void BtnCalcMenu_Click(object sender, RoutedEventArgs e)
        {
            MainContainer.Content = new CalcPage();
        }
        private void BtnReports_Click(object sender, RoutedEventArgs e)
        {
            MainContainer.Content = new ReportsPage();
        }
    }

}
