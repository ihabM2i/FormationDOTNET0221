using Ecommerce.Tools;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Categorie
    {
        private int id;
        private string titre;
        private static SqlCommand command;
        private static string request;
        private static SqlDataReader reader;
        private static SqlConnection connection;
        public int Id { get => id; set => id = value; }
        public string Titre { get => titre; set => titre = value; }

        public bool Save()
        {
            request = "INSERT INTO categorie (titre) output inserted.id values (@titre)";
            connection = Connection.New;
            command = new SqlCommand(request, connection);
            command.Parameters.Add(new SqlParameter("@titre", Titre));
            connection.Open();
            Id = (int)command.ExecuteScalar();
            command.Dispose();
            connection.Close();
            return Id > 0;
        }

        public static List<Categorie> GetCategories()
        {
            List<Categorie> liste = new List<Categorie>();
            request = "SELECT id, titre from categorie";
            connection = Connection.New;
            command = new SqlCommand(request, connection);
            connection.Open();
            reader = command.ExecuteReader();
            while(reader.Read())
            {
                Categorie c = new Categorie()
                {
                    Id = reader.GetInt32(0),
                    Titre = reader.GetString(1)
                };
                liste.Add(c);
            }
            reader.Close();
            command.Dispose();
            connection.Close();
            return liste;
        }
    }
}
