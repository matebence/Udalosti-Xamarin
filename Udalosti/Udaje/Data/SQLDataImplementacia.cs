using System.Threading.Tasks;
using Udalosti.Udaje.Data.Tabulka;

namespace Udalosti.Udaje.Data
{
    interface SQLDataImplementacia
    {
        void VyvorDatabazu();

        void tabulkaExistuje();

        Task<int> novePouzivatelskeUdaje(Pouzivatelia pouzivatelia);

        Task<int> odstranPouzivatelskeUdaje(Pouzivatelia pouzivatelia);

        bool pouzivatelskeUdaje();

        Task<Pouzivatelia> vratAktualnehoPouzivatela();

        Task<int> noveMiestoPrihlasenia(Miesto miesto);

        Task<int> odstranMiestoPrihlasenia(Miesto miesto);

        bool miestoPrihlasenia();

        Task<Miesto> vratMiestoPrihlasenia();
    }
}