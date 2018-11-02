using System.Collections.Generic;
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
        private AutentifikaciaUdaje autentifikaciaUdaje;
        private UvodnaObrazovkaUdaje uvodnaObrazovkaUdaje;
        private UdalostiUdaje udalostiUdaje;

        public TokenUdaje()
        {
            Debug.WriteLine("Metoda TokenUdaje bola vykonana");

            init();
        }

        private void init()
        {
            Debug.WriteLine("Metoda TokenUdaje - init bola vykonana");

            this.autentifikaciaUdaje = new AutentifikaciaUdaje(this);
            this.uvodnaObrazovkaUdaje = new UvodnaObrazovkaUdaje();
            this.udalostiUdaje = new UdalostiUdaje(this, this);
        }

        public async Task novyToken()
        {
            Debug.WriteLine("Metoda novyToken bola vykonana");

            if ((Nastavenia.TOKEN) && (this.uvodnaObrazovkaUdaje.zistiCiPouzivatelskoKontoExistuje()))
            {
                Pouzivatelia pouzivatel = this.uvodnaObrazovkaUdaje.prihlasPouzivatela();
                Miesto miesto = this.udalostiUdaje.miestoPrihlasenia();

                await this.autentifikaciaUdaje.prihlasenie(new Pouzivatelia(pouzivatel.email, pouzivatel.heslo, null));
            }
        }

        public async Task zrusToken()
        {
            Debug.WriteLine("Metoda zrusToken bola vykonana");

            if (this.uvodnaObrazovkaUdaje.zistiCiPouzivatelskoKontoExistuje())
            {
                Pouzivatelia pouzivatel = this.uvodnaObrazovkaUdaje.prihlasPouzivatela();
                await this.udalostiUdaje.odhlasenie(pouzivatel);

                Nastavenia.TOKEN = true;
            }
        }

        public void odpovedServera(string odpoved, string od, Dictionary<string, string> udaje)
        {
            Debug.WriteLine("Metoda TokenUdaje - odpovedServera bola vykonana");

            switch (od)
            {
                case Nastavenia.AUTENTIFIKACIA_PRIHLASENIE:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        Debug.WriteLine("Novy token generovany");

                        Nastavenia.TOKEN = false;

                        Pouzivatelia pouzivatel = this.uvodnaObrazovkaUdaje.prihlasPouzivatela();
                        this.autentifikaciaUdaje.ulozPrihlasovacieUdajeDoDatabazy(new Pouzivatelia(pouzivatel.email, pouzivatel.heslo, udaje["token"]));
                    }
                    else
                    {
                        Debug.WriteLine("CHYBA PRI GENEROVNI NOVEHO TOKENA. Odpoved SERVERA: " + odpoved);
                    }
                    break;

                case Nastavenia.AUTENTIFIKACIA_ODHLASENIE:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        Debug.WriteLine("Token odstranene");
                    }
                    else
                    {
                        Debug.WriteLine("CHYBA PRI ODSTRANENI TOKENA. Odpoved SERVERA: "+ odpoved);
                    }
                    break;
            }
        }

        public Task odpovedServeraAsync(string odpoved, string od, Dictionary<string, string> udaje)
        {
            Debug.WriteLine("Metoda TokenUdaje - odpovedServeraAsync bola vykonana");

            throw new System.NotImplementedException();
        }

        public Task dataZoServeraAsync(string odpoved, string od, List<ObsahUdalosti> udaje)
        {
            Debug.WriteLine("Metoda TokenUdaje - dataZoServeraAsync bola vykonana");

            throw new System.NotImplementedException();
        }

        public void dataZoServera(string odpoved, string od, List<ObsahUdalosti> udaje)
        {
            Debug.WriteLine("Metoda TokenUdaje - dataZoServera bola vykonana");

            throw new System.NotImplementedException();
        }
    }
}