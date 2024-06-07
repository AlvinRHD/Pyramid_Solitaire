
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitario_Piramide.Interfaces
{
    public interface IPyramid
    {
        List<List<ICard>> Rows { get; set; }
        ICard GetCardAtPosition(int row, int col);
    }   
}
