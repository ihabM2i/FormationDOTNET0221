using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiteCoursWPF.Models
{
    public class Personne : AbstractModelWithNotification 
    {
        private int id;
        private string nom;
        private string prenom;
        private static int compteur = 0;
        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set { nom = value; RaisePropertyChange("Nom"); } }
        public string Prenom { get => prenom; set { prenom = value; RaisePropertyChange("Prenom"); } }


        public bool Save()
        {
            //Insert dans une base de données
            Id = ++compteur;
            return Id > 0;
        }

        public bool Update()
        {
            //Mise à jour dans une base de données
            return true;
        }

        public override string ToString()
        {
            return $"Nom : {Nom}, Prénom : {Prenom}";
        }
    }
}
