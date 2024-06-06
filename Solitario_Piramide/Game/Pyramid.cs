using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitario_Piramide.Game
{
    public class Pyramid
    {
        public List<Card[]> Rows { get; }

        public Pyramid(Deck deck)
        {
            Rows = new List<Card[]>();
            for (int i = 1; i <= 7; i++)
            {
                Card[] row = new Card[i];
                for (int j = 0; j < i; j++)
                {
                    row[j] = deck.DrawCard();
                    row[j].IsFaceUp = true;
                }
                Rows.Add(row);
            }
        }
    }
}
