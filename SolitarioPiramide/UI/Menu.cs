using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitarioPiramide.UI
{
    public static class Menu
    {
        public static void DisplayMainMenu()
        {
            Console.Clear();
            // Crear un texto Figlet para el título
            var title = new FigletText("Bienvenido al Solitario Pirámide!")
                .Centered()
                .Color(Color.Yellow);
            AnsiConsole.Write(title);

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Seleccione una opción:")
                    .AddChoices(new[] { "Jugar", "Ayuda", "Salir" }));

            switch (option)
            {
                case "Jugar":
                    Game.GameManager.StartNewGame();
                    break;
                case "Ayuda":
                    DisplayHelp();
                    break;
                case "Salir":
                    Environment.Exit(0);
                    break;
            }
        }

        public static void DisplayHelp()
        {
            Console.Clear();
            AnsiConsole.Markup("[bold yellow]Instrucciones del juego:[/]\n");
            AnsiConsole.Write(
            """
            Se crea un mazo aleatorio, o una pirámide de cartas, intenta juntar 2 cartas que sus valores sumen un total de 13 hasta que se termine la pirámide y podrás ganar.

            Valor de las Cartas:
            As: 1 punto
            2 a 10: su valor numérico
            Jota (J): 11 puntos
            Reina (Q): 12 puntos
            Rey (K): 13 puntos

            """);
            AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Seleccione:")
                .AddChoices(new[] { "Volver al menú principal" }));

            DisplayMainMenu();
        }
    }
}
