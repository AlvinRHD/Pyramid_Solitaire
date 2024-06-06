using System;
using Solitario_Piramide.Game;
using Solitario_Piramide.UI;

namespace PyramidSolitaire
{
    class Program
    {
        static void Main(string[] args)
        {
            var gameEngine = new PyramidSolitaire.Game.GameEngine();
            gameEngine.Run();
        }
    }
}