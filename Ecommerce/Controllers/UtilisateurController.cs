using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class UtilisateurController : Controller
    {
        public IActionResult Login()
        {
            //Formulaire de login avec le submit vers l'action validLogin
            return View();
        }
        public IActionResult ValidLogin(string email, string password)
        {
            //Rechercher dans la table utilisateur s'il existe un utilisateur avec email avec password
            //Si vrai
            HttpContext.Response.Cookies.Append("login", "true");
            HttpContext.Response.Cookies.Append("userId", "idUtilisateur");
            return View();
        }
    }
}