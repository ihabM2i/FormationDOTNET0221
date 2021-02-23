using System;
using System.Data.SqlClient;

namespace CoursAdoDotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            //string oldChaine = @"Data Source=(LocalDb)\coursM2i;Initial Catalog=api;Integrated Security=True";
            string chaineConnexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ihab\source\repos\CoursMCPDNETF\FichierBaseDeDonneesSqlServer.mdf;Integrated Security=True;Connect Timeout=30";
            //Objet de connexion
            SqlConnection connection = new SqlConnection(chaineConnexion);
            //Requête sql Création de Table
            /*string request = "CREATE TABLE personne(id int PRIMARY KEY IDENTITY(1,1), nom varchar(100) not null, prenom varchar(100) not null)";
            //Objet command
            SqlCommand command = new SqlCommand(request, connection);
            //ouvrir la connexion
            connection.Open();
            //Plusieurs manières d'executer la commande
            //Sans résult
            int nb = command.ExecuteNonQuery();

            //La libération des ressources
            command.Dispose();
            //Fermeture de connexion
            connection.Close();
            Console.WriteLine(nb);*/

            //Requête d'insertion
            //Requete à utiliser avec une execution de commande sans resultat
            //string request = "INSERT INTO personne(nom, prenom) values('titi', 'minet')";
            //Requete à utiliser avec une execution de commande avec un resultat unique
            /*string nom = "abadi";
            string prenom = "prenom";
            //string request = "INSERT INTO personne(nom, prenom) OUTPUT INSERTED.id values('titi', 'minet')";
            //Requete avec des paramètres
            string request = "INSERT INTO personne(nom, prenom) OUTPUT INSERTED.id values(@nom, @prenom)";
            SqlCommand command = new SqlCommand(request, connection);
            //Associations des variables avec les alias à l'aide des paramètres de la requête
            //On passe par des paramètres pour eviter les injections Sql(faille de sécurité)
            command.Parameters.Add(new SqlParameter("@nom", nom));
            command.Parameters.Add(new SqlParameter("@prenom", prenom));
            connection.Open();
            //Sans result
            //int nbRow = command.ExecuteNonQuery();
            //Avec un resultat unqiue comme l'id de la ligne
            int id = (int)command.ExecuteScalar();
            command.Dispose();*/

            //Requete de lecture
            string request = "SELECT id, nom, prenom from personne";
            SqlCommand command = new SqlCommand(request, connection);
            //Execute commande avec la méthode excuteReader qui renvoie un dataReader => objet pour lire les données du requete
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            //Pour lire la totalité des données
            //La méthode read essaye de lire une ligne si true, elle déplace le curseur à la ligne suivante
            while(reader.Read())
            {
                Console.WriteLine($"id : {reader.GetInt32(0)}, Nom : {reader.GetString(1)}, Prénom : {reader.GetString(2)}");
            }
            //ferme le reader
            reader.Close();
            command.Dispose();
            connection.Close();
        }
    }
}
