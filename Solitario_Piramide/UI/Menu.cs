using Spectre.Console;

namespace PyramidSolitaire.UI
{
    public class Menu : IMenu
    {
        public void ShowWelcomeMenu()
        {
            AnsiConsole.Write(
                new FigletText("Pyramid Solitaire")
                    .Centered()
                    .Color(Color.Green));

            AnsiConsole.WriteLine("Welcome to Pyramid Solitaire!");
        }
    }
}
