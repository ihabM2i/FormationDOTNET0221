using Annuaire.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                    case "5":
                        ActionMiseAJourContact();
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
            Console.WriteLine("5--Modifier un contact");
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
                ActionAjouterEmail(contact);
            }
        }

        private void ActionAjouterEmail(Contact contact)
        {
            string choix;
            do
            {
                Console.Write("Ajouter un mail (o/n) ? ");
                choix = Console.ReadLine();
                if(choix == "o")
                {
                    Console.Write("Merci de saisir le mail : ");
                    string mail = Console.ReadLine();
                    Email e = new Email { Mail = mail };
                    if(e.Save(contact.Id))
                    {
                        Console.WriteLine("Mail ajouté");
                    }
                }
            } while (choix != "n");
            //ajout des mails
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
                    //Pour une récupération des mails en lazy
                    c.Mails = Email.GetMails(c.Id);
                    Console.WriteLine(c);
                }
            }
            else
            {
                Console.WriteLine("Aucun contact trouvé");
            }
        }

        //private void ActionSupprimerContact()
        //{
        //    Contact contact = ActionRechercheContact();
        //    //Essayer de supprimer les emails et le contact en mode lazy 
        //    //en utilisant le principe de transaction

        //    if(contact != null)
        //    {
        //        //lazy delete
        //        //foreach(Email e in contact.Mails)
        //        //{
        //        //    if(!e.Delete())
        //        //    {
        //        //        allMailsDeleted = false;
        //        //        break;
        //        //    }
        //        //}
        //        //Eager delete
        //        bool allMailsDeleted = Email.deleteMailsByContact(contact.Id);
        //        if (allMailsDeleted)
        //        {
        //            contact.Delete();
        //            Console.WriteLine($"Le contact : {contact} a été supprimé");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Erreur suppression mails");
        //        }
                
        //    }
        //}


            //Avec transaction
        private void ActionSupprimerContact()
        {
            Contact contact = ActionRechercheContact();
            //Essayer de supprimer les emails et le contact en mode lazy 
            //en utilisant le principe de transaction

            if (contact != null)
            {
                SqlTransaction transaction = null;
                DataBase.Connection.Open();
                transaction = DataBase.Connection.BeginTransaction();
                try
                {
                    foreach (Email e in contact.Mails)
                    {
                        e.Delete(transaction);
                    }
                    contact.Delete(transaction);
                    transaction.Commit();
                    Console.WriteLine($"Le contact : {contact} a été supprimé");
                }
                catch(Exception e)
                {
                    transaction.Rollback();
                    Console.WriteLine("Erreur suppression : "+e.Message);
                }
                DataBase.Connection.Close();
            }
        }

        private void ActionMiseAJourContact()
        {
            Contact contact = ActionRechercheContact();
            if (contact != null)
            {
                Console.Write("Merci de saisir le nom : ");
                string nom = Console.ReadLine();
                contact.Nom = nom != "" ? nom : contact.Nom;
                Console.Write("Merci de saisir le prénom : ");
                string prenom = Console.ReadLine();
                contact.Prenom = prenom != "" ? prenom : contact.Prenom;
                Console.Write("Merci de saisir le téléphone : ");
                string telephone = Console.ReadLine();
                contact.Telephone= telephone != "" ? telephone : contact.Telephone;
                if(contact.Update())
                {
                    Console.WriteLine("Mise à jour effectuée");
                }
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
                contact.Mails = Email.GetMails(contact.Id);
                Console.WriteLine("Aucun contact avec cet id");
            }
            return contact;
        }
    }
}
