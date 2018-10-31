using System.Threading.Tasks;
using Udalosti.Udaje.Data.Tabulka;

namespace Udalosti.Udalost.Data
{
    interface UdalostiImplementacia
    {
        Task zoznamUdalostiAsync(Pouzivatelia pouzivatel, Miesto miesto);

        Task zoznamUdalostiPodlaPozicieAsync(Pouzivatelia pouzivatel, Miesto miesto);

        Miesto miestoPrihlasenia();

        void automatickePrihlasenieVypnute(Pouzivatelia pouzivatel);

        Task odhlasenieAsync(Pouzivatelia pouzivatel);

        Task zoznamZaujmovAsync(Pouzivatelia pouzivatel);

        Task zaujemAsync(Pouzivatelia pouzivatel, int idUdalost);

        Task potvrdZaujemAsync(Pouzivatelia pouzivatel, int idUdalost);

        Task odstranZaujemAsync(Pouzivatelia pouzivatel, int idUdalost);
    }
}