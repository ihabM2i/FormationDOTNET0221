using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Caisse.Interfaces
{
    interface IPayment
    {
        bool Pay(decimal amount);
        bool Save(int orderId);
        bool Save(int orderId, SqlTransaction transaction);
    }
}
