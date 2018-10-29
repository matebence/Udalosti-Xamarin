using System;
using System.Threading.Tasks;
using Udalosti.Udaje.Data.Tabulka;

namespace Udalosti.Autentifikacia.Data
{
    interface AutentifikaciaImplementacia
    {
        Task miestoPrihlaseniaAsync(Pouzivatelia pouzivatel, double zemepisnaSirka, double zemepisnaDlzka, bool aktualizuj);

        Task miestoPrihlaseniaAsync(Pouzivatelia pouzivatel);

        Task prihlasenieAsync(Pouzivatelia pouzivatel);

        Task registraciaAsync(String meno, String email, String heslo, String potvrd);

        Task nastavPrvyStartNaPlatny();

        void ulozPrihlasovacieUdajeDoDatabazy(Pouzivatelia pouzivatel);

        void ucetJeNePristupny(Pouzivatelia pouzivatel);
    }
}