using System;
using System.Collections.Generic;
using System.Text;

namespace CoursMCPDNETF.Classes
{
    abstract class Figure
    {
        private double posX;
        private double posY;

        public double PosX { get => posX; set => posX = value; }
        public double PosY { get => posY; set => posY = value; }

        protected Figure(double posX, double posY)
        {
            PosX = posX;
            PosY = posY;
        }

        public abstract void Afficher();
    }
}
