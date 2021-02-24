using Annuaire.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Annuaire.Classes
{
    public class Contact
    {
        private int id;
        private string nom;
        private string prenom;
        private string telephone;
        private List<Email> mails;
        private static SqlCommand command;
        private static SqlDataReader reader;
        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        public List<Email> Mails { get => mails; set => mails = value; }

        //public Contact(int id)
        //{
        //    //request pour chercher le contact
        //}

        public Contact()
        {
            Mails = new List<Email>();
        }
        public bool Save()
        {
            string request = "INSERT INTO contact (nom, prenom, telephone) OUTPUT INSERTED.ID values (@nom, @prenom,@telephone)";
            command = new SqlCommand(request, DataBase.Connection);
            command.Parameters.Add(new SqlParameter("@nom", Nom));
            command.Parameters.Add(new SqlParameter("@prenom", Prenom));
            command.Parameters.Add(new SqlParameter("@telephone", Telephone));
            DataBase.Connection.Open();
            Id = (int)command.ExecuteScalar();
            command.Dispose();
            DataBase.Connection.Close();
            return Id > 0;
        }

        public bool Delete()
        {
            //Instruction de suppression dans la base de données
            string request = "DELETE FROM contact where id=@id";
            command = new SqlCommand(request, DataBase.Connection);
            command.Parameters.Add(new SqlParameter("@id", Id));
            DataBase.Connection.Open();
            int nbRow = command.ExecuteNonQuery();
            command.Dispose();
            DataBase.Connection.Close();
            return nbRow == 1;
        }

        public bool Update()
        {
            //Instruction Mise à jour dans la base de données après modification
            string request = "update contact set nom = @nom, prenom=@prenom, telephone=@telephone where id=@id";
            command = new SqlCommand(request, DataBase.Connection);
            command.Parameters.Add(new SqlParameter("@nom", Nom));
            command.Parameters.Add(new SqlParameter("@prenom", Prenom));
            command.Parameters.Add(new SqlParameter("@telephone", Telephone));
            command.Parameters.Add(new SqlParameter("@id", Id));
            DataBase.Connection.Open();
            int nbRow = command.ExecuteNonQuery();
            command.Dispose();
            DataBase.Connection.Close();
            return nbRow == 1;
        }

        public static Contact GetContactById(int id)
        {
            Contact contact = null;
            //Une méthode pour récupérer un contact avec son id
            string request = "SELECT id, nom, prenom, telephone from contact where id = @id";
            command = new SqlCommand(request, DataBase.Connection);
            command.Parameters.Add(new SqlParameter("@id", id));
            DataBase.Connection.Open();
            reader = command.ExecuteReader();
            if(reader.Read())
            {
                contact = new Contact
                {
                    Id = reader.GetInt32(0),
                    Nom = reader.GetString(1),
                    Prenom = reader.GetString(2),
                    Telephone = reader.GetString(3)
                };
            }
            reader.Close();
            command.Dispose();
            DataBase.Connection.Close();
            return contact;
        }

        public static List<Contact> GetContacts()
        {
            //Pour une récupération totale avec les mails, 
            //on modifie la requete en ajoutant la jointure avec la table mail
            List<Contact> contacts = new List<Contact>();
            string request = "SELECT " +
                "c.id as contactId, c.nom, c.prenom, c.telephone, m.id as mailId, m.mail" +
                " from contact c left join Mail m on c.id = m.contactId";
            command = new SqlCommand(request, DataBase.Connection);
            DataBase.Connection.Open();
            reader = command.ExecuteReader();
            Contact contact = null;
            while (reader.Read())
            {
                if(contact == null || contact.Id != reader.GetInt32(0))
                {
                    contact = new Contact
                    {
                        Id = reader.GetInt32(0),
                        Nom = reader.GetString(1),
                        Prenom = reader.GetString(2),
                        Telephone = reader.GetString(3)
                    };
                    contacts.Add(contact);
                }
                if(reader.GetValue(4).GetType() != typeof(DBNull))
                {
                    Email e = new Email
                    {
                        Id = reader.GetInt32(4),
                        Mail = reader.GetString(5)
                    };
                    contact.Mails.Add(e);
                }
            }
            reader.Close();
            command.Dispose();
            DataBase.Connection.Close();
            return contacts;
        }

        public static List<Contact> SearchContacts(string search)
        {
            
            List<Contact> contacts = new List<Contact>();
            string request = "SELECT id, nom, prenom, telephone from contact where " +
                "nom like @search OR prenom like @search OR telephone like @search";
            command = new SqlCommand(request, DataBase.Connection);
            command.Parameters.Add(new SqlParameter("@search", $"{search}%"));
            DataBase.Connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Contact contact = new Contact
                {
                    Id = reader.GetInt32(0),
                    Nom = reader.GetString(1),
                    Prenom = reader.GetString(2),
                    Telephone = reader.GetString(3)
                };
                contacts.Add(contact);
            }
            reader.Close();
            command.Dispose();

            //Requete 2
            request = "deuxième requete";
            command = new SqlCommand(request, DataBase.Connection);
            //executer la commande


            DataBase.Connection.Close();
            return contacts;
        }

        public override string ToString()
        {
            string retour = $"=====Contact : Id: { Id}, Nom: { Nom}, Prénom: { Prenom}, Téléphone: { Telephone}\n";
            retour += "--Liste mails : \n";
            Mails.ForEach(e =>
            {
                retour += $"{e.ToString()}\n";
            });
            return retour;
        }
    }
}
