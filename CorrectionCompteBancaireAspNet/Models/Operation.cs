using System;
using System.Collections.Generic;
using System.Text;

namespace CorrectionCompteBancaireAspNet.Models
{
    public class Operation
    {
        private int numero;
        private decimal montant;
        private DateTime dateOperation;
        
        public int Numero { get => numero; set => numero = value; }
        public decimal Montant { get => montant; }
        public DateTime DateOperation { get => dateOperation; set => dateOperation = value; }

        public Operation(decimal montant)
        {
            dateOperation = DateTime.Now;
            this.montant = montant;
        }

        public override string ToString()
        {
            return $"Date Opération : {DateOperation}, Montant : {Montant}";
        }
    }
}
