using System;
using System.Collections.Generic;
using System.Text;

namespace CoursMCPDNETF
{
    class Student : Person
    {
        private int level;

        public int Level { get => level; set => level = value; }

        public void DisplayLevel()
        {
            Console.WriteLine("Level {0}", Level);
        }
    }
}
