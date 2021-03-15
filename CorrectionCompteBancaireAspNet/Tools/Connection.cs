using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CorrectionCompteBancaireAspNet.Tools
{
    public class Connection
    {
       public static SqlConnection New { get => new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ihab\source\repos\CoursAP2019\basededonnees.mdf;Integrated Security=True;Connect Timeout=30"); }
    }
}
