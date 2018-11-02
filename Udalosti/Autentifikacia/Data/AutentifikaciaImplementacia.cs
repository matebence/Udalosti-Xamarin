using System;
using System.Threading.Tasks;
using Udalosti.Udaje.Data.Tabulka;

namespace Udalosti.Autentifikacia.Data
{
    interface AutentifikaciaImplementacia
    {
        Task miestoPrihlasenia(Pouzivatelia pouzivatel, double zemepisnaSirka, double zemepisnaDlzka, bool aktualizuj);

        Task miestoPrihlasenia(Pouzivatelia pouzivatel);

        Task prihlasenie(Pouzivatelia pouzivatel);

        Task registracia(string meno, string email, string heslo, string potvrd);

        Task nastavPrvyStartNaPlatny();

        void ulozPrihlasovacieUdajeDoDatabazy(Pouzivatelia pouzivatel);

        void ucetJeNePristupny(Pouzivatelia pouzivatel);
    }
}