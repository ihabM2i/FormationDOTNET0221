using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Annuaire.Tools
{
    public class DataBase
    {
        private static string chaine = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ihab\source\repos\CoursMCPDNETF\FichierBaseDeDonneesSqlServer.mdf;Integrated Security=True;Connect Timeout=30";
        public static SqlConnection Connection = new SqlConnection(chaine);
    }
}
