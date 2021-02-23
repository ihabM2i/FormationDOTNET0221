using System;
using System.Collections.Generic;
using System.Text;

namespace CoursMCPDNETF.Classes
{
    class Notification
    {

        public void NotifyBySms(decimal newPrice)
        {
            Console.WriteLine("notification par sms pour la promotion de la voiture, nouveau prix est de {0} euros",newPrice);
        }

        public void NotifyByEmail(decimal newPrice)
        {
            Console.WriteLine("notification par email pour la promotion de la voiture, nouveaux prix est de {0} euros", newPrice);
        }

        public void NotifyPilePleine(int taille)
        {
            Console.WriteLine("La pile est pleine avec {0} elements", taille);
        }
    }
}
