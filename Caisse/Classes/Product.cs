using Annuaire.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Caisse.Classes
{
    class Product
    {
        private int id;
        private string title;
        private decimal price;
        private int stock;
        //private static int count = 0;

        private static string request;
        private static SqlCommand command;
        private static SqlDataReader reader;
        
        public int Id { get => id;}
        public string Title { get => title; set => title = value; }
        public decimal Price { get => price; set => price = value; }
        public int Stock { get => stock; set => stock = value; }
    

        public Product()
        {
            //id = ++count;
        }

        public Product(int id, string title, decimal price, int stock)
        {
            this.id = id;
            Title = title;
            Price = price;
            Stock = stock;
        }

        public bool Save()
        {
            request = "INSERT INTO product (title, price, stock) OUTPUT INSERTED.id values " +
                "(@title,@price,@stock)";
            command = new SqlCommand(request, DataBase.Connection);
            command.Parameters.Add(new SqlParameter("@title", Title));
            command.Parameters.Add(new SqlParameter("@price", Price));
            command.Parameters.Add(new SqlParameter("@stock", Stock));
            DataBase.Connection.Open();
            id = (int)command.ExecuteScalar();
            command.Dispose();
            DataBase.Connection.Close();
            return id > 0;
        }

        public bool SaveProductOrder(int orderId)
        {
            //sauvegarde le produit achété et on met à jour le stock
            request = "INSERT INTO sale_product (product_id, sale_id) values " +
                "(@product_id, @sale_id); " +
                "UPDATE product set stock=stock-1 where id = @product_id";
            command = new SqlCommand(request, DataBase.Connection);
            command.Parameters.Add(new SqlParameter("@product_id", Id));
            command.Parameters.Add(new SqlParameter("@sale_id", orderId));
            DataBase.Connection.Open();
            int nbRow = command.ExecuteNonQuery();
            command.Dispose();
            DataBase.Connection.Close();
            return nbRow > 0;
        }

        public bool SaveProductOrder(int orderId, SqlTransaction transaction)
        {
            //sauvegarde le produit achété et on met à jour le stock
            request = "INSERT INTO sale_product (product_id, sale_id) values " +
                "(@product_id, @sale_id); " +
                "UPDATE product set stock=stock-1 where id = @product_id";
            command = new SqlCommand(request, DataBase.Connection, transaction);
            command.Parameters.Add(new SqlParameter("@product_id", Id));
            command.Parameters.Add(new SqlParameter("@sale_id", orderId));        
            int nbRow = command.ExecuteNonQuery();
            
            return nbRow > 0;
        }

        public static Product GetProductById(int id)
        {
            Product product = null;
            request = "SELECT id, title, price, stock from product where id=@id";
            command = new SqlCommand(request, DataBase.Connection);
            command.Parameters.Add(new SqlParameter("@id", id));
            DataBase.Connection.Open();
            reader = command.ExecuteReader();
            if(reader.Read())
            {
                product = new Product(reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2), reader.GetInt32(3));
            }
            reader.Close();
            command.Dispose();
            DataBase.Connection.Close();
            return product;
        }
        public override string ToString()
        {
            return $"Id : {Id}, Titre : {Title}, Prix : {Price}";
        }
    }
}
