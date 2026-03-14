using MidasBank.ModelEF;
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

namespace MidasBank
{
    /// <summary>
    /// Логика взаимодействия для AccountsPage.xaml
    /// </summary>
    public partial class AccountsPage : UserControl
    {
        BankDigitalDBEntities db = new BankDigitalDBEntities();

        public AccountsPage()
        {
            InitializeComponent();
            // Загружаем счета вместе с данными о клиентах (через навигационное свойство)
            RefreshData();
        }

        public void RefreshData()
        {
            // .Include("Clients") позволяет сразу увидеть ФИО владельца счета
            var accountsList = db.Accounts.Include("Clients").Include("AccountTypes").ToList();
            DgAccounts.ItemsSource = accountsList;
        }
    }
}