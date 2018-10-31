using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Udalosti.Nastroje;
using Udalosti.Udaje.Data;
using Udalosti.Udaje.Data.Tabulka;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Udaje.Siet;
using Udalosti.Udaje.Siet.Model;
using Udalosti.Udaje.Siet.Model.Akcia;
using Udalosti.Udaje.Siet.Model.Autentifikator;
using Udalosti.Udaje.Siet.Model.Obsah;

namespace Udalosti.Udalost.Data
{
    class UdalostiUdaje : UdalostiImplementacia
    {
        private KommunikaciaOdpoved odpovedeOdServera;
        private KommunikaciaData udajeZoServera;

        private SQLiteDatabaza sqliteDatabaza;
        private Preferencie preferencie;

        public UdalostiUdaje(KommunikaciaOdpoved odpovedeOdServera, KommunikaciaData udajeZoServera)
        {
            Debug.WriteLine("Metoda UdalostiUdaje bola vykonana");

            this.odpovedeOdServera = odpovedeOdServera;
            this.udajeZoServera = udajeZoServera;

            this.sqliteDatabaza = new SQLiteDatabaza();
            this.preferencie = new Preferencie();
        }

        public async Task zoznamUdalostiAsync(Pouzivatelia pouzivatel, Miesto miesto)
        {
            Debug.WriteLine("Metoda zoznamUdalosti bola vykonana");

            var obsah = new Dictionary<string, string>
            {
               { "email", pouzivatel.email },
               { "stat", miesto.stat },
               { "token", pouzivatel.token }
            };

            HttpResponseMessage odpoved = await new Request().postRequestUdalostiServer(obsah, "index.php/udalosti");
            if (odpoved.IsSuccessStatusCode)
            {
                Obsah data = JsonConvert.DeserializeObject<Obsah>(await odpoved.Content.ReadAsStringAsync());
                this.udajeZoServera.dataZoServera(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.UDALOSTI_OBJAVUJ, data.udalosti);
            }
            else
            {
                this.udajeZoServera.dataZoServera("Server je momentalne nedostupný!", Nastavenia.UDALOSTI_OBJAVUJ, null);
            }
        }

        public async Task zoznamUdalostiPodlaPozicieAsync(Pouzivatelia pouzivatel, Miesto miesto)
        {
            Debug.WriteLine("Metoda zoznamUdalostiPodlaPozicie bola vykonana");

            var obsah = new Dictionary<string, string>
            {
               { "email", pouzivatel.email },
               { "stat", miesto.stat },
               { "okres", miesto.okres },
               { "mesto", miesto.pozicia },
               { "token", pouzivatel.token }
            };

            HttpResponseMessage odpoved = await new Request().postRequestUdalostiServer(obsah, "index.php/udalosti/zoznam_podla_pozicie");
            if (odpoved.IsSuccessStatusCode)
            {
                Obsah data = JsonConvert.DeserializeObject<Obsah>(await odpoved.Content.ReadAsStringAsync());
                this.udajeZoServera.dataZoServera(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.UDALOSTI_PODLA_POZICIE, data.udalosti);
            }
            else
            {
                this.udajeZoServera.dataZoServera("Server je momentalne nedostupný!", Nastavenia.UDALOSTI_PODLA_POZICIE, null);
            }
        }

        public Miesto miestoPrihlasenia()
        {
            Debug.WriteLine("Metoda miestoPrihlasenia bola vykonana");

            Miesto miestoPrihlasenia = this.sqliteDatabaza.vratMiestoPrihlasenia();
            if (miestoPrihlasenia != null)
            {
                return miestoPrihlasenia;
            }
            else
            {
                return null;
            }
        }

        public async Task zoznamZaujmovAsync(Pouzivatelia pouzivatel)
        {
            Debug.WriteLine("Metoda zoznamZaujmov bola vykonana");

            var obsah = new Dictionary<string, string>
            {
               { "email", pouzivatel.email },
               { "token", pouzivatel.token }
            };

            HttpResponseMessage odpoved = await new Request().postRequestUdalostiServer(obsah, "index.php/zaujmy/zoznam");
            if (odpoved.IsSuccessStatusCode)
            {
                Obsah data = JsonConvert.DeserializeObject<Obsah>(await odpoved.Content.ReadAsStringAsync());
                await this.udajeZoServera.dataZoServeraAsync(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.ZAUJEM_ZOZNAM, data.udalosti);
            }
            else
            {
                await this.udajeZoServera.dataZoServeraAsync("Server je momentalne nedostupný!", Nastavenia.ZAUJEM_ZOZNAM, null);
            }
        }

        public async Task zaujemAsync(Pouzivatelia pouzivatel, int idUdalost)
        {
            Debug.WriteLine("Metoda zaujem bola vykonana");

            var obsah = new Dictionary<string, string>
            {
               { "email", pouzivatel.email },
               { "token", pouzivatel.token },
               { "idUdalost", idUdalost.ToString() }
            };

            HttpResponseMessage odpoved = await new Request().postRequestUdalostiServer(obsah, "index.php/zaujmy");
            if (odpoved.IsSuccessStatusCode)
            {
                Dictionary<string, string> udaje = new Dictionary<string, string>();
                Akcia data = JsonConvert.DeserializeObject<Akcia>(await odpoved.Content.ReadAsStringAsync());

                if (data.uspech != null)
                {
                    udaje.Add("uspech", data.uspech);
                    await this.odpovedeOdServera.odpovedServeraAsync(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.ZAUJEM, udaje);
                }

                if (data.chyba != null)
                {
                    udaje.Add("chyba", data.chyba);
                    await this.odpovedeOdServera.odpovedServeraAsync(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.ZAUJEM, udaje);
                }
            }
            else
            {
                await this.odpovedeOdServera.odpovedServeraAsync("Server je momentalne nedostupný!", Nastavenia.ZAUJEM, null);
            }
        }

        public async Task potvrdZaujemAsync(Pouzivatelia pouzivatel, int idUdalost)
        {
            Debug.WriteLine("Metoda potvrdZaujemAsync bola vykonana");

            var obsah = new Dictionary<string, string>
            {
               { "email", pouzivatel.email },
               { "token", pouzivatel.token },
               { "idUdalost", idUdalost.ToString() }
            };

            HttpResponseMessage odpoved = await new Request().postRequestUdalostiServer(obsah, "index.php/zaujmy/potvrd");
            if (odpoved.IsSuccessStatusCode)
            {
                Obsah data = JsonConvert.DeserializeObject<Obsah>(await odpoved.Content.ReadAsStringAsync());
                await this.udajeZoServera.dataZoServeraAsync(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.ZAUJEM_POTVRD, data.udalosti);
            }
            else
            {
                await this.udajeZoServera.dataZoServeraAsync("Server je momentalne nedostupný!", Nastavenia.ZAUJEM_POTVRD, null);
            }
        }

        public async Task odstranZaujemAsync(Pouzivatelia pouzivatel, int idUdalost)
        {
            Debug.WriteLine("Metoda odstranZaujem bola vykonana");

            var obsah = new Dictionary<string, string>
            {
               { "email", pouzivatel.email },
               { "token", pouzivatel.token },
               { "idUdalost", idUdalost.ToString() }
            };

            HttpResponseMessage odpoved = await new Request().postRequestUdalostiServer(obsah, "index.php/zaujmy/odstran");
            if (odpoved.IsSuccessStatusCode)
            {
                Dictionary<string, string> udaje = new Dictionary<string, string>();
                Akcia data = JsonConvert.DeserializeObject<Akcia>(await odpoved.Content.ReadAsStringAsync());

                if (data.uspech != null)
                {
                    udaje.Add("uspech", data.uspech);
                    await this.odpovedeOdServera.odpovedServeraAsync(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.ZAUJEM_ODSTRANENIE, udaje);
                }

                if (data.chyba != null)
                {
                    udaje.Add("chyba", data.chyba);
                    await this.odpovedeOdServera.odpovedServeraAsync(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.ZAUJEM_ODSTRANENIE, udaje);
                }
            }
            else
            {
                await this.odpovedeOdServera.odpovedServeraAsync("Server je momentalne nedostupný!", Nastavenia.ZAUJEM_ODSTRANENIE, null);
            }
        }

        public void automatickePrihlasenieVypnute(Pouzivatelia pouzivatel)
        {
            Debug.WriteLine("Metoda automatickePrihlasenieVypnute bola vykonana");

            this.sqliteDatabaza.odstranPouzivatelskeUdaje(pouzivatel);
        }

        public async Task odhlasenieAsync(Pouzivatelia pouzivatel)
        {
            Debug.WriteLine("Metoda odhlasenieAsync bola vykonana");

            var obsah = new Dictionary<string, string>
            {
               { "email", pouzivatel.email }
            };

            HttpResponseMessage odpoved = await new Request().postRequestUdalostiServer(obsah, "index.php/prihlasenie/odhlasit");
            if (odpoved.IsSuccessStatusCode)
            {
                Autentifikator autentifikator = JsonConvert.DeserializeObject<Autentifikator>(await odpoved.Content.ReadAsStringAsync());
                Dictionary<string, string> udaje = new Dictionary<string, string>();

                if (!autentifikator.chyba)
                {
                    this.odpovedeOdServera.odpovedServera(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.AUTENTIFIKACIA_ODHLASENIE, null);
                }
            }
            else
            {
                this.odpovedeOdServera.odpovedServera("Server je momentalne nedostupný!", Nastavenia.AUTENTIFIKACIA_ODHLASENIE, null);
            }
        }
    }
}