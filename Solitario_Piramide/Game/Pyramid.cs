using Solitario_Piramide.Game;
using Solitario_Piramide.Interfaces;
using System.Collections.Generic;

namespace Solitario_Piramide.Game
{
    public class Pyramid : IPyramid
    {
        public List<List<ICard>> Rows { get; set; }

        public Pyramid()
        {
            Rows = new List<List<ICard>>();
        }

        public void SetUpPyramid(Deck deck)
        {
            for (int row = 0; row < 7; row++)
            {
                var rowList = new List<ICard>();
                for (int col = 0; col <= row; col++)
                {
                    rowList.Add(deck.DrawCard());
                }
                Rows.Add(rowList);
            }
        }

        public ICard GetCardAtPosition(int row, int col)
        {
            if (row < 0 || row >= Rows.Count || col < 0 || col >= Rows[row].Count)
            {
                return null;
            }
            return Rows[row][col];
        }
    }
}
