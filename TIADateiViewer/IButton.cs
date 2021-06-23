using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIADateiViewer
{
    public interface IButton
    {
        string Name { get; set; }
    }

    public abstract class Button : IButton
    {
        public string Name { get; set; }
    }
}
