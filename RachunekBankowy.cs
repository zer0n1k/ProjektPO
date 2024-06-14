using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Z1
{
    public class RachunekBankowy : IComparable<RachunekBankowy>
    {
        private string numerRachunku;
        private decimal saldo;
        private string właściciel;
        public string NumerRachunku { get => numerRachunku; }
        public decimal Saldo { get => saldo; }
        public string Właściciel { get => właściciel; }
        public RachunekBankowy(string numerRachunku, string właściciel)
        {
            this.numerRachunku = numerRachunku;
            this.właściciel = właściciel;
        }
        public bool Wpłać(decimal kwota)
        {
            if (kwota < 0)
            {
                OperacjaFinansowa?.Invoke(this, new OperacjaFinansowaArgs(kwota, "Błąd: Próba wpłaty środków mniejszych bądź równych zero."));
                return false;
            }
            OperacjaFinansowa?.Invoke(this, new OperacjaFinansowaArgs(kwota, "Wpłata środków"));
            saldo += kwota;
            return true;
        }
        public bool Wypłać(decimal kwota)
        {
            if(kwota < 0)
            {
                OperacjaFinansowa?.Invoke(this, new OperacjaFinansowaArgs(kwota, "Błąd: Brak wystarczających środków na rachunku."));
                return false;
            }
            if (saldo - kwota < 0)
            {
                OperacjaFinansowa?.Invoke(this, new OperacjaFinansowaArgs(kwota, "Błąd: Brak wystarczających środków na rachunku."));
                return false;
            }
            if (saldo - kwota >= 0)
            {
                saldo -= kwota;
                OperacjaFinansowa?.Invoke(this, new OperacjaFinansowaArgs(kwota, "Wypłata środków"));
                return true;
            }
            return false;
        }
        public int CompareTo(RachunekBankowy other)
        {
            if (other == null) return 1;
            return this.Właściciel.CompareTo(other.Właściciel);
        }
        public override bool Equals(object obj)
        {
            if (obj is RachunekBankowy)
            {
                return obj is RachunekBankowy bankowy &&
                       NumerRachunku == bankowy.NumerRachunku;
            }
            else
                return NumerRachunku == (string)obj;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(NumerRachunku);
        }
        public event OperacjaFinansowaEventHandler OperacjaFinansowa;
        public class SortowanieSaldaComparer : IComparer<RachunekBankowy>
        {
            public int Compare(RachunekBankowy x, RachunekBankowy y)
            {
                if(x == null || y == null) return 1;
                return y.Saldo.CompareTo(x.Saldo);
            }
        }

    }
}
