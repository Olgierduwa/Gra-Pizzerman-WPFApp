using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP.Pizza;

namespace ZTP.State
{
    public class StanSurowa : Stan
    {
        private static StanSurowa stanSurowa = null;
        public static StanSurowa GetStan() { if (stanSurowa == null) stanSurowa = new StanSurowa(); return stanSurowa; }
        private StanSurowa() { }

        private string opis = "surowa";
        private decimal marza = 0.3M;

        public override void Pieczenie(PizzaBazowa pizza) { if (pizza.czas.Seconds > 5) pizza.stan = StanSrednioUpieczona.GetStan(); }
        public override decimal GetCena(decimal cena) { return cena * marza; }
        public override string GetOpis() { return opis; }
    }
}
