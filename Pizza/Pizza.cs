using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP.State;

namespace ZTP
{
    public abstract class PizzaBazowa
    {
        public int srednica;
        public int liczba_ciec = 0;
        public int liczba_ciec_oczekiwania;
        public decimal cena = 0;
        public string opis = "ciasto";
        public Stan stan = StanSurowa.GetStan();
        public TimeSpan czas;

        public abstract decimal GetCena();
        public abstract string GetOpis();
        public abstract bool GetCzesci();
        public string GetWypiek() { return stan.GetOpis(); }
        public  void Pieczenie(TimeSpan czas) { this.czas = czas; stan.Pieczenie(this); }
        public void Krojenie() { liczba_ciec++; }
    }
}
