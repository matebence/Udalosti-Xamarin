using System.Diagnostics;
using Udalosti.Udaje.Data;
using Udalosti.Nastroje;
using System.Threading.Tasks;
using Udalosti.Udaje.Data.Tabulka;

namespace Udalosti.Uvod.Data
{
    class UvodnaObrazovkaUdaje : UvodnaObrazovkaImplementacia
    {
        private SQLiteDatabaza sqliteDatabaza;

        public UvodnaObrazovkaUdaje()
        {
            this.sqliteDatabaza = new SQLiteDatabaza();
        }

        public Pouzivatelia prihlasPouzivatela()
        {
            Debug.WriteLine("Metoda prihlasPouzivatela bola vykonana");

            return sqliteDatabaza.vratAktualnehoPouzivatela();
        }

        public bool prvyStart()
        {
            Debug.WriteLine("Metoda prvyStart bola vykonana");

            Preferencie preferencie = new Preferencie();
            if (preferencie.preferenciaExistuje("prvyStart"))
            {
                if (preferencie.nacitajPreferenciu<bool>("prvyStart"))
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

        public bool zistiCiPouzivatelskoKontoExistuje()
        {
            Debug.WriteLine("Metoda zistiCiPouzivatelskoKontoExistuje bola vykonana");

            return sqliteDatabaza.pouzivatelskeUdaje();
        }
    }
}