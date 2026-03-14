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
    public partial class DashboardPage : UserControl
    {
        BankDigitalDBEntities db = new BankDigitalDBEntities();

        public DashboardPage()
        {
            InitializeComponent();
            UpdateStatistics();
        }

        private void UpdateStatistics()
        {
            // 1. Считаем общую сумму на всех счетах
            // Вариант с явным приведением (самый простой)
            decimal totalGold = db.Accounts.Any() ? (decimal)db.Accounts.Sum(a => a.Balance) : 0;
            txtTotalBalance.Text = totalGold.ToString("N2");

            // 2. Считаем количество клиентов
            int clientsCount = db.Clients.Count();
            txtTotalClients.Text = clientsCount.ToString();

            // 3. Считаем количество транзакций
            int transCount = db.Transactions.Count();
            txtTotalTrans.Text = transCount.ToString();
        }
    }
}