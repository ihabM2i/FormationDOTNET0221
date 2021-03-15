using CorrectionCompteBancaireAspNet.Repositories;
using CorrectionCompteBancaireAspNet.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CorrectionCompteBancaireAspNet.Models
{
    public class Compte
    {
        private int numero;
        private decimal solde;
        private Client client;
        private List<Operation> operations;
        
        private decimal maxDecouvert;

        public event Action<int, decimal> ADecouvert;
        public int Numero { get => numero; set => numero = value; }
        public decimal Solde { get => solde; set => solde = value; }
        public Client Client { get => client; set => client = value; }
        public List<Operation> Operations { get => operations; set => operations = value; }
        public decimal MaxDecouvert { get => maxDecouvert; set => maxDecouvert = value; }

        public Compte()
        {            
            Operations = new List<Operation>();
            solde = 0;
            MaxDecouvert = 200;
        }

        public virtual bool Depot(Operation operation)
        {
            bool result = false;
            if(operation.Montant > 0)
            {
                Operations.Add(operation);              
                SqlConnection connection = Connection.New;
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    OperationRepository operationRepository = new OperationRepository(connection, transaction);
                    operationRepository.Create(operation, Numero);
                    solde += operation.Montant;
                    CompteRepository compteRepository = new CompteRepository(connection, transaction);
                    compteRepository.Update(this);
                    transaction.Commit();          
                    result = true;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                }
                connection.Close();           
            }
            return result;
        }

        public virtual bool Retrait(Operation operation)
        {
            bool result = false;
            if (Math.Abs(Solde - Math.Abs(operation.Montant)) >= MaxDecouvert  && operation.Montant < 0)
            {
                Operations.Add(operation);
                //Mise à jour de la base de données
                SqlConnection connection = Connection.New;
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    OperationRepository operationRepository = new OperationRepository(connection, transaction);
                    operationRepository.Create(operation, Numero);
                    solde += operation.Montant;
                    CompteRepository compteRepository = new CompteRepository(connection, transaction);
                    compteRepository.Update(this);
                    transaction.Commit();
                    if (Solde < 0)
                    {
                        if (ADecouvert != null)
                        {
                            ADecouvert(Numero, Solde);
                        }
                    }
                    result = true;
                }
                catch(Exception e)
                {
                    transaction.Rollback();
                }
                connection.Close();
            }
            return result;
        }


        public override string ToString()
        {
            string retour = $"====Compte Numéro : {Numero}, Solde : {Solde} euros ====\n";
            retour+=$"Client : {Client.ToString()} \n";
            retour += "----Liste des opérations----\n";
            foreach(Operation o in Operations)
            {
                retour += o.ToString() + "\n";
            }
            return retour;
        }
    }
}
