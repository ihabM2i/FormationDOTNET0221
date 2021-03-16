using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace coursAspNetMVC.Models
{
    public class Avatar
    {
        private int id;
        private string chemin;
        private static SqlConnection connection;

        public int Id { get => id; set => id = value; }
        public string Chemin { get => chemin; set => chemin = value; }



        public bool Save()
        {
            connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ihab\source\repos\CoursAP2019\basededonnees.mdf;Integrated Security=True;Connect Timeout=30");
            string request = "INSERT INTO avatar (chemin) OUTPUT INSERTED.ID values(@chemin)";
            SqlCommand command = new SqlCommand(request, connection);
            command.Parameters.Add(new SqlParameter("@chemin", Chemin));
            connection.Open();
            Id = (int)command.ExecuteScalar();
            command.Dispose();
            connection.Close();
            return Id > 0;
        }

        public static List<Avatar> GetAll()
        {
            List<Avatar> liste = new List<Avatar>();
            connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ihab\source\repos\CoursAP2019\basededonnees.mdf;Integrated Security=True;Connect Timeout=30");
            string request = "select * from avatar";
            SqlCommand command = new SqlCommand(request, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                Avatar a = new Avatar()
                {
                    Id = reader.GetInt32(0),
                    Chemin = reader.GetString(1)
                };
                liste.Add(a);
            }
            reader.Close();
            command.Dispose();
            connection.Close();
            return liste;
        }
    }
}
