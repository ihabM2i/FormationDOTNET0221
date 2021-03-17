using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Tools
{
    public class Connection
    {
        public static SqlConnection New
        {
            get
            {
                return new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ihab\source\repos\CoursAP2019\basededonnees.mdf;Integrated Security=True;Connect Timeout=30");
            }
        }
    }
}
