using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP.State;

namespace ZTP.Pizza
{
    public class PizzaMala : PizzaBazowa
    {
        private readonly decimal PIZZA_MALA_CENA = 20.0M;
        private readonly int SREDNICA = 20;
        private readonly int LICZBA_CIEC = 2;

        public PizzaMala()
        {
            srednica = SREDNICA;
            liczba_ciec_oczekiwania = LICZBA_CIEC;
            cena = PIZZA_MALA_CENA;
            opis = "mala";
        }

        public override decimal GetCena() { return stan.GetCena(PIZZA_MALA_CENA); }
        public override string GetOpis() { return opis; }
        public override bool GetCzesci() { if (liczba_ciec == liczba_ciec_oczekiwania) return true; return false; }
    }
}
