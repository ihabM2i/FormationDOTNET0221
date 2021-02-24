using System;
using System.Collections.Generic;
using System.Text;

namespace Annuaire.Classes
{
    public class Email
    {
        private int id;
        private string mail;

        public int Id { get => id; set => id = value; }
        public string Mail { get => mail; set => mail = value; }

        public override string ToString()
        {
            return $"id : {Id}, Mail : {Mail}";
        }

        public bool Save(int contactId)
        {
            //Requete save email
            return false;
        }

        //pour une utilisation lazy
        public static List<Email> GetMails(int contactId)
        {
            return null;
        }
    }
}
