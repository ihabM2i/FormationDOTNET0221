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

        public IActionResult ValiderPanier()
        {
            if(HttpContext.Request.Cookies["login"] == "true")
            {
                //Validation du panier
                Panier panier = GetPanierFromSession();
                int utilisateurId = Convert.ToInt32(HttpContext.Request.Cookies["userId"]);
                if(panier.Save(utilisateurId))
                {
                    HttpContext.Session.Clear();
                    return View();
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Login", "Utilisateur");
            }
        }

        public IActionResult ChangeQty(int id, string type) 
        {
            Panier panier = GetPanierFromSession();
            panier.Produits.ForEach(p =>
            {
                if (p.Produit.Id == id)
                {
                    if (type == "plus")
                    {
                        p.Qty += 1;
                    }
                    else if (type == "moin")
                    {
                        p.Qty = (p.Qty > 0) ? p.Qty - 1 : 0;
                    }
                }
            });
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