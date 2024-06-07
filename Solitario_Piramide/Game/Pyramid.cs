using Solitario_Piramide.Game;
using Solitario_Piramide.Inferfaces;
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
    }
}
