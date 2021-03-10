using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Annuaire.Classes;
using Microsoft.AspNetCore.Mvc;

namespace AnnuaireAspNetMVC.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            //ViewData["contacts"] = Contact.GetContacts();
            ViewBag.Contacts= Contact.GetContacts();
            return View();
        }

        public IActionResult FormContact()
        {
            return View();
        }
    }
}