using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitario_Piramide.Inferfaces;
using static System.Formats.Asn1.AsnWriter;

namespace Solitario_Piramide
{
    public class Player : IPlayer
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
        public void ResetScore()
        {
            Score = 0;
        }
    }
}
