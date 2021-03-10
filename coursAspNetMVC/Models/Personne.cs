using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coursAspNetMVC.Models
{
    public class Personne
    {
        private string nom;
        private string prenom;

        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }


        public static List<Personne> GetPersonnes()
        {
            return new List<Personne>()
            {
                new Personne() {Nom = "abadi", Prenom = "ihab"},
                new Personne() {Nom = "tata", Prenom = "toto"},
            };
        }
    }
}
