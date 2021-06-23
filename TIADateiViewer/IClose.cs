using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIADateiViewer
{
    public interface IClose
    {
        Action Close { get; set; }
    }
}
