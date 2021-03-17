using Ecommerce.Tools;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Image
    {
        private int id;
        private string url;
        private static string request;
        private static SqlCommand command;
        private static SqlDataReader reader;
        private static SqlConnection connection;
        public int Id { get => id; set => id = value; }
        public string Url { get => url; set => url = value; }

        public bool Save(int produitId)
        {
            request = "INSERT INTO image (url, produit_id) output inserted.id values(@url,@produit_id)";
            connection = Connection.New;
            command = new SqlCommand(request, connection);
            command.Parameters.Add(new SqlParameter("@url", Url));
            command.Parameters.Add(new SqlParameter("@produit_id", produitId));
            connection.Open();
            Id = (int)command.ExecuteScalar();
            command.Dispose();
            connection.Close();
            return Id > 0;
        }
    }
}
