using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace coursAspNetMVC.Controllers
{
    public class AccueilController : Controller
    {

        public string ActionBonjour()
        {
            return "Bonjour tout le monde";
        }
        
        public IActionResult Index()
        {
            return new ContentResult() { Content = "<h1>Index Accueil Controller</h1>", ContentType="text/html" };
        }

        public IActionResult First()
        {
            //Return view First => dans le dossier Accueil => Views
            //return View();
            //Return view Index => dans le dossier Accueil => Views
            //return View("Index");
            //Return view Index => dans le dossier Accueil => Views, ~=> racine projet
            return View("~/Views/Accueil/Index.cshtml");
        }
    }
}