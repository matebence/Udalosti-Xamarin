using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Udalosti.Nastroje;
using Udalosti.Udaje.Data;
using Udalosti.Udaje.Data.Tabulka;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Udaje.Siet;
using Udalosti.Udaje.Siet.Model;
using Udalosti.Udaje.Siet.Model.Autentifikator;
using Udalosti.Udaje.Siet.Model.Pozicia;
namespace Udalosti.Autentifikacia.Data
{
    class AutentifikaciaUdaje : AutentifikaciaImplementacia
    {
        private SQLiteDatabaza sqliteDataza;
        private Preferencie preferencie;

        private KommunikaciaOdpoved odpovedeOdServera;

        public AutentifikaciaUdaje(KommunikaciaOdpoved odpovedeOdServera)
        {
            Debug.WriteLine("Metoda AutentifikaciaUdaje bola vykonana");

            this.sqliteDataza = new SQLiteDatabaza();
            this.preferencie = new Preferencie();

            this.odpovedeOdServera = odpovedeOdServera;
        }

        public async Task miestoPrihlasenia(Pouzivatelia pouzivatel, double zemepisnaSirka, double zemepisnaDlzka, bool aktualizuj)
        {
            Debug.WriteLine("Metoda miestoPrihlasenia - GEO bola vykonana");

            HttpResponseMessage odpoved = await new Request().getRequestLocationServer(zemepisnaSirka, zemepisnaDlzka);
            string pozicia, okres, kraj, psc, stat, znakStatu;
            pozicia = okres = kraj = psc = stat = znakStatu = "";

            if (odpoved.IsSuccessStatusCode)
            {
                LocationIQ locationIQ = JsonConvert.DeserializeObject<LocationIQ>(await odpoved.Content.ReadAsStringAsync());

                if (locationIQ.address != null)
                {
                    if (locationIQ.address.city_district != null)
                    {
                        pozicia = locationIQ.address.city_district;
                    }
                    if (locationIQ.address.city != null)
                    {
                        okres = locationIQ.address.city;
                    }
                    if (locationIQ.address.state != null)
                    {
                        kraj = locationIQ.address.state;
                    }
                    if (locationIQ.address.postcode != null)
                    {
                        psc = locationIQ.address.postcode;
                    }
                    if (locationIQ.address.country != null)
                    {
                        stat = locationIQ.address.country;
                    }
                    if (locationIQ.address.country_code != null)
                    {
                        znakStatu = locationIQ.address.country_code;
                    }

                    if (this.sqliteDataza.miesto())
                    {
                        this.sqliteDataza.aktualizujMiesto(new Miesto(pozicia, okres, kraj, psc, stat, znakStatu));
                    }
                    else
                    {
                        this.sqliteDataza.noveMiesto(new Miesto(pozicia, okres, kraj, psc, stat, znakStatu));
                    }
                }

                if (aktualizuj)
                {
                    this.odpovedeOdServera.odpovedServera(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.UDALOSTI_AKTUALIZUJ, null);
                }
                else
                {
                    await prihlasenie(pouzivatel);
                }
            }
            else
            {
                this.odpovedeOdServera.odpovedServera("Server je momentalne nedostupný!", Nastavenia.AUTENTIFIKACIA_PRIHLASENIE, null);
            }
        }

        public async Task miestoPrihlasenia(Pouzivatelia pouzivatel)
        {
            Debug.WriteLine("Metoda miestoPrihlasenia - IP bola vykonana");

            HttpResponseMessage odpoved = await new Request().getRequestGeoServer(Nastavenia.SERVER_GEO_IP);
            string stat = "";

            if (odpoved.IsSuccessStatusCode)
            {
                Pozicia pozicia = JsonConvert.DeserializeObject<Pozicia>(await odpoved.Content.ReadAsStringAsync());
                if (pozicia.country != null)
                {
                    if (pozicia.country.Equals("Slovakia") || pozicia.country.Equals("Slovak Republic"))
                    {
                        stat = "Slovensko";
                    }
                }

                if (this.sqliteDataza.miesto())
                {
                    this.sqliteDataza.aktualizujMiesto(new Miesto(null, null, null, null, stat, null));
                }
                else
                {
                    this.sqliteDataza.noveMiesto(new Miesto(null, null, null, null, stat, null));
                }
                await prihlasenie(pouzivatel);
            }
            else
            {
                this.odpovedeOdServera.odpovedServera("Server je momentalne nedostupný!", Nastavenia.AUTENTIFIKACIA_PRIHLASENIE, null);
            }
        }

        public async Task prihlasenie(Pouzivatelia pouzivatel)
        {
            Debug.WriteLine("Metoda prihlasenie bola vykonana");

            var obsah = new Dictionary<string, string>
            {
               { "email", pouzivatel.email },
               { "heslo", pouzivatel.heslo },
               { "pokus_o_prihlasenie", Guid.NewGuid().ToString() }
            };

            HttpResponseMessage odpoved = await new Request().postRequestUdalostiServer(obsah, Nastavenia.SERVER_PRIHLASENIE);
            if (odpoved.IsSuccessStatusCode)
            {
                Autentifikator autentifikator = JsonConvert.DeserializeObject<Autentifikator>(await odpoved.Content.ReadAsStringAsync());
                Dictionary<string, string> udaje = new Dictionary<string, string>();

                if (autentifikator.chyba)
                {
                    udaje.Add("email", pouzivatel.email);
                    if (autentifikator.validacia.email != null)
                    {
                        this.odpovedeOdServera.odpovedServera(autentifikator.validacia.email, Nastavenia.AUTENTIFIKACIA_PRIHLASENIE, udaje);
                    }
                    else if (autentifikator.validacia.heslo != null)
                    {
                        this.odpovedeOdServera.odpovedServera(autentifikator.validacia.heslo, Nastavenia.AUTENTIFIKACIA_PRIHLASENIE, udaje);
                    }
                    else if (autentifikator.validacia.oznam != null)
                    {
                        this.odpovedeOdServera.odpovedServera(autentifikator.validacia.oznam, Nastavenia.AUTENTIFIKACIA_PRIHLASENIE, udaje);
                    }
                }
                else
                {
                    udaje.Add("email", pouzivatel.email);
                    udaje.Add("heslo", pouzivatel.heslo);
                    udaje.Add("token", autentifikator.pouzivatel.token);

                    this.odpovedeOdServera.odpovedServera(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.AUTENTIFIKACIA_PRIHLASENIE, udaje);
                }
            }
            else
            {
                this.odpovedeOdServera.odpovedServera("Server je momentalne nedostupný!", Nastavenia.AUTENTIFIKACIA_PRIHLASENIE, null);
            }
        }

        public async Task registracia(string meno, string email, string heslo, string potvrd)
        {
            Debug.WriteLine("Metoda registracia bola vykonana");

            var obsah = new Dictionary<string, string>
            {
               { "meno", meno },
               { "email", email },
               { "heslo", heslo },
               { "potvrd", potvrd },
               { "nova_registracia", Guid.NewGuid().ToString() }
            };

            HttpResponseMessage odpoved = await new Request().postRequestUdalostiServer(obsah, Nastavenia.SERVER_REGISTRACIA);
            if (odpoved.IsSuccessStatusCode)
            {
                Autentifikator autentifikator = JsonConvert.DeserializeObject<Autentifikator>(await odpoved.Content.ReadAsStringAsync());
                if (autentifikator.chyba)
                {
                    if (autentifikator.validacia.oznam != null)
                    {
                        await this.odpovedeOdServera.odpovedServeraAsync(autentifikator.validacia.oznam, Nastavenia.AUTENTIFIKACIA_REGISTRACIA, null);
                    }
                    else if (autentifikator.validacia.meno != null)
                    {
                        await this.odpovedeOdServera.odpovedServeraAsync(autentifikator.validacia.meno, Nastavenia.AUTENTIFIKACIA_REGISTRACIA, null);
                    }
                    else if (autentifikator.validacia.email != null)
                    {
                        await this.odpovedeOdServera.odpovedServeraAsync(autentifikator.validacia.email, Nastavenia.AUTENTIFIKACIA_REGISTRACIA, null);
                    }
                    else if (autentifikator.validacia.heslo != null)
                    {
                        await this.odpovedeOdServera.odpovedServeraAsync(autentifikator.validacia.heslo, Nastavenia.AUTENTIFIKACIA_REGISTRACIA, null);
                    }
                    else if (autentifikator.validacia.potvrd != null)
                    {
                        await this.odpovedeOdServera.odpovedServeraAsync(autentifikator.validacia.potvrd, Nastavenia.AUTENTIFIKACIA_REGISTRACIA, null);
                    }
                }
                else
                {
                    await this.odpovedeOdServera.odpovedServeraAsync(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.AUTENTIFIKACIA_REGISTRACIA, null);
                }
            }
            else
            {
                await this.odpovedeOdServera.odpovedServeraAsync("Server je momentalne nedostupný!", Nastavenia.AUTENTIFIKACIA_REGISTRACIA, null);
            }
        }

        public void ulozPrihlasovacieUdajeDoDatabazy(Pouzivatelia pouzivatel)
        {
            Debug.WriteLine("Metoda ulozPrihlasovacieUdajeDoDatabazy bola vykonana");

            if (this.sqliteDataza.pouzivatel())
            {
                this.sqliteDataza.aktualizujPouzivatela(pouzivatel);
            }
            else
            {
                this.sqliteDataza.novyPouzivatel(pouzivatel);
            }
        }

        public void ucetJeNePristupny(Pouzivatelia pouzivatelia)
        {
            Debug.WriteLine("Metoda ucetJeNePristupny bola vykonana");

            this.sqliteDataza.odstranPouzivatela(pouzivatelia);
        }

        public async Task nastavPrvyStartNaPlatny()
        {
            Debug.WriteLine("Metoda nastavPrvyStartNaPlatny bola vykonana");

            await this.preferencie.novaPreferencia<bool>(Nastavenia.START, true);
        }
    }
}