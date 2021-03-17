using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class ProduitPanier
    {
        private Produit produit;
        private int qty;

        public Produit Produit { get => produit; set => produit = value; }
        public int Qty { get => qty; set => qty = value; }
    }
}
