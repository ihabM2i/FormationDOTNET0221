using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class CategorieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //Formulaire pour ajouter une catégorie
        [HttpGet]
        public IActionResult FormCategorie(string message, string typeMessage)
        {
            ViewBag.Message = message;
            ViewBag.TypeMessage = typeMessage;
            return View();
        }

        [HttpPost]
        //Sauvegarde categorie
        public IActionResult SubmitFormCateogrie(Categorie categorie)
        {
            string message, typeMessage;
            if(categorie.Titre != null)
            {
                if(categorie.Save())
                {
                    message = "La catégorie a bien été ajoutée";
                    typeMessage = "success";
                }
                else
                {
                    message = "Erreur d'insertion dans la base de données";
                    typeMessage = "error";
                }
            }
            else
            {
                message = "Merci de remplir le champ titre";
                typeMessage = "danger";
            }
            return RedirectToAction("FormCategorie", new { message = message, typeMessage = typeMessage});
        }
    }
}