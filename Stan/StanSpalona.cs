using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP.Pizza;

namespace ZTP.State
{
    public class StanSpalona : Stan
    {
        private static StanSpalona stanSpalona = null;
        public static StanSpalona GetStan() { if (stanSpalona == null) stanSpalona = new StanSpalona(); return stanSpalona; }
        private StanSpalona() { }

        private string opis = "spalona";
        private decimal marza = 0.3M;

        public override void Pieczenie(PizzaBazowa pizza) { pizza.stan = GetStan(); }
        public override decimal GetCena(decimal cena) { return cena * marza; }
        public override string GetOpis() { return opis; }
    }
}
