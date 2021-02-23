using CoursMCPDNETF.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoursMCPDNETF.Classes
{
    class Rectangle : Figure, IDeformable
    {
        private double largeur;
        private double hauteur;

        public double Largeur { get => largeur; set => largeur = value; }
        public double Hauteur { get => hauteur; set => hauteur = value; }

        public Rectangle(double posX, double posY, double largeur, double hauteur) : base(posX, posY)
        {
            Largeur = largeur;
            Hauteur = hauteur;
        }


        public override void Afficher()
        {
            Console.WriteLine("Je suis un rectangle avec largeur {0} et hauteur {1}", Largeur, Hauteur);
        }

        public Figure Deformation(double coeffH, double coeffV)
        {
            if(coeffH * Largeur == coeffV * Hauteur)
            {
                return new Carre(PosX, PosY, coeffV * Hauteur);
            }
            else
            {
                return new Rectangle(PosX, PosY, coeffH * Largeur, coeffV * Hauteur);
            }
        }
    }
}
