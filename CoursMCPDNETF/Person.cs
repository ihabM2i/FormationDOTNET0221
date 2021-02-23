using System;
using System.Collections.Generic;
using System.Text;

namespace CoursMCPDNETF
{
    abstract class Person : IDisplay
    {
        private string firstName;
        private string lastName;

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }

        public void Display()
        {
            Console.WriteLine("Nom : {0}, Prénom : {1}", FirstName, LastName);
        }
    }
}
