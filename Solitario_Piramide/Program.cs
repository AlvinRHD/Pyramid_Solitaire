using System;
using Solitario_Piramide.Game;


namespace Solitario_Piramide
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