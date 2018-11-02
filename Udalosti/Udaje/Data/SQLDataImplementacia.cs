using Udalosti.Udaje.Data.Tabulka;

namespace Udalosti.Udaje.Data
{
    interface SQLDataImplementacia
    {
        void Databaza();

        int novyPouzivatel(Pouzivatelia pouzivatelia);

        int aktualizujPouzivatela(Pouzivatelia pouzivatelia);

        int odstranPouzivatela(Pouzivatelia pouzivatelia);

        bool pouzivatel();

        Pouzivatelia vratPouzivatela();

        int noveMiesto(Miesto miesto);

        int aktualizujMiesto(Miesto miesto);

        int odstranMiesto(Miesto miesto);

        bool miesto();

        Miesto vratMiesto();
    }
}