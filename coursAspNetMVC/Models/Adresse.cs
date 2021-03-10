using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coursAspNetMVC.Models
{
    public class Adresse
    {
        private string rue;
        private string ville;

        public string Rue { get => rue; set => rue = value; }
        public string Ville { get => ville; set => ville = value; }

        public static List<Adresse> GetAdresses()
        {
            return new List<Adresse>()
            {
                new Adresse() {Rue = "rue 1", Ville ="Lille"},
                new Adresse() {Rue = "rue 2", Ville ="Tourcoing"},
            };
        }
    }
}
