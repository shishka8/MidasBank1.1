using Microsoft.Win32;
using MidasBank.ModelEF;
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

namespace MidasBank
{
    public partial class ReportsPage : UserControl
    {
        BankDigitalDBEntities db = new BankDigitalDBEntities();

        public ReportsPage()
        {
            InitializeComponent();
        }

        private void BtnClientsReport_Click(object sender, RoutedEventArgs e)
        {
            // 1. Готовим данные
            var clients = db.Clients.ToList();

            // Заголовок таблицы
            StringBuilder csvContent = new StringBuilder();
            csvContent.AppendLine("ID;ФИО;Паспорт;Телефон;Email");

            // Заполняем строками
            foreach (var c in clients)
            {
                csvContent.AppendLine($"{c.ClientID};{c.FullName};{c.PassportData};{c.Phone};{c.Email}");
            }

            SaveReport(csvContent.ToString(), "Clients_Report");
        }

        private void BtnTransReport_Click(object sender, RoutedEventArgs e)
        {
            var trans = db.Transactions.Include("Accounts").ToList();

            StringBuilder csvContent = new StringBuilder();
            csvContent.AppendLine("Дата;Сумма;Отправитель;Получатель");

            foreach (var t in trans)
            {
                // Тут мы используем навигационные свойства, чтобы вытащить номера счетов
                csvContent.AppendLine($"{t.TransactionDate};{t.Amount};{t.Accounts.AccountNumber};{t.Accounts1.AccountNumber}");
            }

            SaveReport(csvContent.ToString(), "Transactions_History");
        }

        // Общий метод для сохранения файла на компьютер
        private void SaveReport(string content, string defaultName)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV файлы (*.csv)|*.csv";
            sfd.FileName = $"{defaultName}_{DateTime.Now:ddMMyyyy}";

            if (sfd.ShowDialog() == true)
            {
                // Сохраняем с кодировкой UTF-8 для корректного отображения кириллицы
                File.WriteAllText(sfd.FileName, content, Encoding.UTF8);
                MessageBox.Show("Отчет успешно сформирован и сохранен!", "Midas Bank Reports");
            }
        }
    }
}