
using coursAspNetMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coursAspNetMVC.ViewModels
{
    public class PersonneAdresseViewModel
    {
        public List<Adresse> Adresses { get; set; }
        public List<Personne> Personnes { get; set; }

    }
}
