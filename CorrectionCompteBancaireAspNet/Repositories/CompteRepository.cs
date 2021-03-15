using CorrectionCompteBancaireAspNet.Models;
using CorrectionCompteBancaireAspNet.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CorrectionCompteBancaireAspNet.Repositories
{
    public class CompteRepository : BaseRepository, IRepository<Compte>
    {
        public CompteRepository(SqlConnection connection) : base(connection)
        {
        }

        public CompteRepository(SqlConnection connection, SqlTransaction transaction) : base(connection, transaction)
        {
        }

        public Compte Create(Compte element)
        {
            request = "INSERT INTO compte (solde, client_id, max_decouvert) " +
                "OUTPUT INSERTED.id values (@solde, @client_id, @max_decouvert)";
            command = new SqlCommand(request, _connection);
            if (_transaction != null)
            {
                command.Transaction = _transaction;
            }
            command.Parameters.Add(new SqlParameter("@solde", element.Solde));
            command.Parameters.Add(new SqlParameter("@client_id", element.Client.Id));
            command.Parameters.Add(new SqlParameter("@max_decouvert", element.MaxDecouvert));
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            element.Numero = (int)command.ExecuteScalar();
            command.Dispose();
            if (_connection.State == ConnectionState.Open && _transaction == null)
                _connection.Close();
            return element;            
        }

        public List<Compte> FindAll()
        {
            throw new NotImplementedException();
        }

        public Compte FindElementById(int id)
        {
            Compte compte = null;
            request = "SELECT cp.solde, cp.max_decouvert, c.id, c.nom, c.prenom, c.telephone " +
                "from compte as cp inner join client as c " +
                "on c.id = cp.client_id where cp.id=@id";
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
                compte = new Compte()
                {
                    Numero = id,
                    Solde = reader.GetDecimal(0),
                    MaxDecouvert = reader.GetDecimal(1),
                    Client = new Client()
                    {
                        Id = reader.GetInt32(2),
                        Nom = reader.GetString(3),
                        Prenom = reader.GetString(4),
                        Telephone = reader.GetString(5)
                    }
                };
            }
            reader.Close();
            command.Dispose();
            if (_connection.State == ConnectionState.Open && _transaction == null)
                _connection.Close();
            return compte;
        }

        public Compte Update(Compte element)
        {
            request = "UPDATE compte set solde=@solde where id=@id";
            command = new SqlCommand(request, _connection);
            if (_transaction != null)
            {
                command.Transaction = _transaction;
            }
            command.Parameters.Add(new SqlParameter("@solde", element.Solde));
            command.Parameters.Add(new SqlParameter("@id", element.Numero));
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            command.ExecuteNonQuery();
            command.Dispose();
            if (_connection.State == ConnectionState.Open && _transaction == null)
                _connection.Close();
            return element;
        }
    }
}
