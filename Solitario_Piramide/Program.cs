using System;

namespace PyramidSolitaire
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu.ShowWelcomeMenu();
            GameEngine gameEngine = new GameEngine();
            gameEngine.StartGame();
        }
    }
}