using CoursMCPDNETF.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoursMCPDNETF.Classes
{
    class GenerateurDeMot : IGenerateur
    {
        private string[] mots = new string[] { "amazon", "google", "facebook", "microsoft", "macintosh", "instagram" };

        public string Generer()
        {
            Random r = new Random();
            int randomIndex = r.Next(mots.Length);
            return mots[randomIndex];
        }
    }
}
