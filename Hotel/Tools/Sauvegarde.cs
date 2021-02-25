using Annuaire.Tools;
using Hotel.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Hotel.Tools
{
    class Sauvegarde
    {
        private int hotelId;
        private static string request;
        private static SqlCommand command;
        private static SqlDataReader reader;
        private static SqlTransaction transaction;
        public Sauvegarde(string nom)
        {
            bool hotelExist = false;
            request = "SELECT id from hotel where nom = @nom";
            command = new SqlCommand(request, DataBase.Connection);
            command.Parameters.Add(new SqlParameter("@nom", nom));
            DataBase.Connection.Open();
            reader = command.ExecuteReader();
            if(reader.Read())
            {
                hotelExist = true;
                hotelId = reader.GetInt32(0);
            }
            reader.Close();
            command.Dispose();
            if(!hotelExist)
            {
                request = "INSERT INTO hotel (nom) OUTPUT INSERTED.ID values (@nom)";
                command = new SqlCommand(request, DataBase.Connection);
                command.Parameters.Add(new SqlParameter("@nom", nom));
                hotelId = (int)command.ExecuteScalar();
                command.Dispose();
            }
            DataBase.Connection.Close();
        }
        public List<Client> LireClients()
        {            
            List<Client> listes = new List<Client>();
            request = "SELECT id, nom, prenom, telephone from client where hotel_id = @hotelId";
            command = new SqlCommand(request, DataBase.Connection);
            command.Parameters.Add(new SqlParameter("@hotelId", hotelId));
            DataBase.Connection.Open();
            reader = command.ExecuteReader();
            while(reader.Read())
            {
                Client c = new Client(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                listes.Add(c);
            }
            reader.Close();
            LiberCommandeEtFermerConnexion();
            return listes;
        }
        public void EcrireClients(Client client)
        {
            request = "INSERT INTO client (nom, prenom, telephone, hotel_id) OUTPUT INSERTED.ID values " +
                "(@nom,@prenom,@telephone,@hotel_id)";
            command = new SqlCommand(request, DataBase.Connection);
            command.Parameters.Add(new SqlParameter("@nom", client.Nom));
            command.Parameters.Add(new SqlParameter("@prenom", client.Prenom));
            command.Parameters.Add(new SqlParameter("@telephone", client.Telephone));
            command.Parameters.Add(new SqlParameter("@hotel_id", hotelId));
            DataBase.Connection.Open();
            client.Id = (int)command.ExecuteScalar();
            LiberCommandeEtFermerConnexion();
        }

        public List<Chambre> LireChambres()
        {
            List<Chambre> listes = new List<Chambre>();
            request = "SELECT id, tarif, statut, nbOccp from chambre where hotel_id = @hotelId";
            command = new SqlCommand(request, DataBase.Connection);
            command.Parameters.Add(new SqlParameter("@hotelId", hotelId));
            DataBase.Connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Chambre chambre = new Chambre()
                {
                    Numero = reader.GetInt32(0),
                    Tarif = reader.GetDecimal(1),
                    NbOccp = reader.GetInt32(3),
                    Statut = (StatutChambre)Convert.ToInt32(reader.GetString(2))
                };

                listes.Add(chambre);
            }
            reader.Close();
            LiberCommandeEtFermerConnexion();
            return listes;
        }

        public void EcrireChambres(List<Chambre> chambres)
        {
            DataBase.Connection.Open();
            transaction = DataBase.Connection.BeginTransaction();
            try
            {
                foreach (Chambre chambre in chambres)
                {
                    EcrireChambre(chambre);
                }
                transaction.Commit();
            }catch(Exception e)
            {
                transaction.Rollback();
            }
            DataBase.Connection.Close();

            
        }

        public List<Reservation> LireReservations()
        {
            List<Reservation> listes = new List<Reservation>();
            
            request = "SELECT r.id, r.statut, c.id, c.nom, c.prenom, c.telephone " +
                "from reservation r inner join client c on c.id = r.client_id where r.hotel_id = @hotelId ";
            command = new SqlCommand(request, DataBase.Connection);
            command.Parameters.Add(new SqlParameter("@hotelId", hotelId));
            DataBase.Connection.Open();
            reader = command.ExecuteReader();
            while(reader.Read())
            {
                Reservation r = new Reservation()
                {
                    Numero = reader.GetInt32(0),
                    Statut = (StatutReservation)Convert.ToInt32(reader.GetString(1)),
                    Client = new Client(reader.GetInt32(2), reader.GetString(3), reader.GetString(4), reader.GetString(5))
                };
                listes.Add(r);
            }
            reader.Close();
            command.Dispose();
            //Récupération des chambres se fait en lazy ça implique un nombre de requetes, par exemple pour 1000 réservations; de l'ordre(min 11000 requetes) <> en utilisant le mode eager (on sera à un ordre de 1000 requetes)
            foreach (Reservation r in listes)
            {
                LireChambresReservation(r);
            }
            DataBase.Connection.Close();
            return listes;
        }


        private void LireChambresReservation(Reservation reservation)
        {
            request = "SELECT id, tarif, statut, nbOccp from reservation_chambre rc inner join chambre c on c.id = rc.chambre_id " +
                "where rc.reservation_id = @reservationId";
            command = new SqlCommand(request, DataBase.Connection);
            command.Parameters.Add(new SqlParameter("@reservationId", reservation.Numero));
            DataBase.Connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Chambre chambre = new Chambre()
                {
                    Numero = reader.GetInt32(0),
                    Tarif = reader.GetDecimal(1),
                    NbOccp = reader.GetInt32(3),
                    Statut = (StatutChambre)Convert.ToInt32(reader.GetString(2))
                };

                reservation.Chambres.Add(chambre);
            }
            reader.Close();
            command.Dispose();
        }


        public void EcrireReservations(Reservation reservation)
        {
            DataBase.Connection.Open();
            transaction = DataBase.Connection.BeginTransaction();
            try
            {
                string request = "INSERT INTO reservation (statut, hotel_id)  OUTPUT INSERTED.ID " +
                "values (@statut, @hotelId)";
                command = new SqlCommand(request, DataBase.Connection, transaction);
                command.Parameters.Add(new SqlParameter("@statut", reservation.Statut));
                command.Parameters.Add(new SqlParameter("@hotelId", hotelId));
                reservation.Numero = (int)command.ExecuteScalar();
                command.Dispose();
                EcrireReservationChambres(reservation);
                transaction.Commit();
            }
            catch(Exception e)
            {
                transaction.Rollback();
            }
            DataBase.Connection.Close();
        }

        public void MiseAjourReservations(Reservation reservation)
        {
            
        }

        private void EcrireReservationChambres(Reservation reservation)
        {
            foreach(Chambre c in reservation.Chambres)
            {
                request = "INSERT into reservation_chambre (reservation_id, chambre_id) values " +
                    "(@reservationId, @chambreId)";
                command = new SqlCommand(request, DataBase.Connection, transaction);
                command.Parameters.Add(new SqlParameter("@reservationId", reservation.Numero));
                command.Parameters.Add(new SqlParameter("@chambreId", c.Numero));
                command.ExecuteNonQuery();
                command.Dispose();
            }
        }

        private void LiberCommandeEtFermerConnexion()
        {
            command.Dispose();
            DataBase.Connection.Close();
        }

        private void EcrireChambre(Chambre chambre)
        {
            request = "INSERT INTO Chambre (tarif, statut, nbOccp, hotel_id) output inserted.id values " +
                "(@tarif, @statut, @nbOccp, @hotel_id)";
            command = new SqlCommand(request, DataBase.Connection, transaction);
            command.Parameters.Add(new SqlParameter("@tarif", chambre.Tarif));
            command.Parameters.Add(new SqlParameter("@statut", chambre.Statut));
            command.Parameters.Add(new SqlParameter("@nbOccp", chambre.NbOccp));
            command.Parameters.Add(new SqlParameter("@hotel_id", hotelId));
            chambre.Numero = (int)command.ExecuteScalar();
            command.Dispose();
        }
    }
}
