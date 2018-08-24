using System.Collections.Generic;

namespace Udalosti.UvodnaObrazovka.Data
{
    interface UvodnaObrazovkaImplementacia
    {
        void prvyStart();

        bool zistiCiPouzivatelskoKontoExistuje();

        Dictionary<string, string> prihlasPouzivatela();
    }
}