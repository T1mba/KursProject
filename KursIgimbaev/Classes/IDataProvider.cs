using KursIgimbaev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursIgimbaev.Classes
{
    public interface IDataProvider
    {
        void SaveProduct(Product ChangeProduct);
        IEnumerable<ProductType> GetProductTypes();
        IEnumerable<Product> GetProduct();
        void SetAveragePrice(List<int> ProductId, decimal NewCost);
    }
}
