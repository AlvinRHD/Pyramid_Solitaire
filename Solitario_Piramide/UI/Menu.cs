using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitario_Piramide.UI
{
    public static class Menu
    {
        public static void ShowWelcomeMenu()
        {
            AnsiConsole.Write(new FigletText("Pyramid Solitaire")
                .Centered()
                .Color(Color.Green));

            AnsiConsole.Write(new Markup("[bold yellow]Welcome to Pyramid Solitaire![/]"));
            AnsiConsole.WriteLine();
            AnsiConsole.Write(new Markup("Press [bold green]Enter[/] to start the game."));
            Console.ReadLine();
        }
    }
}
