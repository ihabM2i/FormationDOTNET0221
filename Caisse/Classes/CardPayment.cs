using Caisse.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Caisse.Classes
{
    class CardPayment : Payment, IPayment
    {
        public bool Pay(decimal amount)
        {
            Amount = amount;
            return true;
        }
    }
}
