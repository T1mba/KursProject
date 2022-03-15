using KursIgimbaev.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KursIgimbaev.Classes
{
    public class MySqlDataProvider : IDataProvider
    {
        private MySqlConnection Connection;
        public MySqlDataProvider()
        {
            try
            {
                Connection = new MySqlConnection("Server=kolei.ru;Database=tigimbaev;port=3306;UserId=tigimbaev;password=020703;");
                
            }
            catch (Exception)
            {

            }
        }
        public IEnumerable<Product> GetProduct()
        {
            List<Product> ProductList = new List<Product>();
            string Query = @"SELECT
                            p.*,
                        p.Category AS ProductTypeName
                        FROM Tg_Product p
                        LEFT JOIN
                        Tg_ProductType ON p.Category = p.ID;";
            try
            {
                Connection.Open();
                try
                {
                    MySqlCommand Command = new MySqlCommand(Query, Connection);
                    MySqlDataReader Reader = Command.ExecuteReader();
                    while (Reader.Read())
                    {
                        Product NewProduct = new Product();
                        NewProduct.id = Reader.GetInt32("id");
                        NewProduct.FullName = Reader.GetString("FullName");
                        NewProduct.Weight = Reader.GetString("Weight");
                        NewProduct.Price = Reader.GetDecimal("Price");
                        NewProduct.Category = Reader.GetString("Category");
                        NewProduct.Image = Reader.GetString("Image");
                        //NewProduct.CurrentProductType = GetProductType(Reader.GetInt32("ProductTypeID"));
                        ProductList.Add(NewProduct);
                    }
                }
                finally
                {
                    Connection.Close();
                }
            }
            catch (Exception)
            {

            }
            return ProductList;
        }
        private List<ProductType> ProductTypes = null;
        private ProductType GetProductType(int Id)
        {

            return ProductTypes.Find(pt => pt.ID == Id);
        }

        public IEnumerable<ProductType> GetProductTypes()
        {
           if (ProductTypes == null) { 
                ProductTypes = new List<ProductType>();
                string Query = "SELECT * FROM Tg_ProductType";
                try
                {
                    Connection.Open();
                    try
                    {
                        MySqlCommand Command = new MySqlCommand(Query, Connection);
                        MySqlDataReader Reader = Command.ExecuteReader();

                        while (Reader.Read())
                        {
                            ProductType NewProductType = new ProductType();
                            NewProductType.ID = Reader.GetInt32("ID");
                            NewProductType.Name = Reader.GetString("Name");

                            ProductTypes.Add(NewProductType);
                        }
                    }
                    finally
                    {
                        Connection.Close();
                    }
                }
                catch (Exception)
                {
                }
            }

            return ProductTypes;
        }

        public void SetAveragePrice(List<int> ProductId, decimal NewCost)
        {
            try
            {
                Connection.Open();
                try
                {
                    string Query = @"UPDATE Tg_Product
                    SET Price = @Price WHERE id=@id";
                    foreach(int item in ProductId)
                    {
                        MySqlCommand Command = new MySqlCommand(Query, Connection);
                        Command.Parameters.AddWithValue("@Price", NewCost);
                        Command.Parameters.AddWithValue("@id", item);
                        Command.ExecuteNonQuery();
                    }
                    


                }
                finally
                {
                    Connection.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SaveProduct(Product ChangeProduct)
        {
            Connection.Open();
            try
            {
                if(ChangeProduct.id == 0)
                {
                    string Query = @"INSERT INTO Tg_Product
                       (FullName,
                        Weight,
                        Price,
                        Category,
                        Image)
                        VALUES
                        (@FullName,
                         @Weight,
                           @Price,
                            @Category,
                            @Image)";
                    MySqlCommand Command = new MySqlCommand(Query,Connection);
                    Command.Parameters.AddWithValue(@"FullName", ChangeProduct.FullName);
                    Command.Parameters.AddWithValue(@"Weight", ChangeProduct.Weight);
                    Command.Parameters.AddWithValue(@"Price",ChangeProduct.Price);
                    Command.Parameters.AddWithValue(@"Category", ChangeProduct.Category);
                    Command.Parameters.AddWithValue(@"Image", ChangeProduct.Image);
                    Command.ExecuteNonQuery();
                }
                else
                {
                    string Query = @"UPDATE Tg_Product
                            SET
                        FullName = @FullName,
                        Weight = @Weight,
                        Price = @Price,
                        Category = @Category,
                        Image = @Image,
           
                        WHERE id = @id";
                    MySqlCommand Command = new MySqlCommand(Query, Connection);
                    Command.Parameters.AddWithValue(@"FullName", ChangeProduct.FullName);
                    Command.Parameters.AddWithValue(@"Weight", ChangeProduct.Weight);
                    Command.Parameters.AddWithValue(@"Price", ChangeProduct.Price);
                    Command.Parameters.AddWithValue(@"Category", ChangeProduct.Category);
                    Command.Parameters.AddWithValue(@"Image", ChangeProduct.Image);
                    Command.ExecuteNonQuery();
                }
                
            }
            finally
            {
                Connection.Close();
            }
        }
    }
}
