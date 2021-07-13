using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP.Pizza;

namespace ZTP.Budowniczy
{
    public class BudowniczyDuzaPizza : IBudowniczy
    {
        private PizzaBazowa pizza;
        public PizzaBazowa GetPizza() { return pizza; }
        public void Walkowanie() { pizza = new PizzaDuza(); }
    }
}
