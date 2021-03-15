using CorrectionCompteBancaireAspNet.Models;
using CorrectionCompteBancaireAspNet.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CorrectionCompteBancaireAspNet.Repositories
{
    public class OperationRepository : BaseRepository, IRepository<Operation>
    {
        public OperationRepository(SqlConnection connection) : base(connection)
        {
        }

        public OperationRepository(SqlConnection connection, SqlTransaction transaction) : base(connection, transaction)
        {
        }

        public Operation Create(Operation element)
        {
            throw new NotImplementedException();
        }

        public List<Operation> FindAll()
        {
            throw new NotImplementedException();
        }

        public Operation FindElementById(int id)
        {
            throw new NotImplementedException();
        }

        public Operation Update(Operation element)
        {
            throw new NotImplementedException();
        }

        public Operation Create(Operation element, int compteId)
        {
            request = "INSERT INTO operation (date_operation, montant, compte_id) " +
                "OUTPUT INSERTED.ID values (@date_operation, @montant, @compte_id)";
            command = new SqlCommand(request, _connection);
            if (_transaction != null)
            {
                command.Transaction = _transaction;
            }
            command.Parameters.Add(new SqlParameter("@date_operation", element.DateOperation));
            command.Parameters.Add(new SqlParameter("@montant", element.Montant));
            command.Parameters.Add(new SqlParameter("@compte_id", compteId));
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            element.Numero = (int)command.ExecuteScalar();
            command.Dispose();
            if (_connection.State == ConnectionState.Open && _transaction == null)
                _connection.Close();
            return element;
        }

        public List<Operation> FindAll(int compteId)
        {
            List<Operation> liste = new List<Operation>();
            request = "SELECT id, montant, date_operation from operation where compte_id = @compte_id";
            command = new SqlCommand(request, _connection);
            if (_transaction != null)
            {
                command.Transaction = _transaction;
            }
            command.Parameters.Add(new SqlParameter("@compte_id", compteId));
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            reader = command.ExecuteReader();
            while(reader.Read())
            {
                Operation o = new Operation(reader.GetDecimal(1))
                {
                    Numero = reader.GetInt32(0),
                    DateOperation = reader.GetDateTime(2)
                };
                liste.Add(o);
            }
            reader.Close();
            command.Dispose();
            if (_connection.State == ConnectionState.Open && _transaction == null)
                _connection.Close();
            return liste;
        }
    }
}
