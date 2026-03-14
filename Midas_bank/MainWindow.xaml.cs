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
    public partial class MainWindow : Window
    {
        // Создаем связь с базой данных (имя должно совпадать с тем, что ты дал в Шаге 2)
        BankDigitalDBEntities db = new BankDigitalDBEntities();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Берем данные из полей
            string login = txtLogin.Text;
            string pass = txtPassword.Password;

            // Ищем сотрудника в таблице Employees
            // Мы используем FirstOrDefault, чтобы найти первого совпавшего или null
            var user = db.Employees.FirstOrDefault(u => u.Login == login && u.Password == pass);

            if (user != null)
            {
                AppWindow app = new AppWindow();
                app.Show();
                this.Close();
                // В следующем шаге мы создадим AppWindow (основной интерфейс)
                // AppWindow app = new AppWindow();
                // app.Show();
                // this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка авторизации. Неверные учетные данные.", "Midas Bank Security", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}