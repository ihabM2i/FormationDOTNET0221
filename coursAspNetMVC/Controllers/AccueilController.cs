using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coursAspNetMVC.Models;
using coursAspNetMVC.ViewModels;
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

        public IActionResult Personnes()
        {           
            List<Personne> liste = Personne.GetPersonnes();
            List<Adresse> listeAdresses = Adresse.GetAdresses();
            //L'uitilisation du ViewData pour passer les données du controller vers la view
            //ViewData["listesPersonnes"] = liste;
            //L'uitilisation du ViewBag pour passer les données du controller vers la view

            //ViewBag => objet dynamic
            //ViewBag.ListePersonnes = liste;

            //Un objet => à la vue, un model de vue != model MVC
            PersonneAdresseViewModel vModel = new PersonneAdresseViewModel();
            vModel.Adresses = listeAdresses;
            vModel.Personnes = liste;
            return View(vModel);
        }

        public IActionResult DetailPersonne(string nom, int id)
        {
            ViewBag.Nom = nom;
            ViewBag.Id = id;
            return View();
        }

        public IActionResult FormPersonne()
        {
            return View();
        }

        public IActionResult SubmitFormPersonne(string nom, string prenom)
        {
            Personne p = new Personne() { Nom = nom, Prenom = prenom };
            //Ajouter dans une base de données avec la méthode save apr exemple 
            //Si je n'ai pas d'erreurs
            // redirection vers l'action Personnes du même controller, si vers un autre controller
            //return RedirectToAction("Personnes");
            return RedirectToAction("NomAction", "NomDuController");
        }
    }
}