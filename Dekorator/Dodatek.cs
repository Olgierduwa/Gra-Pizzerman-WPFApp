using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP.Dekorator
{
    public abstract class Dodatek : PizzaBazowa
    {
        public abstract override decimal GetCena();
        public abstract override string GetOpis();
        public abstract override bool GetCzesci();
        public abstract void UzupelnijDane(PizzaBazowa pizza);
    }
}
