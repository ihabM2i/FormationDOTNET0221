using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Classes
{
    class Client
    {
        private int id;
        private string nom;
        private string prenom;
        private string telephone;

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Telephone { get => telephone; set => telephone = value; }

        private static int compteur = 0;

        public Client()
        {
            //id = ++compteur;
        }

        public Client(int id, string nom, string prenom, string telephone)
        {
            this.id = id;
            Nom = nom;
            Prenom = prenom;
            Telephone = telephone;
        }

        public override string ToString()
        {
            return $"Nom : {Nom}, Prénom : {Prenom}, Téléphone : {Telephone}";
        }

        
    }
}
