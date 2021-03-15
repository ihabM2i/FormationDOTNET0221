using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAOBanque.Classes;
using Microsoft.AspNetCore.Mvc;

namespace CorrectionCompteBancaireAspNet.Controllers
{
    public class CompteController : Controller
    {
        public IActionResult Index(string message, int? numero)
        {
            Compte compte = null;
            if(message != null)
            {
                ViewBag.Message = message;
            }
            if(numero != null)
            {
                Banque banque = new Banque("banquedefrance");
                compte = banque.RechercherCompte((int)numero);
            }
            return View(compte);
        }

        
        public IActionResult CreationCompte()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitCreationCompte([Bind("Nom,Prenom,Telephone")] Client client, [Bind("Solde")]Compte compte)
        {
            if(client.Nom != null && client.Prenom != null && client.Telephone != null)
            {
                compte.Client = client;
                Banque banque = new Banque("banqueDeFrance");
                banque.CreationCompte(compte);
                if(compte.Numero > 0)
                {
                    return RedirectToAction("Index", new { message = "Compte crée avec le numéro "+ compte.Numero});
                }
                else
                {
                    ViewBag.ErrorMessage = "Merci de création de compte";
                    return View("CreationCompte");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Merci de remplir la totalité des champs";
                return View("CreationCompte");
            }
        }

        public IActionResult DetailCompte(int id)
        {
            Banque banque = new Banque("banquedefrance");
            Compte compte = banque.RechercherCompteEtOperation(id);
            return View(compte);
        }

        public IActionResult FormOperation(int id, string type)
        {
            ViewBag.Numero = id;
            ViewBag.Type = type;
            return View();
        }

        public IActionResult SubmitOperation(int numero, string type, decimal montant)
        {
            ViewBag.Numero = numero;
            ViewBag.Type = type;
            Banque banque = new Banque("banquedefrance");
            Compte compte = banque.RechercherCompteEtOperation(numero);
            if(compte != null)
            {
                bool error = true;
                if(type == "depot")
                {
                    Operation o = new Operation(montant);
                    if(compte.Depot(o))
                    {
                        error = false;
                    }
                }
                else if (type == "retrait")
                {
                    Operation o = new Operation(montant * -1);
                    if (compte.Retrait(o))
                    {
                        error = false;
                    }
                }
                if(!error)
                {
                    return RedirectToAction("DetailCompte", new { id = numero });
                }
                else
                {
                    ViewBag.ErrorMessage = "Opération impossible";
                    return View("FormOperation");
                }
            }
            ViewBag.ErrorMessage = "Aucun compte avec ce numéro";
            return View("FormOperation");
        }
    }
}