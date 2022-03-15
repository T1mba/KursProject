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

namespace KursIgimbaev.Windows
{
    /// <summary>
    /// Логика взаимодействия для ChangePriceWindow.xaml
    /// </summary>
    public partial class ChangePriceWindow : Window
    {
        public ChangePriceWindow(decimal AvgCost)
        {
            InitializeComponent();
            CostTextBox.Text = AvgCost.ToString();
        }
        public decimal Result;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Result = Convert.ToDecimal(CostTextBox.Text);
                DialogResult = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Стоимость должна быть числом");
            }
        }
    }
}
