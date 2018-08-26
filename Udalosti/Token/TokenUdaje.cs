using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Udalosti.Autentifikacia.Data;
using Udalosti.Udaje.Data.Tabulka;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Udaje.Siet.Model;
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
            this.autentifikaciaUdaje = new AutentifikaciaUdaje(this);
            this.uvodnaObrazovkaUdaje = new UvodnaObrazovkaUdaje();
            this.udalostiUdaje = new UdalostiUdaje(this, this);
        }

        public async Task novyTokenAsync()
        {
            Debug.WriteLine("Metoda novyToken bola vykonana");

            if ((Nastavenia.TOKEN) && (uvodnaObrazovkaUdaje.zistiCiPouzivatelskoKontoExistuje()))
            {
                Pouzivatelia pouzivatel = uvodnaObrazovkaUdaje.prihlasPouzivatela().Result;
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
                Pouzivatelia pouzivatel = uvodnaObrazovkaUdaje.prihlasPouzivatela().Result;
                await udalostiUdaje.odhlasenieAsync(pouzivatel.email);

                Nastavenia.TOKEN = true;
            }
        }

        public Task odpovedServera(string odpoved, string od, Dictionary<string, string> udaje)
        {
            switch (od)
            {
                case Nastavenia.AUTENTIFIKACIA_PRIHLASENIE:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        Debug.WriteLine("Novy token generovany");
                    }
                    break;
                case Nastavenia.AUTENTIFIKACIA_ODHLASENIE:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        Debug.WriteLine("Token odstranene");
                    }
                    break;
            }
            return null;
        }

        public Task dataZoServeraAsync(string odpoved, string od, List<Udaje.Siet.Model.Udalost.Udalost> udaje)
        {
            throw new System.NotImplementedException();
        }
    }
}