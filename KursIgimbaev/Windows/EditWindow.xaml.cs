
using KursIgimbaev.model;
using KursIgimbaev.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    /// Класс окна для создания нового либо изменения существующего продукта
    public partial class EditWindow : Window, INotifyPropertyChanged
    {
        public IEnumerable<ProductType> ProductTypes { get; set; }
      
        public Product CurrentProduct { get; set; }
        public string WindowName
        {
            get
            {
                return CurrentProduct.id == 0 ? "Новый продукт" : "Редактирование продукта";
            }
        }
        //Констуктор окна
        public EditWindow(Product EditProduct)
        {
            InitializeComponent();
            DataContext = this;
            CurrentProduct = EditProduct;
            ProductTypes = Globals.DataProvider.GetProductTypes();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void Invalidate(string ComponentName = "ProductList")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(ComponentName));
        }

        private void ProductTypeFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        // Кнопка изменения фотографии
        private void ChangePhotoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog GetImageDialog = new OpenFileDialog();
            GetImageDialog.Filter = "Файлы изображений:  (*.png, *.jpg)|*.png;*.jpg";
            GetImageDialog.InitialDirectory = Environment.CurrentDirectory;
            if(GetImageDialog.ShowDialog() == true)
            {
                CurrentProduct.Image = GetImageDialog.FileName.Substring(Environment.CurrentDirectory.Length);
                Invalidate();
            }
        }
        // Кнопка сохранения 
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                Globals.DataProvider.SaveProduct(CurrentProduct);
                DialogResult = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
