using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP.State;

namespace ZTP.Pizza
{
    public class PizzaDuza : PizzaBazowa
    {
        private readonly decimal PIZZA_DUZA_CENA = 40.0M;
	    private readonly int SREDNICA = 40;
	    private readonly int LICZBA_CIEC = 4;

        public PizzaDuza()
        {
            srednica = SREDNICA;
            liczba_ciec_oczekiwania = LICZBA_CIEC;
            cena = PIZZA_DUZA_CENA;
            opis = "duza";
        }

        public override decimal GetCena() { return stan.GetCena(PIZZA_DUZA_CENA);}
        public override string GetOpis() { return opis; }
        public override bool GetCzesci() { if (liczba_ciec == liczba_ciec_oczekiwania) return true; return false; }
    }
}
