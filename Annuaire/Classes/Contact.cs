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
        private static SqlCommand command;
        private static SqlDataReader reader;
        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Telephone { get => telephone; set => telephone = value; }

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
            List<Contact> contacts = new List<Contact>();
            string request = "SELECT id, nom, prenom, telephone from contact";
            command = new SqlCommand(request, DataBase.Connection);
            DataBase.Connection.Open();
            reader = command.ExecuteReader();
            while(reader.Read())
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
            return $"Id : {Id}, Nom : {Nom}, Prénom : {Prenom}, Téléphone : {Telephone}";
        }
    }
}
