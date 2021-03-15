using System;
using System.Collections.Generic;
using System.Text;

namespace CorrectionCompteBancaireAspNet.Models
{
    public class Client
    {
        private int id;
        private string nom;
        private string prenom;
        private string telephone;

        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        public int Id { get => id; set => id = value; }

        public override string ToString()
        {
            return $"Nom : {Nom}, Prénom : {Prenom}, Téléphone {Telephone}";
        }
    }
}
