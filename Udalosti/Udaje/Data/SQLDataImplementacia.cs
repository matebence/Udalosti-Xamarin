using System.Threading.Tasks;
using Udalosti.Udaje.Data.Tabulka;

namespace Udalosti.Udaje.Data
{
    interface SQLDataImplementacia
    {
        void VyvorDatabazu();

        void tabulkaExistuje();

        int novePouzivatelskeUdaje(Pouzivatelia pouzivatelia);

        int aktualizujPouzivatelskeUdaje(Pouzivatelia pouzivatelia);

        int odstranPouzivatelskeUdaje(Pouzivatelia pouzivatelia);

        bool pouzivatelskeUdaje();

        Pouzivatelia vratAktualnehoPouzivatela();

        int noveMiestoPrihlasenia(Miesto miesto);

        int aktualizujMiestoPrihlasenia(Miesto miesto);

        int odstranMiestoPrihlasenia(Miesto miesto);

        bool miestoPrihlasenia();

        Miesto vratMiestoPrihlasenia();
    }
}