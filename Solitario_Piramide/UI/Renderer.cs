using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitario_Piramide.Game;
using Spectre.Console;

namespace Solitario_Piramide.UI
{
    public static class Renderer
    {
        public static void RenderPyramid(Pyramid pyramid)
        {
            foreach (var row in pyramid.Rows)
            {
                foreach (var card in row)
                {
                    AnsiConsole.Write(new Markup($"[bold] {card.Value}{card.Suit[0]} [/]").Centered());
                }
                AnsiConsole.WriteLine();
            }
        }
    }
}
