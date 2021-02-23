using System;
using System.Collections.Generic;
using System.Text;

namespace Caisse.Classes
{
    abstract class Payment
    {
        //Reference paiement
        private int id;
        private DateTime datePayment;
        private decimal amount;

        public int Id { get => id; set => id = value; }
        public DateTime DatePayment { get => datePayment; set => datePayment = value; }
        public decimal Amount { get => amount; set => amount = value; }

        protected Payment()
        {
            DatePayment = DateTime.Now;
        }
    }
}
