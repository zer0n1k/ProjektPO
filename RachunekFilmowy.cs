using System;
using System.Collections.Generic;
using System.Text;

namespace Z1
{
    public class RachunekFilmowy : RachunekBankowy
    {
        private string nazwaFirmy;
        public RachunekFilmowy(string numerRachunku, string właściciel, string nazwaFirmy) : base(numerRachunku, właściciel)
        {
            this.nazwaFirmy = nazwaFirmy;
        }
    }
}
