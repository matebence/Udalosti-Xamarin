using System.Collections.Generic;
using System.Diagnostics;
using Udalosti.Udaje.Data;

namespace Udalosti.UvodnaObrazovka.Data
{
    class UvodnaObrazovkaUdaje : UvodnaObrazovkaImplementacia
    {
        private SQLiteDatabaza sqliteDatabaza;

        public UvodnaObrazovkaUdaje()
        {
            this.sqliteDatabaza = new SQLiteDatabaza();
        }

        public Dictionary<string, string> prihlasPouzivatela()
        {
            Debug.WriteLine("Metoda prihlasPouzivatela bola vykonana");

            return sqliteDatabaza.vratAktualnehoPouzivatela();
        }

        public void prvyStart()
        {
            Debug.WriteLine("Metoda prvyStart bola vykonana");

            throw new System.NotImplementedException();
        }

        public bool zistiCiPouzivatelskoKontoExistuje()
        {
            Debug.WriteLine("Metoda zistiCiPouzivatelskoKontoExistuje bola vykonana");

            return sqliteDatabaza.pouzivatelskeUdaje();
        }
    }
}