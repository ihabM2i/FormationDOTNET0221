using Ecommerce.Tools;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Panier
    {
        private int id;
        private int utilisateurId;
        private List<ProduitPanier> produits;

        private DateTime dateAchat;
        private static string request;
        private static SqlCommand command;
        private static SqlDataReader reader;
        private static SqlConnection connection;
        public int Id { get => id; set => id = value; }
        public int UtilisateurId { get => utilisateurId; set => utilisateurId = value; }
        public List<ProduitPanier> Produits { get => produits; set => produits = value; }
        public decimal Total
        {
            get
            {
                decimal total = 0;
                Produits.ForEach(p =>
                {
                    total += p.Qty * p.Produit.Prix;
                });
                return total;
            }
        }
        public DateTime DateAchat { get => dateAchat; set => dateAchat = value; }

        public Panier()
        {
            Produits = new List<ProduitPanier>();
        }


        public bool Save(int utilisateurId)
        {
            bool retour = false;
            dateAchat = DateTime.Now;
            connection = Connection.New;
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                request = "INSERT INTO Panier (total, utilisateur_id, date_achat) output inserted.id " +
                    "values (@total, @utilisateur_id,@date_achat)";
                command = new SqlCommand(request, connection, transaction);
                command.Parameters.Add(new SqlParameter("@total", Total));
                command.Parameters.Add(new SqlParameter("@utilisateur_id", utilisateurId));
                command.Parameters.Add(new SqlParameter("@date_achat", DateAchat));
                Id = (int)command.ExecuteScalar();
                command.Dispose();
                foreach(ProduitPanier p in Produits)
                {
                    request = "INSERT INTO produit_panier (panier_id, produit_id, qty) values (@panier_id, @produit_id, @qty)";
                    command = new SqlCommand(request, connection, transaction);
                    command.Parameters.Add(new SqlParameter("@panier_id", Id));
                    command.Parameters.Add(new SqlParameter("@produit_id", p.Produit.Id));
                    command.Parameters.Add(new SqlParameter("@qty", p.Qty));
                    command.ExecuteNonQuery();
                    command.Dispose();
                }
                transaction.Commit();
                retour = true;
            }
            catch(Exception ex)
            {
                transaction.Rollback();
            }
            connection.Close();
            return retour;
        }
    }
}
