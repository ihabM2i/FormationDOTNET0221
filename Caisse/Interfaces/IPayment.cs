using System;
using System.Collections.Generic;
using System.Text;

namespace Caisse.Interfaces
{
    interface IPayment
    {
        bool Pay(decimal amount);
    }
}
