using Annuaire.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Caisse.Classes
{
    abstract class Payment
    {
        //Reference paiement
        private int id;
        private DateTime datePayment;
        private decimal amount;

        private static string request;
        private static SqlCommand command;
        private static SqlDataReader reader;
        public int Id { get => id; set => id = value; }
        public DateTime DatePayment { get => datePayment; set => datePayment = value; }
        public decimal Amount { get => amount; set => amount = value; }

        protected Payment()
        {
            DatePayment = DateTime.Now;
        }

        public virtual bool Save(int orderId)
        {
            //Sauvegarde dans la table paiement
            request = "INSERT INTO payment (amount, order_id, payment_type, payment_date) " +
                "OUTPUT INSERTED.id values " +
                "(@amount, @order_id, @payment_type, @payment_date)";
            command = new SqlCommand(request, DataBase.Connection);
            command.Parameters.Add(new SqlParameter("@amount", Amount));
            command.Parameters.Add(new SqlParameter("@order_id", orderId));
            command.Parameters.Add(new SqlParameter("@payment_type", this.GetType().ToString()));
            command.Parameters.Add(new SqlParameter("@payment_date", DatePayment));
            DataBase.Connection.Open();
            id = (int)command.ExecuteScalar();
            DataBase.Connection.Close();
            return id > 0;
        }

        public virtual bool Save(int orderId, SqlTransaction transaction)
        {
            //Sauvegarde dans la table paiement
            request = "INSERT INTO payment (amount, order_id, payment_type, payment_date) " +
                "OUTPUT INSERTED.id values " +
                "(@amount, @order_id, @payment_type, @payment_date)";
            command = new SqlCommand(request, DataBase.Connection, transaction);
            command.Parameters.Add(new SqlParameter("@amount", Amount));
            command.Parameters.Add(new SqlParameter("@order_id", orderId));
            command.Parameters.Add(new SqlParameter("@payment_type", this.GetType()));
            command.Parameters.Add(new SqlParameter("@payment_date", DatePayment));     
            id = (int)command.ExecuteScalar();
            return id > 0;
        }
    }
}
