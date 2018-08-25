using System;
using System.Threading.Tasks;
using Udalosti.Udaje.Data.Tabulka;

namespace Udalosti.Autentifikacia.Data
{
    interface AutentifikaciaImplementacia
    {
        void ucetJeNePristupny(Pouzivatelia pouzivatelia);

        void ulozPrihlasovacieUdajeDoDatabazy(String email, String heslo, String token);

        Task miestoPrihlaseniaAsync(String email, String heslo);

        Task prihlasenieAsync(String email, String heslo, String stat, String okres, String mesto);

        Task registraciaAsync(String meno, String email, String heslo, String potvrd);
    }
}