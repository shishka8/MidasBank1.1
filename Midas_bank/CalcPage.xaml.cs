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
    /// Логика взаимодействия для CalcPage.xaml
    /// </summary>
    public partial class CalcPage : UserControl
    {
        public CalcPage()
        {
            InitializeComponent();
        }
    
    private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double P = double.Parse(txtAmount.Text); // Сумма
                double r = double.Parse(txtRate.Text) / 12 / 100; // Ставка в месяц
                int n = int.Parse(txtMonths.Text); // Срок

                // Формула: M = P * (r * (1 + r)^n) / ((1 + r)^n - 1)
                double monthlyPayment = P * (r * Math.Pow(1 + r, n)) / (Math.Pow(1 + r, n) - 1);

                txtResult.Text = monthlyPayment.ToString("N2") + " Gold";
            }
            catch
            {
                MessageBox.Show("Пожалуйста, введите корректные числа");
            }
        }
    }
}
