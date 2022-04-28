
using KursIgimbaev.model;
using KursIgimbaev.Models;
using KursIgimbaev.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KursIgimbaev
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// Класс главного окна

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        // Лист типов продукта
                public List<ProductType> ProductTypeList { get; set; }
        private IEnumerable<Product> _ProductList = null;

        public Product DelProduct = new Product();
        // Лист продуктов
        public IEnumerable<Product> ProductList
        {
            // Реализация поиска/фильтрации/сортировки
            get
            {
                var Result = _ProductList;
                if (ProductTypeFilterid > 0)
                    Result = Result.Where(p => p.CurrentProductType.ID == ProductTypeFilterid);
                switch (SortType)
                {
                    case 1:
                        Result = Result.OrderBy(p => p.FullName);
                        break;

                        case 2:
                        Result = Result.OrderByDescending(p => p.FullName);
                        break;
                        
                        case 3:
                        Result = Result.OrderByDescending(p => p.Weight);
                        break;
                        case 4:
                        Result = Result.OrderBy(p => p.Weight);
                        break;
                    case 5:
                        Result = Result.OrderByDescending(p => p.Price);
                        break;
                    case 6:
                        Result = Result.OrderBy(p => p.Price);
                        break;
                        
                }
                
                // ищем вхождение строки фильтра в названии и описании объекта без учета регистра
                if (SearchFilter != "")
                    Result = Result.Where(
                        p => p.FullName.IndexOf(SearchFilter, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        (p.CurrentProductType!=null && p.CurrentProductType.Name.IndexOf(SearchFilter, StringComparison.OrdinalIgnoreCase) >= 0)


                    );


                return Result;
            }
            set
            {
                _ProductList = value;
                Invalidate();
            }
        }
        // Конструктор главного окна
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            // Получение данных в главное окно
            Globals.DataProvider = new MySqlDataProvider();
            ProductList = Globals.DataProvider.GetProduct();
            ProductTypeList = Globals.DataProvider.GetProductTypes().ToList();
            ProductTypeList.Insert(0, new ProductType { Name = "Все типы" });
            
        }

        

        public event PropertyChangedEventHandler PropertyChanged;
        // Метод, говорящий о том что лист продуктов изменился
        private void Invalidate(string ComponentName = "ProductList")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(ComponentName));
        }
        private string SearchFilter = "";
       
        // Реализация TextBox`a для поиска продукта
        private void SearchFilterTextBox_KeyUp_1(object sender, KeyEventArgs e)
        {
            SearchFilter = SearchFilterTextBox.Text;
            Invalidate();
        }
        // Массив значений, по которым список продуктов будет сортироваться
        public string[] SortList { get; set; } =
        {
            "Без сортировки",
            "Название по убыванию",
            "Название по возрастанию",
            "Вес по убыванию",
            "Вес по возростанию",
            "Цена по убыванию",
            "Цена по возрастанию" };
        // Реализация ComboBox`a для сортировки продуктов
        private int SortType = 0;
        private void SortTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortType = SortTypeComboBox.SelectedIndex;
            Invalidate();
        }
        public int ProductTypeFilterid = 0;
        // Реализация ComboBox`а для фильтрации продуктов по их типу
        private void ProductTypeFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductTypeFilterid = (ProductTypeFilter.SelectedItem as ProductType).ID;
            Invalidate();
        }
        private int ProductSelectedCount = 0;
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductSelectedCount = ProductListView.SelectedItems.Count;
            Invalidate("PriceChangeButtonVisible");
        }
        // Метод появления кнопки, если выбран продукт
        public string PriceChangeButtonVisible
        {
            get
            {
                if (ProductSelectedCount > 0) return "Visible";
                return "Collapsed";
            }
        }
        // Кнопка изменении цены
        private void PriceChangeButton_Click(object sender, RoutedEventArgs e)
        {
            decimal Sum = 0;
            List<int> idList = new List<int>();
            foreach(Product item in ProductListView.SelectedItems)
            {
                Sum += item.Price;
                idList.Add(item.id);
            }
            var NewWindow = new ChangePriceWindow(Sum / ProductListView.SelectedItems.Count);
            if ((bool)NewWindow.ShowDialog())
            {
                Globals.DataProvider.SetAveragePrice(idList, NewWindow.Result);
                ProductList = Globals.DataProvider.GetProduct();
            }

        }
       
        // Метод двойного нажатия по продукту
        private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var NewEditWindow = new EditWindow(ProductListView.SelectedItem as Product);
            if ((bool)NewEditWindow.ShowDialog())
            {
                ProductList = Globals.DataProvider.GetProduct();
            }
        }
        // Кнопка для создания нового продукта
        private void EditNewProduct_Click(object sender, RoutedEventArgs e)
        {
            var NewEditWindow = new EditWindow(new Product());
            if ((bool)NewEditWindow.ShowDialog())
            {
                ProductList =  Globals.DataProvider.GetProduct();
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
           
        }
        // Кнопка удаления продукта
        private void DeleteProduct_Click_1(object sender, RoutedEventArgs e)
        {
            var id = Convert.ToInt32((sender as Button).Tag.ToString());
            foreach (var DelProduct in ProductList)
            {
                if (DelProduct.id == id)
                {
                    Globals.DataProvider.DeleteProduct(DelProduct);
                    ProductList = Globals.DataProvider.GetProduct();
                    break;
                 
                }
            }
            
        }
    }
}
