using Ecommerce.Tools;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Produit
    {
        private int id;
        private string titre;
        private decimal prix;
        private string description;
        private int categoryId;
        List<Image> images;
        private static string request;
        private static SqlCommand command;
        private static SqlDataReader reader;
        private static SqlConnection connection;
        public int Id { get => id; set => id = value; }
        public string Titre { get => titre; set => titre = value; }
        public decimal Prix { get => prix; set => prix = value; }
        public string Description { get => description; set => description = value; }
        public int CategoryId { get => categoryId; set => categoryId = value; }
        public List<Image> Images { get => images; set => images = value; }


        public bool Save()
        {
            request = "INSERT INTO produit (titre, description, prix, categorie_id) " +
                "output inserted.id values (@titre, @description, @prix, @categorie_id)";
            connection = Connection.New;
            command = new SqlCommand(request, connection);
            command.Parameters.Add(new SqlParameter("@titre", Titre));
            command.Parameters.Add(new SqlParameter("@description", Description));
            command.Parameters.Add(new SqlParameter("@prix", Prix));
            command.Parameters.Add(new SqlParameter("@categorie_id", CategoryId));
            connection.Open();
            Id = (int)command.ExecuteScalar();
            command.Dispose();
            connection.Close();
            return Id > 0;
        }
        
        public static List<Produit> GetProduitsByCategories(int categoryId)
        {
            //Récupérer les produits
            List<Produit> liste = new List<Produit>();
            request = "SELECT id,prix, titre FROM produit ";
            if(categoryId > 0)
            {
                request += "where categorie_id = @category_id";
            }
            connection = Connection.New;
            command = new SqlCommand(request, connection);
            if(categoryId > 0)
            {
                command.Parameters.Add(new SqlParameter("@category_id", categoryId));
            }
            connection.Open();
            reader = command.ExecuteReader();
            while(reader.Read())
            {
                Produit p = new Produit()
                {
                    Id = reader.GetInt32(0),
                    Prix = reader.GetDecimal(1),
                    Titre = reader.GetString(2),
                };
                liste.Add(p);
            }
            reader.Close();
            command.Dispose();
            connection.Close();
            liste.ForEach(p =>
            {
                p.Images = Image.GetImages(p.Id);
            });
            return liste;
        }
    }
}
