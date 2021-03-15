using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CorrectionCompteBancaireAspNet.Repositories
{
    public abstract class BaseRepository
    {
        protected SqlCommand command;
        protected SqlDataReader reader;
        protected SqlTransaction _transaction;
        protected SqlConnection _connection;
        protected string request;
        public BaseRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public BaseRepository(SqlConnection connection, SqlTransaction transaction) : this(connection)
        {
            _transaction = transaction;
        }
    }
}
