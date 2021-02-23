using CoursMCPDNETF.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoursMCPDNETF.Classes
{
    class GenerateurDeMotVersion2 : IGenerateur
    {
        public string Generer()
        {
            return "Toujours le même mot";
        }
    }
}
