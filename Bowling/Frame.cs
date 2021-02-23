using System;
using System.Collections.Generic;
using System.Text;

namespace Bowling
{
    public class Frame
    {
        private int score;
        private List<Roll> rolls;
        //Ajouter le générateur 
        private IGenerator generator;

        private bool lastFrame;

        public int Score
        {
            get
            {
                Rolls.ForEach(r =>
                {
                    score += r.Pins;
                });
                return score;
            }
                
        }
        public List<Roll> Rolls { get => rolls; set => rolls = value; }
        public bool LastFrame { get => lastFrame; set => lastFrame = value; }

        public Frame(IGenerator g)
        {
            generator = g;
            Rolls = new List<Roll>();
            score = 0;
        }
        public bool Roll()
        {
            int nbRolls = Rolls.Count;
            int randomPins;
            if (nbRolls == 0)
            {
                randomPins = generator.RandomPins(10);
                Rolls.Add(new Roll(randomPins));
            }
            else if (nbRolls == 1)
            {
                Roll r = Rolls[0];
                if (r.Pins == 10)
                {
                    return false;
                }
                randomPins = generator.RandomPins(10 - r.Pins);
                Rolls.Add(new Roll(randomPins));
            }
            else
            {
                return false;
            }
            return true;
        }


    }
}
