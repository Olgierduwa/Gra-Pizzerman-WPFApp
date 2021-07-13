using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP.State;

namespace ZTP.Pizza
{
    public class PizzaSrednia : PizzaBazowa
    {
        private readonly decimal PIZZA_SREDNIA_CENA = 30.0M;
    	private readonly int SREDNICA = 30;
    	private readonly int LICZBA_CIEC = 3;

        public PizzaSrednia()
        {
            srednica = SREDNICA;
            liczba_ciec_oczekiwania = LICZBA_CIEC;
            cena = PIZZA_SREDNIA_CENA;
            opis = "srednia";
        }

        public override decimal GetCena() { return stan.GetCena(PIZZA_SREDNIA_CENA); }
        public override string GetOpis() { return opis; }
        public override bool GetCzesci() { if (liczba_ciec == liczba_ciec_oczekiwania) return true; return false; }
    }
}
