using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP.Pizza;

namespace ZTP.State
{
    public class StanDobrzeUpieczona : Stan
    {
        private static StanDobrzeUpieczona stanDobrzeUpieczona = null;
        public static StanDobrzeUpieczona GetStan() { if (stanDobrzeUpieczona == null) stanDobrzeUpieczona = new StanDobrzeUpieczona(); return stanDobrzeUpieczona; }
        private StanDobrzeUpieczona() { }

        private string opis = "dobrze upieczona";
        private decimal marza = 1.0M;

        public override void Pieczenie(PizzaBazowa pizza) { if (pizza.czas.Seconds > 15) pizza.stan = StanSpalona.GetStan(); }
        public override decimal GetCena(decimal cena) { return cena * marza; }
        public override string GetOpis() { return opis; }
    }
}
