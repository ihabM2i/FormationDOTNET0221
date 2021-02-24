using System;
using System.Collections.Generic;
using System.Text;

namespace CoursAdoDotNet
{
    public class Personne
    {
        private int id;
        private string nom;
        private string prenom;
        private List<Adresse> adresses;
        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public List<Adresse> Adresses { get => adresses; set => adresses = value; }

        public Personne()
        {
            Adresses = new List<Adresse>();
        }

        public override string ToString()
        {
            string retour = "=========================\n";
            retour += $"Id {Id}, Nom : {Nom}, Prénom : {Prenom} \n";
            retour += "=========Adresses========\n";
            Adresses.ForEach(a =>
            {
                retour += $"{a.ToString()}\n";
            });
            return retour;
        }
    }
}
