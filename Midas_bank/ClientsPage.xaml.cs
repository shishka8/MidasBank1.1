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
    /// Логика взаимодействия для ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : UserControl
    {
        BankDigitalDBEntities db = new BankDigitalDBEntities();
        public ClientsPage()
        {
            InitializeComponent();
            DgClients.ItemsSource = db.Clients.ToList(); // Выводим всех клиентов из базы
        }
    
    private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = SearchBox.Text.ToLower();

            // LINQ-запрос для поиска (как в профессиональных ИС)
            var filtered = db.Clients
                .Where(c => c.FullName.ToLower().Contains(search) ||
                            c.PassportData.Contains(search))
                .ToList();

            DgClients.ItemsSource = filtered;
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            // 1. Создаем экземпляр нашего нового окна
            AddClientWindow addWindow = new AddClientWindow();

            // 2. Указываем владельца окна (чтобы оно открылось красиво поверх главного)
            addWindow.Owner = Window.GetWindow(this);

            // 3. Открываем окно как модальное (пока не закроешь его, в главное не вернешься)
            // Метод ShowDialog возвращает 'true', если в окне мы нажали "Сохранить"
            if (addWindow.ShowDialog() == true)
            {
                // 4. Если сохранение прошло успешно, обновляем таблицу
                // Мы заново просим базу данных дать нам список клиентов
                DgClients.ItemsSource = db.Clients.ToList();
            }
        }
    }
}