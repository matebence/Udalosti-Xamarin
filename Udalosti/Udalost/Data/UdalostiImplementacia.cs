using System.Threading.Tasks;
using Udalosti.Udaje.Data.Tabulka;

namespace Udalosti.Udalost.Data
{
    interface UdalostiImplementacia
    {
        Task zoznamUdalosti(Pouzivatelia pouzivatel, Miesto miesto);

        Task zoznamUdalostiPodlaPozicie(Pouzivatelia pouzivatel, Miesto miesto);

        Miesto miestoPrihlasenia();

        void automatickePrihlasenieVypnute(Pouzivatelia pouzivatel);

        Task odhlasenie(Pouzivatelia pouzivatel);

        Task zoznamZaujmov(Pouzivatelia pouzivatel);

        Task zaujem(Pouzivatelia pouzivatel, int idUdalost);

        Task potvrdZaujem(Pouzivatelia pouzivatel, int idUdalost);

        Task odstranZaujem(Pouzivatelia pouzivatel, int idUdalost);
    }
}