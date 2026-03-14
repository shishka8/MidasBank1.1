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
    /// Логика взаимодействия для TransactionsPage.xaml
    /// </summary>
    public partial class TransactionsPage : UserControl
    {
        // Подключаем базу данных
        BankDigitalDBEntities db = new BankDigitalDBEntities();

        public TransactionsPage()
        {
            InitializeComponent();
            LoadAccounts();
        }

        // Загружаем список счетов в выпадающие списки (ComboBox)
        private void LoadAccounts()
        {
            var accounts = db.Accounts.ToList();
            cbSourceAccount.ItemsSource = accounts;
            cbTargetAccount.ItemsSource = accounts;
        }

        private void BtnTransfer_Click(object sender, RoutedEventArgs e)
        {
            // 1. Проверяем, выбраны ли счета
            if (cbSourceAccount.SelectedItem == null || cbTargetAccount.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите оба счета для проведения операции.", "Midas Bank Security");
                return;
            }

            // 2. Проверяем сумму
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Введите корректную сумму перевода (положительное число).", "Midas Bank Security");
                return;
            }

            // Получаем объекты счетов из ComboBox
            var source = cbSourceAccount.SelectedItem as Accounts;
            var target = cbTargetAccount.SelectedItem as Accounts;

            // 3. Проверка: нельзя перевести самому себе
            if (source.AccountID == target.AccountID)
            {
                MessageBox.Show("Операция невозможна: счета отправителя и получателя совпадают.", "Midas Bank Security");
                return;
            }

            // 4. Проверка: хватает ли денег
            if (source.Balance < amount)
            {
                MessageBox.Show($"Недостаточно средств. Текущий баланс: {source.Balance} Gold", "Midas Bank Security");
                return;
            }

            try
            {
                // ИЗМЕНЕНИЕ ДАННЫХ
                source.Balance -= amount; // Списываем
                target.Balance += amount; // Зачисляем

                // Сохраняем изменения в SQL Server
                db.SaveChanges();

                MessageBox.Show("Транзакция успешно завершена!", "Midas Bank Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Очищаем поле ввода и обновляем списки (чтобы увидеть новый баланс)
                txtAmount.Clear();
                LoadAccounts();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Критическая ошибка при проведении транзакции: " + ex.Message);
            }
        }
    }
}