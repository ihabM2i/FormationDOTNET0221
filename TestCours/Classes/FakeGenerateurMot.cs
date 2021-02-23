using CoursMCPDNETF.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestCours.Classes
{
    public class FakeGenerateurMot : IGenerateur
    {
        public string Generer()
        {
            return "Bonjour";
        }
    }
}
