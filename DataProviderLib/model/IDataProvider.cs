using KursIgimbaev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursIgimbaev.model
{
    public interface IDataProvider
    {
        // Метод удаления продукта
        void DeleteProduct(Product DelProduct);
        //Метод сохранения продукта
        void SaveProduct(Product ChangeProduct);
        //Метод получения типа продукта
        IEnumerable<ProductType> GetProductTypes();
        // Метод получения продуктов
        IEnumerable<Product> GetProduct();
        // Метод расчёта средней цены
        void SetAveragePrice(List<int> ProductId, decimal NewCost);
    }
}
