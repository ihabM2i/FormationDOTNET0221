﻿using Annuaire.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Annuaire.Classes
{
    public class Email
    {
        private int id;
        private string mail;
        private static string request;
        private static SqlCommand command;
        private static SqlDataReader reader;

        public int Id { get => id; set => id = value; }
        public string Mail { get => mail; set => mail = value; }

        public override string ToString()
        {
            return $"id : {Id}, Mail : {Mail}";
        }

        public bool Save(int contactId)
        {
            //Requete save email
            request = "INSERT INTO Mail (mail, contactId) OUTPUT INSERTED.ID values (@mail, @contactId)";
            command = new SqlCommand(request, DataBase.Connection);
            command.Parameters.Add(new SqlParameter("@mail", Mail));
            command.Parameters.Add(new SqlParameter("@contactId", contactId));
            DataBase.Connection.Open();
            Id = (int)command.ExecuteScalar();
            command.Dispose();
            DataBase.Connection.Close();
            return Id > 0;
        }

        //pour une utilisation lazy
        public static List<Email> GetMails(int contactId)
        {
            List<Email> liste = new List<Email>();
            request = "SELECT id,mail FROM mail where contactId = @contactId";
            command = new SqlCommand(request, DataBase.Connection);
            command.Parameters.Add(new SqlParameter("@contactId", contactId));
            DataBase.Connection.Open();
            reader = command.ExecuteReader();
            while(reader.Read())
            {
                Email e = new Email
                {
                    Id = reader.GetInt32(0),
                    Mail = reader.GetString(1)
                };
                liste.Add(e);
            }
            reader.Close();
            command.Dispose();
            DataBase.Connection.Close();
            return liste;
        }

        public bool Delete()
        {
            request = "DELETE FROM mail where id = @id";
            command = new SqlCommand(request, DataBase.Connection);
            command.Parameters.Add(new SqlParameter("@id", Id));
            DataBase.Connection.Open();
            int nbRow = command.ExecuteNonQuery();
            command.Dispose();
            DataBase.Connection.Close();
            return nbRow == 1;
        }

        //Avec une transaction
        public bool Delete(SqlTransaction transaction)
        {
            request = "DELETE FROM mail where id = @id";
            command = new SqlCommand(request, DataBase.Connection, transaction);
            command.Parameters.Add(new SqlParameter("@id", Id));          
            int nbRow = command.ExecuteNonQuery();
            return nbRow == 1;
        }

        //Deuxième manière de supprimer les emails d'un contact
        public static bool deleteMailsByContact(int contactId)
        {
            request = "DELETE FROM mail where contactId = @id";
            command = new SqlCommand(request, DataBase.Connection);
            command.Parameters.Add(new SqlParameter("@id", contactId));
            DataBase.Connection.Open();
            int nbRow = command.ExecuteNonQuery();
            command.Dispose();
            DataBase.Connection.Close();
            return nbRow >= 1;
        }
    }
}
