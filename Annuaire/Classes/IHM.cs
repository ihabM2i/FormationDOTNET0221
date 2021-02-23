using System;
using System.Collections.Generic;
using System.Text;

namespace Annuaire.Classes
{
    public class IHM
    {
        public void Start()
        {
            string choix;
            do
            {
                Menu();
                choix = Console.ReadLine();
                Console.Clear();
                switch(choix)
                {
                    case "1":
                        ActionAjouterContact();
                        break;
                    case "2":
                        ActionAfficherListeContacts();
                        break;
                    case "3":
                        ActionRechercherContact();
                        break;
                }
            } while(choix != "0");
        }
        private void Menu()
        {
            Console.WriteLine("1--Ajouter un contact");
            Console.WriteLine("2--Afficher la liste des contacts");
            Console.WriteLine("3--Rechercher des contacts");
        }

        private void ActionAjouterContact()
        {
            Contact contact = new Contact();
            Console.WriteLine("Merci de saisir le nom : ");
            contact.Nom = Console.ReadLine();
            Console.WriteLine("Merci de saisir le prénom : ");
            contact.Prenom= Console.ReadLine();
            Console.WriteLine("Merci de saisir le téléphone : ");
            contact.Telephone = Console.ReadLine();
            if(contact.Save())
            {
                Console.WriteLine("Contact ajouté avec id : " + contact.Id);
            }
        }

        private void ActionAfficherListeContacts()
        {
            Console.WriteLine("---La liste des contacts---");
            foreach(Contact c in Contact.GetContacts())
            {
                Console.WriteLine(c);
            }
        }

        private void ActionRechercherContact()
        {
            Console.Write("Merci de saisir la recherche : ");
            string search = Console.ReadLine();
            List<Contact> contacts = Contact.SearchContacts(search);
            if(contacts.Count > 0)
            {
                foreach(Contact c in contacts)
                {
                    Console.WriteLine(c);
                }
            }
            else
            {
                Console.WriteLine("Aucun contact trouvé");
            }
        }
    }
}
