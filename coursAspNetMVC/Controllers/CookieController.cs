using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace coursAspNetMVC.Controllers
{
    public class CookieController : Controller
    {
        public IActionResult Index()
        {
            //Ecrire un cookie
            HttpContext.Response.Cookies.Append("nom", "abadi");
            return View();
        }

        public IActionResult ReadCookie()
        {
            ViewBag.Nom = HttpContext.Request.Cookies["nom"];
            return View();
        }
    }
}