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
    }
}
