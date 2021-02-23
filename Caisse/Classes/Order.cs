using Caisse.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Caisse.Classes
{
    //=> classe vente
    class Order
    {
        //Reference unique de la vente
        private int id;
        //Date et heure de la vente
        private DateTime dateOrder;
        private List<Product> products;
        private OrderStatus status;
        private IPayment payment;

        private static int count = 0;

        public int Id { get => id; }
        public DateTime DateOrder { get => dateOrder; set => dateOrder = value; }
        public List<Product> Products { get => products; set => products = value; }
        public OrderStatus Status { get => status; set => status = value; }
        public IPayment Payment { get => payment; set => payment = value; }

        public decimal Total
        {
            get
            {
                decimal total = 0;
                //Expression lambda pour parccourir une liste
                Products.ForEach(p =>
                {
                    total += p.Price;
                });
                return total;
            }
        }

        public Order()
        {
            Products = new List<Product>();
            //Affecter la valeur 0 à l'enum statut => Pour récupérer une valeur d'enum, on utilise le nom de l'enum et la valeur souhaitée
            Status = OrderStatus.Waiting;
            DateOrder = DateTime.Now;
            id = ++count;
        }

        public bool AddProduct(Product product)
        {
            Products.Add(product);
            return true;
        }

        public bool Confirm(IPayment payment)
        {
            Payment = payment;
            if(Payment.Pay(Total))
            {
                Status = OrderStatus.Confirmed;
                Products.ForEach(p =>
                {
                    p.Stock -= 1;
                });
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            string response = $"=========Numéro de vente : {Id}==========\n";
            response += "-----Lite produits -----\n";
            Products.ForEach(p =>
            {
                response += p.ToString() + "\n";
            });
            response += $"Total : {Total} euros";
            return response;
        }
    }

    //Création d'une enum pour le statut des ventes
    enum OrderStatus
    {
        Waiting,
        Confirmed,
        Canceled,
    }
}
