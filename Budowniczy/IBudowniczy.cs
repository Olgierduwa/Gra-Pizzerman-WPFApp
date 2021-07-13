using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP.Budowniczy
{
    public interface IBudowniczy 
    {
        PizzaBazowa GetPizza();
        void Walkowanie();
    }
}
