using System;
using Solitario_Piramide.Interfaces;
using Solitario_Piramide.Sound;
using Solitario_Piramide.UI;

namespace Solitario_Piramide
{
    class Program
    {
        static void Main(string[] args)
        {
            IMenu menu = new Menu();
            menu.ShowWelcomeMenu();
        }
    }
}
