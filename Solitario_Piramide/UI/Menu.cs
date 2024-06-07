using Solitario_Piramide.Game.GameEngine;
using Solitario_Piramide.Inferfaces;
using Spectre.Console;
using System;

namespace Solitario_Piramide.UI
{
    public class Menu : IMenu
    {
        public void ShowWelcomeMenu()
        {
            while (true)
            {
                Console.Clear();
                AnsiConsole.Write(
                    new FigletText("Pyramid Solitaire")
                        .Centered()
                        .Color(Color.Green));

                var option = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Seleccione una opción:")
                        .AddChoices(new[] { "Iniciar Juego", "Salir" }));

                if (option == "Iniciar Juego")
                {
                    StartGame();
                }
                else if (option == "Salir")
                {
                    Environment.Exit(0);
                }
            }
        }

        private void StartGame()
        {
            var gameEngine = new GameEngine();
            gameEngine.StartGame();
        }
    }
}
