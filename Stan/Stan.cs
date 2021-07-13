using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP.Pizza;

namespace ZTP.State
{
    public abstract class Stan
    {
        public abstract void Pieczenie(PizzaBazowa pizza);
        public abstract string GetOpis();
        public abstract decimal GetCena(decimal cena);
    }
}
