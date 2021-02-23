using System;
using System.Collections.Generic;
using System.Text;

namespace Caisse.Classes
{
    class Product
    {
        private int id;
        private string title;
        private decimal price;
        private int stock;
        private static int count = 0;

        public int Id { get => id;}
        public string Title { get => title; set => title = value; }
        public decimal Price { get => price; set => price = value; }
        public int Stock { get => stock; set => stock = value; }
    

        public Product()
        {
            id = ++count;
        }

        public override string ToString()
        {
            return $"Id : {Id}, Titre : {Title}, Prix : {Price}";
        }
    }
}
