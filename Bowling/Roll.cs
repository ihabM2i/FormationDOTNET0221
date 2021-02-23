using System;
using System.Collections.Generic;
using System.Text;

namespace Bowling
{
    public class Roll
    {
        private int pins;

        public Roll(int pins)
        {
            this.pins = pins;
        }

        public int Pins { get => pins;}
    }
}
