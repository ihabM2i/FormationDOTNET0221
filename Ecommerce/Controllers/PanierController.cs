using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Ecommerce.Controllers
{
    public class PanierController : Controller
    {
        public IActionResult Index()
        {
            Panier panier = GetPanierFromSession();
            return View(panier);
        }


        public IActionResult AjouterAuPanier(int id)
        {
            Panier panier = GetPanierFromSession();
            bool nouveau = true;
            foreach(ProduitPanier p in panier.Produits)
            {
                if(p.Produit.Id == id)
                {
                    p.Qty += 1;
                    nouveau = false;
                    break;
                }
            }
            if(nouveau)
            {
                panier.Produits.Add(new ProduitPanier() { Qty = 1, Produit = Produit.GetProduit(id) });
            }
            SetPanierToSession(panier);
            return RedirectToAction("Index");
        }

        private Panier GetPanierFromSession()
        {
            Panier panier;
            string panierChaine = HttpContext.Session.GetString("panier");
            if(panierChaine != null)
            {
                panier = JsonConvert.DeserializeObject<Panier>(panierChaine);
            }else
            {
                panier = new Panier();
            }
            return panier;
        }

        private void SetPanierToSession(Panier panier)
        {
            HttpContext.Session.SetString("panier", JsonConvert.SerializeObject(panier));
        }
    }
}