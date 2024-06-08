using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitario_Piramide.Interfaces
{
    public interface ICard
    {
        int Value { get; set; }
        string Suit { get; set; }
    }
}
