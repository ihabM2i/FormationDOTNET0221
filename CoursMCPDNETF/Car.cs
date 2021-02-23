using CoursMCPDNETF.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoursMCPDNETF
{
    class Car : IDisplay
    {
        private string model;
        private decimal price;

        public event Action<decimal> Promotion;

        public string Model { get => model; set => model = value; }
        public decimal Price { get => price; set => price = value; }

        public void Display()
        {
            Console.WriteLine("Model voiture est {0}", Model);
        }

        public void DisplayPrice()
        {
            Console.WriteLine("Prix voiture est de {0}", Price);
        }

        //public void Promotion()
        //{
        //    Notification notification = new Notification();
        //    notification.NotifyByEmail();
        //    notification.NotifyBySms();
        //}

        public void Reduction(decimal reduction)
        {
            Price -= reduction;
            //Si une méthode ecoute notre event promotion
            if(Promotion != null)
                Promotion(Price);
        }
    }
}
