using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitario_Piramide
{
    public class Player
    {
        public int Score { get; private set; }

        public Player()
        {
            Score = 0;
        }

        public void AddPoints(int points)
        {
            Score += points;
        }
    }
}
