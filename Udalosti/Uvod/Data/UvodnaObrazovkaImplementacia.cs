using Udalosti.Udaje.Data.Tabulka;

namespace Udalosti.Uvod.Data
{
    interface UvodnaObrazovkaImplementacia
    {
        bool prvyStart();

        void vytvorDatabazu();

        bool zistiCiPouzivatelskoKontoExistuje();

        Pouzivatelia prihlasPouzivatela();
    }
}