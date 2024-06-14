using System;
using System.Reflection;
using NUnit.Framework;

namespace Z1.Tests
{
    [TestFixture]
    public class RachunekBankowyTests
    {
        [Test]
        [Category("Właściwość")]
        public void NumerRachunku_Getter_DziałaPoprawnie()
        {
            // Arrange
            string numerRachunku = "123456789";
            RachunekBankowy rachunek = new RachunekOsobisty(numerRachunku, "Jan Kowalski");

            // Act
            string retrievedNumerRachunku = rachunek.NumerRachunku;

            // Assert
            Assert.AreEqual(numerRachunku, retrievedNumerRachunku);
        }

        [Test]
        [Category("Właściwość")]
        public void Wlasciciel_Getter_DziałaPoprawnie()
        {
            // Arrange
            string właściciel = "Jan Kowalski";
            RachunekBankowy rachunek = new RachunekOsobisty("123456789", właściciel);

            // Act
            string retrievedWłaściciel = rachunek.Właściciel;

            // Assert
            Assert.AreEqual(właściciel, retrievedWłaściciel);
        }

        [Test]
        [Category("Właściwość")]
        public void NumerRachunku_Setter_NieJestDostepny()
        {
            // Arrange
            PropertyInfo propertyInfo = typeof(RachunekBankowy).GetProperty("NumerRachunku");

            // Act
            bool isSetterAvailable = propertyInfo.GetSetMethod(true) != null;

            // Assert
            Assert.IsFalse(isSetterAvailable);
        }

        [Test]
        [Category("Właściwość")]
        public void Wlasciciel_Setter_NieJestDostepny()
        {
            // Arrange
            PropertyInfo propertyInfo = typeof(RachunekBankowy).GetProperty("Właściciel");

            // Act
            bool isSetterAvailable = propertyInfo.GetSetMethod(true) != null;

            // Assert
            Assert.IsFalse(isSetterAvailable);
        }

        [Test]
        [Category("Wpłata")]
        public void Wpłata_PoprawnaKwota_WpłataWykonanaPoprawnie()
        {
            // Arrange
            RachunekBankowy rachunek = new RachunekOsobisty("123456789", "Jan Kowalski");
            // Act
            var res = rachunek.Wpłać(100);
            decimal aktualneSaldo = rachunek.Saldo;
                                                                  
            // Assert
            Assert.AreEqual(100, aktualneSaldo);
            Assert.IsTrue(res);
        }


        [Test]
        [Category("Wpłata")]
        public void Wpłata_NiepoprawnaKwota_BłądPróbaWpłatyZaPomocąUjemnejKwoty()
        {
            // Arrange
            RachunekBankowy rachunek = new RachunekOsobisty("123456789", "Jan Kowalski");
            // Act
            var res = rachunek.Wpłać(-100);
            decimal aktualneSaldo = rachunek.Saldo;

            // Assert
            Assert.AreEqual(0, aktualneSaldo);
            Assert.IsFalse(res);
        }

        [Test]
        [Category("Wpłata")]
        public void Wpłata_WielokrotnaPoprawnaKwota_WpłataWykonanaPoprawnie()
        {
            // Arrange
            RachunekBankowy rachunek = new RachunekOsobisty("123456789", "Jan Kowalski");
            // Act
            rachunek.Wpłać(100);
            rachunek.Wpłać(100);
            rachunek.Wpłać(100);
            var res = rachunek.Wpłać(100);
            decimal aktualneSaldo = rachunek.Saldo;

            // Assert
            Assert.AreEqual(400, aktualneSaldo);
            Assert.IsTrue(res);
        }

        [Test]
        [Category("Wpłata")]
        public void Wpłata_WielokrotnaNiepoprawnaKwota_BłądPróbaWpłatyZaPomocąUjemnejKwoty()
        {
            // Arrange
            RachunekBankowy rachunek = new RachunekOsobisty("123456789", "Jan Kowalski");
            // Act
            rachunek.Wpłać(100);
            rachunek.Wpłać(-100);
            var res = rachunek.Wpłać(-100);
            rachunek.Wpłać(0);
            decimal aktualneSaldo = rachunek.Saldo;

            // Assert
            Assert.AreEqual(100, aktualneSaldo);
            Assert.IsFalse(res);
        }


        [Test]
        [Category("Wypłata")]
        public void Wypłata_PoprawnaKwota_WypłataWykonanaPoprawnie()
        {
            // Arrange
            RachunekBankowy rachunek = new RachunekOsobisty("123456789", "Jan Kowalski");

            // Act
            var res = rachunek.Wpłać(100);
            rachunek.Wypłać(100);

            // Assert
            decimal saldoKońcowe = rachunek.Saldo;
            Assert.AreEqual(0, saldoKońcowe);
            Assert.IsTrue(res);
        }

        [Test]
        [Category("Wypłata")]
        public void Wypłata_NiepoprawnaKwota_BłądBrakuŚrodków()
        {
            // Arrange
            RachunekBankowy rachunek = new RachunekOsobisty("123456789", "Jan Kowalski");
            decimal saldoPoczątkowe = rachunek.Saldo;
            decimal kwota = 1000;

            // Act
            var res = rachunek.Wypłać(kwota);

            // Assert
            decimal saldoKońcowe = rachunek.Saldo;
            Assert.AreEqual(saldoPoczątkowe, saldoKońcowe);
            Assert.IsFalse(res);
        }

        [Test]
        [Category("Wypłata")]
        public void Wypłata_NiepoprawnaKwota_BłądPróbaWypłatyZaPomocąUjemnejKwoty()
        {
            // Arrange
            RachunekBankowy rachunek = new RachunekOsobisty("123456789", "Jan Kowalski");
            decimal saldoPoczątkowe = rachunek.Saldo;
            decimal kwota = -50;

            // Act
            var res = rachunek.Wypłać(kwota);

            // Assert
            decimal saldoKońcowe = rachunek.Saldo;
            Assert.AreEqual(saldoPoczątkowe, saldoKońcowe);
            Assert.IsFalse(res);
        }

        [Test]
        [Category("CompareTo")]
        public void CompareTo_RachunekOsobisty_WiekszyWynikGdyWlascicieleJestPozniejWAlfabecie()
        {
            // Arrange
            RachunekBankowy rachunek1 = new RachunekOsobisty("123456789", "Kowalski");
            RachunekBankowy rachunek2 = new RachunekOsobisty("987654321", "Nowak");

            // Act
            int result = rachunek1.CompareTo(rachunek2);

            // Assert
            Assert.Less(result, 0);
        }

        [Test]
        [Category("CompareTo")]
        public void CompareTo_RachunekOsobisty_MniejszyWynikGdyWlascicieleJestWczesniejWAlfabecie()
        {
            // Arrange
            RachunekBankowy rachunek1 = new RachunekOsobisty("123456789", "Nowak");
            RachunekBankowy rachunek2 = new RachunekOsobisty("987654321", "Kowalski");

            // Act
            int result = rachunek1.CompareTo(rachunek2);

            // Assert
            Assert.Greater(result, 0);
        }

        [Test]
        [Category("CompareTo")]
        public void CompareTo_RachunekOsobisty_RownyWynikGdyWlascicieleSaTacySami()
        {
            // Arrange
            RachunekBankowy rachunek1 = new RachunekOsobisty("123456789", "Kowalski");
            RachunekBankowy rachunek2 = new RachunekOsobisty("987654321", "Kowalski");

            // Act
            int result = rachunek1.CompareTo(rachunek2);

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        [Category("Comparer")]
        public void Compare_RachunekOsobisty_SaldoXWiekszeNizSaldoY_WynikMniejszyOdZera()
        {
            // Arrange
            RachunekBankowy rachunekX = new RachunekOsobisty("123456789", "Jan Kowalski");
            rachunekX.Wpłać(1000); // Saldo = 1000
            RachunekBankowy rachunekY = new RachunekOsobisty("987654321", "Anna Nowak");
            rachunekY.Wpłać(500); // Saldo = 500
            var comparer = new RachunekBankowy.SortowanieSaldaComparer();

            // Act
            int result = comparer.Compare(rachunekX, rachunekY);

            // Assert
            Assert.Less(result, 0);
        }

        [Test]
        [Category("Comparer")]
        public void Compare_RachunekOsobisty_SaldoXMniejszeNizSaldoY_WynikWiekszyOdZera()
        {
            // Arrange
            RachunekBankowy rachunekX = new RachunekOsobisty("123456789", "Jan Kowalski");
            rachunekX.Wpłać(500); // Saldo = 500
            RachunekBankowy rachunekY = new RachunekOsobisty("987654321", "Anna Nowak");
            rachunekY.Wpłać(1000); // Saldo = 1000
            var comparer = new RachunekBankowy.SortowanieSaldaComparer();

            // Act
            int result = comparer.Compare(rachunekX, rachunekY);

            // Assert
            Assert.Greater(result, 0);
        }

        [Test]
        [Category("Comparer")]
        public void Compare_RachunekOsobisty_SaldoXRowneSaldoY_WynikRownyZero()
        {
            // Arrange
            RachunekBankowy rachunekX = new RachunekOsobisty("123456789", "Jan Kowalski");
            rachunekX.Wpłać(1000); // Saldo = 1000
            RachunekBankowy rachunekY = new RachunekOsobisty("987654321", "Anna Nowak");
            rachunekY.Wpłać(1000); // Saldo = 1000
            var comparer = new RachunekBankowy.SortowanieSaldaComparer();

            // Act
            int result = comparer.Compare(rachunekX, rachunekY);

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        [Category("WplataEvent")]
        public void Wplata_WykonujeOperacjeFinansowaEvent_ZPoprawnymiArgumentami()
        {
            // Arrange
            RachunekBankowy rachunek = new RachunekOsobisty("123456789", "Jan Kowalski");
            decimal kwota = 1000;
            bool eventCalled = false;

            // Act
            rachunek.OperacjaFinansowa += (sender, e) =>
            {
                eventCalled = true;
                // Sprawdzamy czy event został wywołany z prawidłowymi parametrami
                Assert.AreEqual(sender, rachunek);
                Assert.AreEqual(e.Kwota, kwota);
                Assert.AreEqual(e.Opis, "Wpłata środków");
            };

            rachunek.Wpłać(kwota);

            // Assert
            Assert.IsTrue(eventCalled, "OperacjaFinansowa event nie został wywołany.");
        }

        [Test]
        [Category("WplataEvent")]
        public void Wplata_WykonujeOperacjeFinansowaEvent_ZUjemnąKwotą()
        {
            // Arrange
            RachunekBankowy rachunek = new RachunekOsobisty("123456789", "Jan Kowalski");
            decimal kwota = -1000;
            bool eventCalled = false;

            // Act
            rachunek.OperacjaFinansowa += (sender, e) =>
            {
                eventCalled = true;
                // Sprawdzamy czy event został wywołany z prawidłowymi parametrami
                Assert.AreEqual(rachunek, sender);
                Assert.AreEqual(kwota, e.Kwota);
                Assert.AreEqual("Błąd: Próba wpłaty środków mniejszych bądź równych zero.", e.Opis);
            };

            rachunek.Wpłać(kwota);

            // Assert
            Assert.IsTrue(eventCalled, "OperacjaFinansowa event nie został wywołany.");
        }

        [Test]
        [Category("WplataEvent")]
        public void Wplata_WykonujeOperacjeFinansowaEvent_ZPustaListaInvoke()
        {
            // Arrange
            RachunekBankowy rachunek = new RachunekOsobisty("123456789", "Jan Kowalski");
            decimal kwota = -1000;
            bool eventCalled = false;

            rachunek.Wpłać(kwota);

            // Assert
            Assert.IsFalse(eventCalled);
        }

        [Test]
        [Category("WyplataEvent")]
        public void Wyplata_WykonujeOperacjeFinansowaEvent_ZPoprawnymiArgumentami()
        {
            // Arrange
            RachunekBankowy rachunek = new RachunekOsobisty("123456789", "Jan Kowalski");
            rachunek.Wpłać(1000);
            decimal kwota = 100;
            bool eventCalled = false;

            // Act
            rachunek.OperacjaFinansowa += (sender, e) =>
            {
                eventCalled = true;
                // Sprawdzamy czy event został wywołany z prawidłowymi parametrami
                Assert.AreEqual(rachunek, sender);
                Assert.AreEqual(kwota, e.Kwota);
                Assert.AreEqual("Wypłata środków", e.Opis);
            };


            rachunek.Wypłać(kwota);

            // Assert
            Assert.IsTrue(eventCalled, "OperacjaFinansowa event nie został wywołany.");
        }

        [Test]
        [Category("WyplataEvent")]
        public void Wyplata_WykonujeOperacjeFinansowaEvent_ZaDuzaKwotaWyplaty()
        {
            // Arrange
            RachunekBankowy rachunek = new RachunekOsobisty("123456789", "Jan Kowalski");
            rachunek.Wpłać(1000);
            decimal kwota = 10000;
            bool eventCalled = false;

            // Act
            rachunek.OperacjaFinansowa += (sender, e) =>
            {
                eventCalled = true;
                // Sprawdzamy czy event został wywołany z prawidłowymi parametrami
                Assert.AreEqual(rachunek, sender);
                Assert.AreEqual(kwota, e.Kwota);
                Assert.AreEqual("Błąd: Brak wystarczających środków na rachunku.", e.Opis);
            };


            rachunek.Wypłać(kwota);

            // Assert
            Assert.IsTrue(eventCalled, "OperacjaFinansowa event nie został wywołany.");
        }

        [Test]
        [Category("WyplataEvent")]
        public void Wyplata_WykonujeOperacjeFinansowaEvent_UjemnaKwotaWyplaty()
        {
            // Arrange
            RachunekBankowy rachunek = new RachunekOsobisty("123456789", "Jan Kowalski");
            rachunek.Wpłać(1000);
            decimal kwota = -1;
            bool eventCalled = false;

            // Act
            rachunek.OperacjaFinansowa += (sender, e) =>
            {
                eventCalled = true;
                // Sprawdzamy czy event został wywołany z prawidłowymi parametrami
                Assert.AreEqual(rachunek, sender);
                Assert.AreEqual(kwota, e.Kwota);
                Assert.AreEqual("Błąd: Brak wystarczających środków na rachunku.", e.Opis);
            };


            rachunek.Wypłać(kwota);

            // Assert
            Assert.IsTrue(eventCalled, "OperacjaFinansowa event nie został wywołany.");
        }

        [Test]
        [Category("EqualsHashCode")]
        public void Equals_PorownujeRachunkiBankowe_RetunrnsTrueWhenEqual()
        {
            // Arrange
            RachunekBankowy rachunek1 = new RachunekOsobisty("123456789", "Jan Kowalski");
            RachunekBankowy rachunek2 = new RachunekOsobisty("123456789", "Anna Nowak");

            // Act
            bool result = rachunek1.Equals(rachunek2);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        [Category("EqualsHashCode")]
        public void Equals_PorownujeRachunekBankowyZString_RetunrnsTrueWhenEqual()
        {
            // Arrange
            RachunekBankowy rachunek = new RachunekOsobisty("123456789", "Jan Kowalski");
            string numerRachunku = "123456789";

            // Act
            bool result = rachunek.Equals(numerRachunku);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        [Category("EqualsHashCode")]
        public void GetHashCode_ZwracaPrawidlowyHashCode()
        {
            // Arrange
            RachunekBankowy rachunek1 = new RachunekOsobisty("123456789", "Jan Kowalski");
            RachunekBankowy rachunek2 = new RachunekOsobisty("123456789", "Janina Kowalska");

            // Assert
            Assert.AreEqual(rachunek1.GetHashCode(), rachunek2.GetHashCode());
        }
    }
}
