using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP.Dekorator
{
    public class Sos : Dodatek
    {
        PizzaBazowa pizza;
        public Sos(PizzaBazowa pizza) { this.pizza = pizza; UzupelnijDane(pizza); }
        public override decimal GetCena() { pizza.stan = this.stan; return pizza.GetCena() + 3; }
        public override string GetOpis() { return pizza.GetOpis() + ", sos"; }
        public override bool GetCzesci() { if (liczba_ciec == pizza.liczba_ciec_oczekiwania) return true; return false; }
        public override void UzupelnijDane(PizzaBazowa pizza)
        {
            this.srednica = pizza.srednica;
            this.liczba_ciec = pizza.liczba_ciec;
            this.liczba_ciec_oczekiwania = pizza.liczba_ciec_oczekiwania;
            this.cena = pizza.cena;
            this.opis = pizza.opis;
            this.stan = pizza.stan;
            this.czas = pizza.czas;
        }
    }
}
