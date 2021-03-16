using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using coursAspNetMVC.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace coursAspNetMVC.Controllers
{
    public class UploadController : Controller
    {
        private IWebHostEnvironment _env;
        
        public UploadController(IWebHostEnvironment env)
        {
            _env = env;
        }
        public IActionResult Index()
        {
            return View(Avatar.GetAll());
        }

        public IActionResult SubmitUpload(string nom, IFormFile avatar)
        {
            //if(avatar.Length > 10000)
            //{
            //    throw new Exception("Large file");
            //}
            //Uploader l'image dans un dossier
            //Chemin complet de sauvegarde de l'image
            //string filePath = @"C:\Users\ihab\source\repos\CoursMCPDNETF\coursAspNetMVC\wwwroot\avatar-" + nom +"-"+avatar.FileName;
            string filePath = Path.Combine(_env.WebRootPath, "avatar","avatar-" + nom +"-"+avatar.FileName);

            //Créer un flux pour sauvegarder l'image => A l'aide de la classe FILE
            Stream stream = System.IO.File.Create(filePath);
            avatar.CopyTo(stream);
            stream.Close();
            //Enregistrer dans la base de données
            string chemin =  "avatar/avatar-" + nom + "-" + avatar.FileName;
            Avatar a = new Avatar()
            {
                Chemin = chemin
            };
            a.Save();
            return RedirectToAction("Index");
        }

        public IActionResult SubmitMultiUpload(string nom, IFormFile[] avatar)
        {
            //if(avatar.Length > 10000)
            //{
            //    throw new Exception("Large file");
            //}
            //Uploader l'image dans un dossier
            //Chemin complet de sauvegarde de l'image
            //string filePath = @"C:\Users\ihab\source\repos\CoursMCPDNETF\coursAspNetMVC\wwwroot\avatar-" + nom +"-"+avatar.FileName;
            
            foreach(IFormFile i in avatar)
            {
                string filePath = Path.Combine(_env.WebRootPath, "avatar", "avatar-" + nom + "-" + i.FileName);

                //Créer un flux pour sauvegarder l'image => A l'aide de la classe FILE
                Stream stream = System.IO.File.Create(filePath);
                i.CopyTo(stream);
                stream.Close();
                //Enregistrer dans la base de données
                string chemin = "avatar/avatar-" + nom + "-" + i.FileName;
                Avatar a = new Avatar()
                {
                    Chemin = chemin
                };
                a.Save();
            }
            
            return RedirectToAction("Index");
        }
    }
}