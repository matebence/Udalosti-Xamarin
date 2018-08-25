using System.Collections.Generic;

namespace Udalosti.Uvod.Data
{
    interface UvodnaObrazovkaImplementacia
    {
        bool prvyStart();

        bool zistiCiPouzivatelskoKontoExistuje();

        Dictionary<string, string> prihlasPouzivatela();
    }
}