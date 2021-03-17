using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class ProduitController : Controller
    {
        private IWebHostEnvironment _env;

        public ProduitController(IWebHostEnvironment env)
        {
            _env = env;
        }
        [HttpGet]
        public IActionResult FormProduit(string message, string typeMessage)
        {
            ViewBag.Message = message;
            ViewBag.TypeMessage = typeMessage;
            return View(Categorie.GetCategories());
        }

        [HttpPost] 
        public IActionResult SubmitFormProduit([Bind("Titre, Prix, Description, CategoryId")] Produit produit, IFormFile[] images)
        {
            string message, typeMessage;
            if(produit.Titre != null && produit.Description != null && produit.CategoryId > 0 && produit.Prix > 0)
            {
                if(produit.Save())
                {
                    //Enregister les images
                    for(int i = 0; i < images.Length; i++)
                    {
                        Image image = new Image() { Url = Upload(images[i], produit.Id, i) };
                        image.Save(produit.Id);
                    }
                    message = "produit ajouté";
                    typeMessage = "success";
                }
                else
                {
                    message = "Erreur d'insertion du produit dans la base de données";
                    typeMessage = "danger";
                }
            }
            else
            {
                message = "Merci de remplir la totalité des champs";
                typeMessage = "danger";
            }
            return RedirectToAction("FormProduit", new { message = message, typeMessage = typeMessage });

        }

        private string Upload(IFormFile image, int produitId, int numero)
        {
            string filePath = Path.Combine(_env.WebRootPath, "images", $"{produitId}-{numero}-{image.FileName}");
            Stream stream = System.IO.File.Create(filePath);
            image.CopyTo(stream);
            stream.Close();
            return $"images/{produitId}-{numero}-{image.FileName}";
        }
    }
}