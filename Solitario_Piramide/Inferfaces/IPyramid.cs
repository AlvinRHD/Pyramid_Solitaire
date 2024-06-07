using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitario_Piramide.Inferfaces
{
    public interface IPyramid
    {
        List<List<ICard>> Rows { get; set; }
    }   
}
