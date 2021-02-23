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
                    case "4":
                        ActionSupprimerContact();
                        break;
                }
            } while(choix != "0");
        }
        private void Menu()
        {
            Console.WriteLine("1--Ajouter un contact");
            Console.WriteLine("2--Afficher la liste des contacts");
            Console.WriteLine("3--Rechercher des contacts");
            Console.WriteLine("4--Supprimer un contact");
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

        private void ActionSupprimerContact()
        {
            Contact contact = ActionRechercheContact();
            if(contact != null)
            {
                contact.Delete();
                Console.WriteLine($"Le contact : {contact} a été supprimé");
            }
        }

        private Contact ActionRechercheContact()
        {
            Console.Clear();
            Console.Write("Merci de saisir l'id du contact : ");
            int id = Convert.ToInt32(Console.ReadLine());
            Contact contact = Contact.GetContactById(id);
            if(contact == null)
            {
                Console.WriteLine("Aucun contact avec cet id");
            }
            return contact;
        }
    }
}
