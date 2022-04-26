using KursIgimbaev.Classes;
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
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public List<ProductType> ProductTypeList { get; set; }
        private IEnumerable<Product> _ProductList = null;
        public Product DelProduct = new Product();
        public IEnumerable<Product> ProductList
        {
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

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Globals.DataProvider = new MySqlDataProvider();
            ProductList = Globals.DataProvider.GetProduct();
            ProductTypeList = Globals.DataProvider.GetProductTypes().ToList();
            ProductTypeList.Insert(0, new ProductType { Name = "Все типы" });
            
        }

        

        public event PropertyChangedEventHandler PropertyChanged;
        private void Invalidate(string ComponentName = "ProductList")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(ComponentName));
        }
        private string SearchFilter = "";
       

        private void SearchFilterTextBox_KeyUp_1(object sender, KeyEventArgs e)
        {
            SearchFilter = SearchFilterTextBox.Text;
            Invalidate();
        }
        public string[] SortList { get; set; } =
        {
            "Без сортировки",
            "Название по убыванию",
            "Название по возрастанию",
            "Вес по убыванию",
            "Вес по возростанию",
            "Цена по убыванию",
            "Цена по возрастанию" };
        private int SortType = 0;
        private void SortTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortType = SortTypeComboBox.SelectedIndex;
            Invalidate();
        }
        public int ProductTypeFilterid = 0;
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
        public string PriceChangeButtonVisible
        {
            get
            {
                if (ProductSelectedCount > 0) return "Visible";
                return "Collapsed";
            }
        }

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
       

        private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var NewEditWindow = new EditWindow(ProductListView.SelectedItem as Product);
            if ((bool)NewEditWindow.ShowDialog())
            {
                ProductList = Globals.DataProvider.GetProduct();
            }
        }

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
