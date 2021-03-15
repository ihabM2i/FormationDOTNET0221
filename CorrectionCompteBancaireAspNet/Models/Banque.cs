using CorrectionCompteBancaireAspNet.Repositories;
using CorrectionCompteBancaireAspNet.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CorrectionCompteBancaireAspNet.Models
{
    public class Banque
    {
        private string nom;
        private List<Compte> comptes;

        public string Nom { get => nom; set => nom = value; }
        public List<Compte> Comptes { get => comptes; set => comptes = value; }

        public Banque(string nom)
        {
            Nom = nom;
            Comptes = new List<Compte>();
        }

        public void CreationCompte(Compte compte)
        {
            compte.ADecouvert += NotificationDecouvert;
            //Comptes.Add(compte);
            SqlTransaction transaction = null;
            SqlConnection connection = Connection.New;
            connection.Open();
            transaction = connection.BeginTransaction();
            try
            {
                ClientRepository clientRepository = new ClientRepository(connection, transaction);
                clientRepository.Create(compte.Client);
                CompteRepository compteRepository = new CompteRepository(connection, transaction);
                compteRepository.Create(compte);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            connection.Close();
        }

        public Compte RechercherCompte(int numero)
        {
            CompteRepository compteRepository = new CompteRepository(Connection.New);
            Compte compte = compteRepository.FindElementById(numero);
            if (compte != null) compte.ADecouvert += NotificationDecouvert;
            return compte;
        }

        public Compte RechercherCompteEtOperation(int numero)
        {
            Compte compte = RechercherCompte(numero);
            OperationRepository operationRepository = new OperationRepository(Connection.New);
            if (compte != null) compte.Operations = operationRepository.FindAll(compte.Numero);
            return compte;
        }

        public void NotificationDecouvert(int numero, decimal solde)
        {
            Console.WriteLine($"Le compte numéro {numero} est à decouvert, le solde est de {solde} euros");
            Console.WriteLine("Touche pour continuer....");
            Console.ReadLine();
        }
    }

}
