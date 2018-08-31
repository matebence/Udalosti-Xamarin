﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Udalosti.Autentifikacia.Data;
using Udalosti.Udaje.Data.Tabulka;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Udaje.Siet.Model;
using Udalosti.Udaje.Zdroje;
using Udalosti.Udalost.Data;
using Udalosti.Uvod.Data;

namespace Udalosti.Token
{
    class TokenUdaje : TokenImplementacia, KommunikaciaOdpoved, KommunikaciaData
    {
        private Pouzivatelia pouzivatel;

        private AutentifikaciaUdaje autentifikaciaUdaje;
        private UvodnaObrazovkaUdaje uvodnaObrazovkaUdaje;
        private UdalostiUdaje udalostiUdaje;

        public TokenUdaje()
        {
            init();
        }

        private void init()
        {
            this.autentifikaciaUdaje = new AutentifikaciaUdaje(this);
            this.uvodnaObrazovkaUdaje = new UvodnaObrazovkaUdaje();
            this.udalostiUdaje = new UdalostiUdaje(this, this);

            this.pouzivatel = uvodnaObrazovkaUdaje.prihlasPouzivatela();
        }

        public async Task odpovedServera(string odpoved, string od, Dictionary<string, string> udaje)
        {
            switch (od)
            {
                case Nastavenia.AUTENTIFIKACIA_PRIHLASENIE:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        Debug.WriteLine("Novy token generovany");
                        autentifikaciaUdaje.ulozPrihlasovacieUdajeDoDatabazy(pouzivatel.email, pouzivatel.heslo, udaje["token"]);
                    }
                    break;
                case Nastavenia.AUTENTIFIKACIA_ODHLASENIE:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        Debug.WriteLine("Token odstranene");
                    }
                    break;
            }
        }

        public Task dataZoServeraAsync(string odpoved, string od, List<ObsahUdalosti> udaje)
        {
            throw new System.NotImplementedException();
        }

        public async Task novyTokenAsync()
        {
            Debug.WriteLine("Metoda novyToken bola vykonana");

            if ((Nastavenia.TOKEN) && (uvodnaObrazovkaUdaje.zistiCiPouzivatelskoKontoExistuje()))
            {
                Pouzivatelia pouzivatel = uvodnaObrazovkaUdaje.prihlasPouzivatela();
                Miesto miesto = udalostiUdaje.miestoPrihlasenia();

                await autentifikaciaUdaje.prihlasenieAsync(
                    pouzivatel.email,
                    pouzivatel.heslo,
                    miesto.stat,
                    miesto.okres,
                    miesto.mesto
                    );
            }
        }

        public async Task zrusTokenAsync()
        {
            Debug.WriteLine("Metoda zrusToken bola vykonana");

            if (uvodnaObrazovkaUdaje.zistiCiPouzivatelskoKontoExistuje())
            {
                await udalostiUdaje.odhlasenieAsync(pouzivatel.email);

                Nastavenia.TOKEN = true;
            }
        }
    }
}