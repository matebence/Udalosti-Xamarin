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

        Task zaujem(Pouzivatelia pouzivatel, int idUdalost);

        Task potvrdZaujem(Pouzivatelia pouzivatel, int idUdalost);

        Task odstranZaujem(Pouzivatelia pouzivatel, int idUdalost);
    }
}