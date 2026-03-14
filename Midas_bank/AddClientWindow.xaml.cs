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
using System.Windows.Shapes;

namespace MidasBank
{
    /// <summary>
    /// Логика взаимодействия для AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        // Подключаем контекст базы данных
        BankDigitalDBEntities db = new BankDigitalDBEntities();

        public AddClientWindow()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text) || string.IsNullOrWhiteSpace(txtPassport.Text))
            {
                MessageBox.Show("Заполните основные поля!");
                return;
            }

            try
            {
                // 1. Создаем клиента
                Clients newClient = new Clients()
                {
                    FullName = txtFullName.Text,
                    PassportData = txtPassport.Text,
                    Phone = txtPhone.Text,
                    Email = txtEmail.Text
                };

                db.Clients.Add(newClient);
                db.SaveChanges(); // Сохраняем, чтобы получить ClientID

                // 2. АВТОМАТИЗАЦИЯ: Сразу открываем ему счет
                Accounts newAccount = new Accounts()
                {
                    ClientID = newClient.ClientID, // Привязываем к новому клиенту
                    TypeID = 1, // 1 - это 'Текущий' (мы добавляли это в SQL)
                    AccountNumber = "MIDAS-" + new Random().Next(100000, 999999).ToString(), // Генерируем номер
                    Balance = 1000.00m, // Подарочный баланс от банка Midas
                    OpenDate = DateTime.Now
                };

                db.Accounts.Add(newAccount);
                db.SaveChanges();

                MessageBox.Show($"Клиент зарегистрирован! Открыт счет: {newAccount.AccountNumber}", "Midas Bank");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}