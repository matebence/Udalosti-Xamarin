using System;
using System.Threading.Tasks;
using Udalosti.Udaje.Data.Tabulka;

namespace Udalosti.Udalosti.Data
{
    interface UdalostiImplementacia
    {
        Task zoznamUdalostiAsync(Pouzivatelia pouzivatel, Miesto miesto);

        Task zoznamUdalostiPodlaPozicieAsync(Pouzivatelia pouzivatel, Miesto miesto);

        void automatickePrihlasenieVypnute(Pouzivatelia pouzivatel);

        Task odhlasenieAsync(String email);

        Miesto miestoPrihlasenia();
    }
}