using System;
using System.Collections.Generic;
using System.Text;

namespace CoursAdoDotNet
{
    public class Adresse
    {
        private int id;
        private string rue;
        private string ville;
        private string codePostal;

        public int Id { get => id; set => id = value; }
        public string Rue { get => rue; set => rue = value; }
        public string Ville { get => ville; set => ville = value; }
        public string CodePostal { get => codePostal; set => codePostal = value; }

        public override string ToString()
        {
            return $"Id : {Id}, Rue : {Rue}, Ville : {Ville}, CodePostal : {CodePostal}";
        }
    }
}
