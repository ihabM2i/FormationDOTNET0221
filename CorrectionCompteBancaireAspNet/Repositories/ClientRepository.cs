using CorrectionCompteBancaireAspNet.Models;
using CorrectionCompteBancaireAspNet.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CorrectionCompteBancaireAspNet.Repositories
{
    public class ClientRepository : BaseRepository, IRepository<Client>
    {
        public ClientRepository(SqlConnection connection) : base(connection)
        {
        }

        public ClientRepository(SqlConnection connection, SqlTransaction transaction) : base(connection, transaction)
        {
        }

        public Client Create(Client element)
        {
            request = "INSERT INTO client (nom, prenom, telephone) " +
                "OUTPUT INSERTED.id values (@nom, @prenom, @telephone)";
            command = new SqlCommand(request, _connection);
            if(_transaction != null)
            {
                command.Transaction = _transaction;
            }
            command.Parameters.Add(new SqlParameter("@nom", element.Nom));
            command.Parameters.Add(new SqlParameter("@prenom", element.Prenom));
            command.Parameters.Add(new SqlParameter("@telephone", element.Telephone));
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            element.Id = (int)command.ExecuteScalar();
            command.Dispose();
            if (_connection.State == ConnectionState.Open && _transaction == null)
                _connection.Close();
            return element;
        }

        public List<Client> FindAll()
        {
            throw new NotImplementedException();
        }

        public Client FindElementById(int id)
        {
            Client client = null;
            request = "SELECT id, nom, prenom, telephone from client where id=@id";
            command = new SqlCommand(request, _connection);
            if (_transaction != null)
            {
                command.Transaction = _transaction;
            }
            command.Parameters.Add(new SqlParameter("@id", id));           
            
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                client = new Client()
                {
                    Id = reader.GetInt32(0),
                    Nom = reader.GetString(1),
                    Prenom = reader.GetString(2),
                    Telephone = reader.GetString(3)
                };
            }
            reader.Close();
            command.Dispose();
            if (_connection.State == ConnectionState.Open && _transaction == null)
                _connection.Close();
            return client;
        }

        public Client Update(Client element)
        {
            throw new NotImplementedException();
        }
    }
}
