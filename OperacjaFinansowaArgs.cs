using System;
using System.Collections.Generic;
using System.Text;

namespace Z1
{
    public delegate void OperacjaFinansowaEventHandler(object sender, OperacjaFinansowaArgs e);
    public class OperacjaFinansowaArgs : EventArgs
    {
        public decimal Kwota { get; }
        public string Opis {  get; }
        public OperacjaFinansowaArgs(decimal kwota, string opis)
        {
            Kwota = kwota;
            Opis = opis;
        }
    }
}
