﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Annuaire.Classes;
using Microsoft.AspNetCore.Mvc;

namespace AnnuaireAspNetMVC.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index(string message)
        {
            //ViewData["contacts"] = Contact.GetContacts();
            ViewBag.Contacts = Contact.GetContacts();
            ViewBag.Message = message;
            return View();
        }

        public IActionResult FormContact()
        {
            return View();
        }

        //public IActionResult SubmitFormContact(string nom, string prenom, string telephone)
        //{
        //    if(nom != null && prenom != null && telephone != null)
        //    {
        //        Contact contact = new Contact()
        //        {
        //            Nom = nom,
        //            Prenom = prenom,
        //            Telephone = telephone
        //        };
        //        if(contact.Save())
        //        {
        //            return RedirectToAction("Index", "Contact", new { message = "Contact ajouté"});
        //        }
        //        else
        //        {
        //            ViewBag.MessageError = "Erreur d'insertion dans la base de données";
        //            return View("FormContact");
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.MessageError = "Merci de remplir la totalité des champs";
        //        return View("FormContact");
        //    }
        //}

        [HttpPost]
        public IActionResult SubmitFormContact([Bind("Nom, Prenom, Telephone")] Contact contact)
        {
            if (contact != null && contact.Nom != null && contact.Prenom != null && contact.Telephone!= null)
            {
                if (contact.Save())
                {
                    return RedirectToAction("Index", "Contact", new { message = "Contact ajouté" });
                }
                else
                {
                    ViewBag.MessageError = "Erreur d'insertion dans la base de données";
                    return View("FormContact");
                }
            }
            else
            {
                ViewBag.MessageError = "Merci de remplir la totalité des champs";
                return View("FormContact");
            }
        }
        public IActionResult DetailContact(int id)
        {
            Contact contact = Contact.GetContactById(id);
            return View(contact);
        }

        public IActionResult Search(string search)
        {
            ViewBag.Contacts = Contact.SearchContacts(search);            
            return View("Index");
        }

        public IActionResult DeleteContact(int id)
        {
            Contact contact = Contact.GetContactById(id);
            if(contact != null && contact.Delete())
            {
                return RedirectToAction("Index", "Contact", new { message = "Contact supprimé" });
            }
            else
            {
                return RedirectToAction("Index", "Contact");
            }
        }
    }
}