using System;
using System.Collections.Generic;
using System.Text;

namespace CoursMCPDNETF.Classes
{
    class Triangle : Figure
    {
        private double laBase;
        private double hauteur;

        public double LaBase { get => laBase; set => laBase = value; }
        public double Hauteur { get => hauteur; set => hauteur = value; }

        public Triangle(double posX, double posY, double laBase, double hauteur) : base(posX, posY)
        {
            LaBase = laBase;
            Hauteur = hauteur;
        }

        public override void Afficher()
        {
            Console.WriteLine("Je suis un triangle isocèle avec une base de {0} et une hauteur de {1}", LaBase, Hauteur);
        }
    }
}
