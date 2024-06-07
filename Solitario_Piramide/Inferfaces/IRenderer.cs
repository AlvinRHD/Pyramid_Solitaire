using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitario_Piramide.Interfaces
{
    public interface IRenderer
    {
        void RenderPyramid(IPyramid pyramid, int score);
    }
}
