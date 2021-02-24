using System;
using System.Collections.Generic;
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
            /*Console.Write("Entrez le nom : ");
            string nom = Console.ReadLine();
            Console.Write("Entrez le prenom : ");
            string prenom = Console.ReadLine();
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

            //Insertion des adresses (Table jointure avec la table personne)
            /*string choix;
            do
            {
                Console.Write("Ajouter une adresse (o/n) ?");
                choix = Console.ReadLine();
                if(choix == "o")
                {
                    Console.Write("Entrez la rue : ");
                    string rue = Console.ReadLine();
                    Console.Write("Entrez la ville : ");
                    string ville = Console.ReadLine();
                    Console.Write("Entrez le code postal : ");
                    string codePostal = Console.ReadLine();
                    request = "INSERT INTO adresse (rue, ville, codePostal, personneId) values " +
                        "(@rue, @ville, @codePostal, @personneId)";
                    command = new SqlCommand(request, connection);
                    command.Parameters.Add(new SqlParameter("@rue", rue));
                    command.Parameters.Add(new SqlParameter("@ville", ville));
                    command.Parameters.Add(new SqlParameter("@codePostal", codePostal));
                    command.Parameters.Add(new SqlParameter("@personneId", id));
                    if( command.ExecuteNonQuery()>0)
                    {
                        Console.WriteLine("Adresse ajoutée");
                    }
                    command.Dispose();
                }
            } while (choix != "n");*/


            //Requete de lecture
            /*string request = "SELECT id, nom, prenom from personne";
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
            command.Dispose();*/
            List<Personne> personnes = new List<Personne>();
            //Lecture des données à partir de deux tables (personne et adresse) avec une seule requete
            /*string request = "SELECT " +
                "p.id as personneId, p.nom, p.prenom, a.id as adresseId, a.rue, a.ville, a.codePostal" +
                " FROM personne p left join adresse a on p.id = a.personneId";
            SqlCommand command = new SqlCommand(request, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            
            
            Personne personne = null;
            while (reader.Read())
            {   
                if(personne == null || (personne.Id != reader.GetInt32(0)))
                {
                    personne = new Personne()
                    {
                        Id = reader.GetInt32(0),
                        Nom = reader.GetString(1),
                        Prenom = reader.GetString(2)
                    };
                    personnes.Add(personne);
                }
                //On teste si la ligne a une valeur dans la col adresseId
                //pour eviter de créer des adresses pour les lignes sans adresse
                //reader.GetValue(3) => renvoie la valeur de la col adresseId, si null dans la table, le type de cet objet est de DBNull
                if (reader.GetValue(3).GetType() != typeof(DBNull))
                {
                    Adresse adresse = new Adresse()
                    {
                        Id = reader.GetInt32(3),
                        Rue = reader.GetString(4),
                        Ville = reader.GetString(5),
                        CodePostal = reader.GetString(6)
                    };
                    personne.Adresses.Add(adresse);
                }
            }
            reader.Close();
            command.Dispose();*/

            //Lecture des données à partir de deux tables (personne et adresse) En lazy
            string request = "SELECT id, nom, prenom from personne";
            SqlCommand command = new SqlCommand(request, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                Personne p = new Personne()
                {
                    Id = reader.GetInt32(0),
                    Nom = reader.GetString(1),
                    Prenom = reader.GetString(2)
                };
                personnes.Add(p);
            }
            reader.Close();
            command.Dispose();

            foreach(Personne p in personnes)
            {
                //executer une requete pour chercher les adresses de cette personne
                request = "SELECT id, rue, ville, codePostal from adresse where personneId = @personneId";
                command = new SqlCommand(request, connection);
                command.Parameters.Add(new SqlParameter("@personneId", p.Id));
                reader = command.ExecuteReader();
                while(reader.Read())
                {
                    Adresse adresse = new Adresse()
                    {
                        Id = reader.GetInt32(0),
                        Rue = reader.GetString(1),
                        Ville = reader.GetString(2),
                        CodePostal = reader.GetString(3)
                    };
                    p.Adresses.Add(adresse);
                }
                reader.Close();
                command.Dispose();
            }

            connection.Close();

            personnes.ForEach(p =>
            {
                Console.WriteLine(p);
            });
        }
    }
}
