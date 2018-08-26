﻿using System.Threading.Tasks;
using Udalosti.Udaje.Data.Tabulka;

namespace Udalosti.Uvod.Data
{
    interface UvodnaObrazovkaImplementacia
    {
        bool prvyStart();

        bool zistiCiPouzivatelskoKontoExistuje();

        Task<Pouzivatelia> prihlasPouzivatela();
    }
}