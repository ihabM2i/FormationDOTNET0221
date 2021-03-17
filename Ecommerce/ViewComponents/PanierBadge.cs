using Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.ViewComponents
{
    public class PanierBadge : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            int nb = 0;
            GetPanierFromSession().Produits.ForEach(p =>
            {
                nb += p.Qty;
            });
            ViewBag.Nombre = nb;
            return View();
        }
        private Panier GetPanierFromSession()
        {
            Panier panier;
            string panierChaine = HttpContext.Session.GetString("panier");
            if (panierChaine != null)
            {
                panier = JsonConvert.DeserializeObject<Panier>(panierChaine);
            }
            else
            {
                panier = new Panier();
            }
            return panier;
        }
    }
}
