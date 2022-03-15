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
            string Query = "SELECT * FROM Tg_Product";
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
    }
}
