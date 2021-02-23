using CoursMCPDNETF.Classes;
using CoursMCPDNETF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace CoursMCPDNETF
{
    class Program
    {
        static void Main(string[] args)
        {
            #region rappel Abstract
            //Person p = new Person();
            //Création d'un objet Student
            //Student s = new Student()
            //{
            //    FirstName = "Abadi",
            //    LastName = "Ihab"
            //};
            //Création d'un objet Employee
            //Employee e = new Employee()
            //{
            //    FirstName = "titi",
            //    LastName = "Minet"
            //};
            //Utilisation de la méthode display pour les objets e et s déclarée dans la classe mère Person
            //e.Display();
            //s.Display();

            //List<Person> persons = new List<Person>();
            //persons.Add(e);
            //persons.Add(s);
            //foreach(Person p in persons)
            //{
            //    p.Display();
            //    //Convertir en utilisant le boxing () => génère une exception
            //    //if(p.GetType() == typeof(Student))
            //    //{
            //    //    Student se = (Student)p;
            //    //    se.DisplayLevel();
            //    //}
            //    //Convertir en utilisant le as
            //    //Student se = p as Student;
            //    //if(se != null)
            //    //{
            //    //    se.DisplayLevel();
            //    //}
            //    //Convertir en utilisant le is
            //    if(p is Student se)
            //    {
            //        se.DisplayLevel();
            //    }
            //}
            #endregion

            #region cours Interface
            //IDisplay voiture = new Car() { Model = "kia"};
            //IDisplay eleve = new Student() { FirstName = "abadi", LastName = "ihab" };
            //List<IDisplay> liste = new List<IDisplay>();
            //liste.Add(eleve);
            //liste.Add(voiture);            
            //foreach(IDisplay i in liste)
            //{
            //    i.Display();
            //}
            #endregion

            #region correction exercice Figure
            //List<Figure> figures = new List<Figure>();
            //figures.Add(new Rectangle(10, 10, 10, 20));
            //figures.Add(new Carre(10, 10, 10));
            //figures.Add(new Triangle(10, 10, 10, 20));
            //foreach(Figure f in figures)
            //{
            //    f.Afficher();
            //    //On récupère le type de l'objet f ensuite on récupère la liste des interfaces et on vérifie si on a l'interface Ideformable
            //    if(f.GetType().GetInterfaces().Contains(typeof(IDeformable)))
            //    {
            //        IDeformable figureDeformable = f as IDeformable;
            //        Figure newFigure = figureDeformable.Deformation(2, 1);
            //        newFigure.Afficher();
            //    }
            //}
            #endregion

            #region suite cours Interface
            GenerateurDeMot generateur = new GenerateurDeMot();
            GenerateurDeMotVersion2 generateur2 = new GenerateurDeMotVersion2();
            JeuPendu jeu = new JeuPendu(generateur);
            #endregion

            #region cours Suite méthode (Délégué, expression lambda)
            //Calculatrice c = new Calculatrice();
            //Raccourci comment => shift+ctrl+: ou ctrl+k+c
            //Raccourci no comment => shift+ctrl+: ou ctrl+k+u
            //c.Calculer(10, 30, c.Addition);
            //c.Calculer(10, 30, c.Soustraction);
            //c.Calculer(10, 30, Multiplication);
            //c.Calculer(34, 3, Modulo);
            //Expression lambda => est un façon d'écrire une fonction anonyme (cad sans avoir de nom de méthode)
            //Exemple création d'une expression lambda pour la division
            //c.Calculer(40, 4, delegate (double a, double b) { return a / b; });
            //c.Calculer(40, 4, (double a, double b) => { return a / b; });
            //c.Calculer(40, 4, (a, b) =>  a / b);
            //Exemple d'utilisation des expressions lambda avec une recherche

            //List<Car> cars = new List<Car>()
            //{
            //    new Car(){Model = "Ford"},
            //    new Car(){Model = "Kia"},
            //    new Car(){Model = "Fiat"},
            //};
            //Recherche sans expression lambda
            /*Car car = null;
            foreach(Car c in cars)
            {
                if(c.Model == "Ford")
                {
                    car = c;
                    break;
                }
            }
            if(car != null)
            {
                Console.WriteLine("Voiture trouvée");
            }*/
            //Avec expression lambda
            //Car car = cars.Find(a => a.Model == "Ford");
            //Correction méthode find d'une pile
            //Pile<Student> pile = new Pile<Student>(3);
            //pile.Empiler(new Student { FirstName = "toto", LastName = "tata" });
            //pile.Empiler(new Student { FirstName = "titi", LastName = "minet" });
            ////Student e = pile.Find(s => s.FirstName == "titi");
            //Student e = pile.Find(s => s.FirstName.Contains("t") && s.LastName.Contains("m"));
            #endregion

            #region suite cours delegate multicast et Event 

            //Utilisation du multicast des delegates
            //Calculatrice calculatrice = new Calculatrice();
            //calculatrice.ExecuteCalcule = Multiplication;
            //calculatrice.ExecuteCalcule += Addition;
            //calculatrice.MultiCalcule(10, 20);

            //Exemple d'utilisation d'event promotion avec la classe voiture
            /*Car car = new Car() { Model = "ford", Price = 10000 };
            //Ecouter l'event promotion
            Notification notification = new Notification();
            //n'importe quelle méthode qui respecte le delegate de l'event promotion peut écouter l'event
            car.Promotion += notification.NotifyBySms;
            car.Promotion += notification.NotifyByEmail;
            string choix;
            int countPromotion = 0;
            do
            {
                Console.Write("Promotion ?  (O/N) : ");
                choix = Console.ReadLine();
                if(choix == "O")
                {
                    Console.Write("Montant reduction : ");
                    decimal reduction = Convert.ToDecimal(Console.ReadLine());
                    car.Reduction(reduction);
                    countPromotion++;
                    if(countPromotion == 3)
                    {
                        car.Promotion -= notification.NotifyBySms;
                        car.Promotion -= notification.NotifyByEmail;
                    }
                    else if(countPromotion == 5)
                    {
                        car.Promotion += notification.NotifyBySms;
                    }
                }
            } while (choix != "0");*/
            //Exemple d'utilisation de pile avec event pile pleine
            //Pile<int> pileEntier = new Pile<int>(4);
            //Notification notification = new Notification();
            //pileEntier.Pleine += notification.NotifyPilePleine;
            //pileEntier.Empiler(1);
            //pileEntier.Empiler(5);
            //pileEntier.Empiler(4);
            //pileEntier.Empiler(2);
            //pileEntier.Empiler(6);
            #endregion

            #region cours Création, lecture et écriture Fichier
            //Ecriture fichier txt
            //on utilise un objet StreamWriter
            //StreamWriter writer = new StreamWriter("fichier.html");
            //writer.WriteLine("<h1>Bonjour tout le monde</h1>");
            ////fermeture du flux d'ecriture
            //writer.Close();
            //Lecture d'un fichier txt
            //if(File.Exists("fichier.txt"))
            // {
            //     StreamReader reader = new StreamReader("fichier.txt");
            //     //lire la totalité du fichier
            //     //Console.WriteLine(reader.ReadToEnd());
            //     //lire ligne par ligne
            //     string ligne = reader.ReadLine();
            //     while(ligne != null)
            //     {
            //         Console.WriteLine(ligne);
            //         ligne = reader.ReadLine();
            //     }
            //     reader.Close();
            // }
            // else
            // {
            //     Console.WriteLine("Aucun fichier avec ce chemin");
            // }
            //Ecrire un fichier CSV
            //List<Student> students = new List<Student>();
            //students.Add(new Student() { FirstName = "toto", LastName = "tata" });
            //students.Add(new Student() { FirstName = "titi", LastName = "minet" });
            //StreamWriter writer = new StreamWriter("students.csv");
            //foreach (Student s in students)
            //{
            //    writer.WriteLine($"{s.FirstName};{s.LastName}");
            //    writer.WriteLine(s.FirstName + ";" + s.LastName);
            //    writer.WriteLine("{0};{1}", s.FirstName, s.LastName);
            //}
            ////writer.Close();
            ////Lecture un fichier CSV
            //List<Student> students = new List<Student>();
            //if(File.Exists("students.csv"))
            //{
            //    StreamReader reader = new StreamReader("students.csv");
            //    //lire la totalité du fichier
            //    //Console.WriteLine(reader.ReadToEnd());
            //    //lire ligne par ligne
            //    string ligne = reader.ReadLine();
            //    while (ligne != null)
            //    {
            //        Console.WriteLine(ligne);
            //        //Convertir la ligne en student
            //        string[] chaines = ligne.Split(';');
            //        Student s = new Student { FirstName = chaines[0], LastName = chaines[1] };
            //        students.Add(s);
            //        ligne = reader.ReadLine();
            //    }
            //    reader.Close();
            //}
            #endregion
        }

        //static double Multiplication(double a, double b)
        //{
        //    return a * b;
        //}
        static void Multiplication(double a, double b)
        {
            Console.WriteLine(a * b);
        }

        static void Addition(double a, double b)
        {
            Console.WriteLine(a + b);
        }

        static double Modulo(double a, double b)
        {
            return a % b;
        }


        
    }
}
