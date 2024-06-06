using Spectre.Console;
using Solitario_Piramide.Game;

namespace Solitario_Piramide.UI
{
    public class Renderer : IRenderer
    {
        public void RenderPyramid(Pyramid pyramid)
        {
            foreach (var row in pyramid.Rows)
            {
                foreach (var card in row)
                {
                    if (card != null)
                    {
                        if (card.IsFaceUp)
                        {
                            AnsiConsole.Write(new Markup($"[bold] {card.Value}{card.Suit[0]} [/]").Centered());
                        }
                        else
                        {
                            AnsiConsole.Write(new Markup($"[bold] XX [/]").Centered());
                        }
                    }
                    else
                    {
                        AnsiConsole.Write(new Markup("[bold] -- [/]").Centered());
                    }
                }
                AnsiConsole.WriteLine();
            }
        }

        public void RenderScore(int score)
        {
            AnsiConsole.WriteLine($"Score: {score}");
        }
    }
}
