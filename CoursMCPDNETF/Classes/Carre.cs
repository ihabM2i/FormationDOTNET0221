using CoursMCPDNETF.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoursMCPDNETF.Classes
{
    class Carre : Figure, IDeformable
    {
        private double cote;

        public double Cote { get => cote; set => cote = value; }

        public Carre(double posX, double posY, double cote): base(posX, posY)
        {
            Cote = cote;
        }

        public override void Afficher()
        {
            Console.WriteLine("Je suis un carré de longueur de côté {0}", Cote);
        }

        public Figure Deformation(double coeffH, double coeffV)
        {
            if(coeffV == coeffH)
            {
                return new Carre(PosX, PosY, Cote * coeffV);
            }
            else
            {
                return new Rectangle(PosX, PosY, coeffH * Cote, coeffV * Cote);
            }
        }
    }
}
