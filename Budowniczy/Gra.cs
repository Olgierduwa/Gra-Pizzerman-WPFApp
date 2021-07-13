using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP.Budowniczy;

namespace ZTP.Budowniczy
{
    public class Gra
    {
        private IBudowniczy budowniczy;

        public void StworzPizze(IBudowniczy budowniczy)
        {
            this.budowniczy = budowniczy;
            budowniczy.Walkowanie();
        }
        public PizzaBazowa GetPizza()
        {
            return budowniczy.GetPizza();
        }
    }
}
