using System;
using System.Collections.Generic;
using System.Text;

namespace CoursMCPDNETF.Classes
{
    public class Calculatrice
    {
        //Pour créer des delegate on peut utiliser le mot clé delegate avec la signature des méthodes qu'on souhaite faire passer par le delegate
        //public delegate double MethodeCalcule(double a, double b);
        //Ou on peut utiliser les génériques Action and Func
        //Les génériques Action sont utilisés pour des méthodes qui sont void
        //Les génériques Func sont utilisés pour des méthodes avec un type de retour
        public Func<double,double,double> MethodeCalcule;

        //Exemple de delegate de type Action avec deux arguements de type double

        public Action<double, double> ExecuteCalcule;
        public double Addition(double a, double b)
        {
            return a + b;
        }

        public double Soustraction(double a, double b)
        {
            return a - b;
        }

        //La méthode calculer récupère deux paramètres a et b et la méthode de calcule
        //Utilisation du delegate avec le mot clé Func<arg1, arg2, typeRetour>
        public void Calculer(double a, double b, Func<double, double, double> methode)
        {
            Console.WriteLine(methode(a, b));
        }

        public void MultiCalcule(double a, double b)
        {
            ExecuteCalcule(a, b);
        }
    }
}
