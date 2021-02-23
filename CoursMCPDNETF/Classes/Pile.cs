using System;
using System.Collections.Generic;
using System.Text;

namespace CoursMCPDNETF.Classes
{
    class Pile<T>
    {
        T[] elements;
        int taille;
        int compteur = 0;

        public event Action<int> Pleine;

        public Pile(int t)
        {
            taille = t;
            elements = new T[taille];
        }

        public void Empiler(T element)
        {
            if (compteur < taille)
            {
                elements[compteur] = element;
                compteur++;
            }
            else
            {
                if (Pleine != null)
                    Pleine(taille);
            }
        }

        public void Deplier()
        {
            if (compteur > 0)
            {
                Console.WriteLine("Je dépile le dernier élément...");
                elements[compteur - 1] = default(T);
                compteur--;
            }

        }
        public T Get(int index)
        {
            return elements[index];
        }

        //Créer une méthode de recherche en fonction du type du générique 
        public T Find(Func<T,bool> search)
        {
            T element = default(T);
            foreach(T e in elements)
            {
                if(search(e))
                {
                    element = e;
                    break;
                }
            }
            return element;
        }
    }
}
