using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace coursAspNetMVC.Controllers
{
    public class SessionController : Controller
    {
        public IActionResult Index()
        {
            //Ecrire des données dans la session
            HttpContext.Session.SetString("nom", "abadi");
            //Ecrire un objet dans une session
            List<string> liste = new List<string>() { "toto", "tata", "titi" };
            //Sérialisation d'un objet en chaine de caractères => json, on utilise le package nuget newtonsoft json
            HttpContext.Session.SetString("liste", JsonConvert.SerializeObject(liste));
            return View();
        }

        public IActionResult ReadSession()
        {
            ViewBag.Nom = HttpContext.Session.GetString("nom");
            //Lire un objet à partir d'une session en utilisant le package nuget newton soft et sous format json
            List<string> liste = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("liste"));
            return View(liste);
        }
    }
}