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
                Connection = new MySqlConnection("Server=home.kolei.ru;Database=tigimbaev;port=3306;UserId=tigimbaev;password=020703;");
                
            }
            catch (Exception)
            {

            }
        }
        public IEnumerable<Product> GetProduct()
        {
            GetProductTypes();

            List<Product> ProductList = new List<Product>();
            string Query = @"SELECT p.*, pt.`Name`
                        FROM Tg_Product p
                        LEFT JOIN
                        Tg_ProductType pt ON p.CategoryId = pt.ID;";
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
                        NewProduct.Image = Reader["Image"].ToString();
                        
                        NewProduct.CurrentProductType = GetProductType(Reader.GetInt32("CategoryId"));
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
                        CategoryId,
                        Image)
                        VALUES
                        (@FullName,
                         @Weight,
                           @Price,
                            @CategoryId,
                            @Image)";
                    MySqlCommand Command = new MySqlCommand(Query,Connection);
                    Command.Parameters.AddWithValue("@FullName", ChangeProduct.FullName);
                    Command.Parameters.AddWithValue("@Weight", ChangeProduct.Weight);
                    Command.Parameters.AddWithValue("@Price",ChangeProduct.Price);
                    Command.Parameters.AddWithValue("@CategoryId", ChangeProduct.CurrentProductType.ID);
                    Command.Parameters.AddWithValue("@Image", ChangeProduct.Image);
                  
                    Command.ExecuteNonQuery();
                }
                else
                {
                    string Query = @"UPDATE Tg_Product
                            SET
                                FullName = @FullName,
                                Weight = @Weight,
                                Price = @Price,
                                CategoryId = @CategoryId,
                                Image = @Image
                            WHERE 
                                id = @id";
                    MySqlCommand Command = new MySqlCommand(Query, Connection);
                    Command.Parameters.AddWithValue("@FullName", ChangeProduct.FullName);
                    Command.Parameters.AddWithValue("@Weight", ChangeProduct.Weight);
                    Command.Parameters.AddWithValue("@Price", ChangeProduct.Price);
                    Command.Parameters.AddWithValue("@CategoryId", ChangeProduct.CurrentProductType.ID);
                    Command.Parameters.AddWithValue("@Image", ChangeProduct.Image);
                    Command.Parameters.AddWithValue("@id", ChangeProduct.id);
                    Command.ExecuteNonQuery();
                }
                
            }
            finally
            {
                Connection.Close();
            }
        }

        public void DeleteProduct(Product DelProduct)
        {
            if (DelProduct.id == 0)
                MessageBox.Show("ID = 0");
            else
            {
                try
                {




                    Connection.Open();
                    try
                    {

                        string Query = "DELETE FROM Tg_Product WHERE id=@id";
                        MySqlCommand Command = new MySqlCommand(Query, Connection);
                        Command.Parameters.AddWithValue("@id", DelProduct.id);
                        Command.ExecuteNonQuery();




                    }
                    finally
                    {
                        Connection.Close();
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
         
        }
    }
}
