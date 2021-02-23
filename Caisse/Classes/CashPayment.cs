using Caisse.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Caisse.Classes
{
    class CashPayment : Payment, IPayment
    {
        //Monnaie à rendre
        private decimal change;

        //Montant donné
        private decimal givenAmount;
        public decimal Change { get => change; }
        public decimal GivenAmount { get => givenAmount; set => givenAmount = value; }

        public bool Pay(decimal amount)
        {
            if(amount <= GivenAmount)
            {
                change = GivenAmount - amount;
                return true;
            }
            return false;
        }
    }
}
