using KursIgimbaev.model;
using KursIgimbaev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeDataProviderLib
{
    public class FakeDataProvider : IDataProvider

    {
        Product[] ProductArray = new Product[]
        {
            new Product{id = 109, FullName = "йцуй",Weight = "20", Price = 123, Image = "Фотка", ImageView  = null, CurrentProductType = ProductTypeArray[0]},
            new Product{id =110, FullName = "В тесте", Weight = "25", Price = 111, Image = "Фоточки", ImageView = null, CurrentProductType = ProductTypeArray[1]},
            new Product{id =110, FullName = "Пирожок", Weight = "50", Price = 612, Image = "Фоточка", ImageView = null, CurrentProductType = ProductTypeArray[2]},
        };
        static ProductType[] ProductTypeArray = new ProductType[]
        {
            new ProductType{ID = 111, Name = "Такой"},
            new ProductType{ID =123, Name = "сякой"},
            new ProductType{ID = 133, Name = "И такой"}
        };
        public void DeleteProduct(Product DelProduct)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProduct()
        {
            return ProductArray;
        }

        public IEnumerable<ProductType> GetProductTypes()
        {
            return ProductTypeArray;
        }

        public void SaveProduct(Product ChangeProduct)
        {
            throw new NotImplementedException();
        }

        public void SetAveragePrice(List<int> ProductId, decimal NewCost)
        {
            throw new NotImplementedException();
        }
    }
}
