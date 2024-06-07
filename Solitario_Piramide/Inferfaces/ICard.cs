using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitario_Piramide.Inferfaces
{
    public interface ICard
    {
        int Value { get; set; }
        string Suit { get; set; }
    }
}
