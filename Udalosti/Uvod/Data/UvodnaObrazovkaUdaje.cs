using System.Diagnostics;
using Udalosti.Udaje.Data;
using Udalosti.Nastroje;
using Udalosti.Udaje.Data.Tabulka;
using Udalosti.Udaje.Nastavenia;

namespace Udalosti.Uvod.Data
{
    class UvodnaObrazovkaUdaje : UvodnaObrazovkaImplementacia
    {
        private SQLiteDatabaza sqliteDatabaza;
        private Preferencie preferencie;

        public UvodnaObrazovkaUdaje()
        {
            Debug.WriteLine("Metoda UvodnaObrazovkaUdaje bola vykonana");

            this.sqliteDatabaza = new SQLiteDatabaza();
            this.preferencie = new Preferencie();
        }

        public bool prvyStart()
        {
            Debug.WriteLine("Metoda prvyStart bola vykonana");

            if (this.preferencie.preferenciaExistuje("prvyStart"))
            {
                if (this.preferencie.nacitajPreferenciu<bool>(Nastavenia.START))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void vytvorDatabazu()
        {
            Debug.WriteLine("Metoda vytvorDatabazu bola vykonana");

            this.sqliteDatabaza.Databaza();
        }

        public bool zistiCiPouzivatelskoKontoExistuje()
        {
            Debug.WriteLine("Metoda zistiCiPouzivatelskoKontoExistuje bola vykonana");

            return this.sqliteDatabaza.pouzivatel();
        }

        public Pouzivatelia prihlasPouzivatela()
        {
            Debug.WriteLine("Metoda prihlasPouzivatela bola vykonana");

            return this.sqliteDatabaza.vratPouzivatela();
        }
    }
}