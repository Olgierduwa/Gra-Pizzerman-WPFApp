using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP.Pizza;

namespace ZTP.State
{
    public class StanSrednioUpieczona : Stan
    {
        private static StanSrednioUpieczona stanSrednioUpieczona = null;
        public static StanSrednioUpieczona GetStan() { if (stanSrednioUpieczona == null) stanSrednioUpieczona = new StanSrednioUpieczona(); return stanSrednioUpieczona; }
        private StanSrednioUpieczona() { }

        private string opis = "srednio upieczona";
        private decimal marza = 1.0M;

        public override void Pieczenie(PizzaBazowa pizza) { if (pizza.czas.Seconds > 10) pizza.stan = StanDobrzeUpieczona.GetStan(); }
        public override decimal GetCena(decimal cena) { return cena * marza; }
        public override string GetOpis() { return opis; }
    }
}
