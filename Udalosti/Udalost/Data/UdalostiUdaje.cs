using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
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

        public UdalostiUdaje(KommunikaciaOdpoved odpovedeOdServera, KommunikaciaData udajeZoServera)
        {
            Debug.WriteLine("Metoda UdalostiUdaje bola vykonana");

            this.odpovedeOdServera = odpovedeOdServera;
            this.udajeZoServera = udajeZoServera;

            this.sqliteDatabaza = new SQLiteDatabaza();
        }

        public async Task zoznamUdalosti(Pouzivatelia pouzivatel, Miesto miesto)
        {
            Debug.WriteLine("Metoda zoznamUdalosti bola vykonana");

            var obsah = new Dictionary<string, string>
            {
               { "email", pouzivatel.email },
               { "stat", miesto.stat },
               { "token", pouzivatel.token }
            };

            HttpResponseMessage odpoved = await new Request().postRequestUdalostiServer(obsah, Nastavenia.SERVER_ZOZNAM_UDALOSTI);
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

        public async Task zoznamUdalostiPodlaPozicie(Pouzivatelia pouzivatel, Miesto miesto)
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

            HttpResponseMessage odpoved = await new Request().postRequestUdalostiServer(obsah, Nastavenia.SERVER_ZOZNAM_UDALOSTI_PODLA_POZCIE);
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

            Miesto miestoPrihlasenia = this.sqliteDatabaza.vratMiesto();
            if (miestoPrihlasenia != null)
            {
                return miestoPrihlasenia;
            }
            else
            {
                return null;
            }
        }

        public async Task zoznamZaujmov(Pouzivatelia pouzivatel)
        {
            Debug.WriteLine("Metoda zoznamZaujmov bola vykonana");

            var obsah = new Dictionary<string, string>
            {
               { "email", pouzivatel.email },
               { "token", pouzivatel.token }
            };

            HttpResponseMessage odpoved = await new Request().postRequestUdalostiServer(obsah, Nastavenia.SERVER_ZOZNAM_ZAUJMOV);
            if (odpoved.IsSuccessStatusCode)
            {
                Obsah data = JsonConvert.DeserializeObject<Obsah>(await odpoved.Content.ReadAsStringAsync());
                this.udajeZoServera.dataZoServera(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.ZAUJEM_ZOZNAM, data.udalosti);
            }
            else
            {
                this.udajeZoServera.dataZoServera("Server je momentalne nedostupný!", Nastavenia.ZAUJEM_ZOZNAM, null);
            }
        }

        public async Task zaujem(Pouzivatelia pouzivatel, int idUdalost)
        {
            Debug.WriteLine("Metoda zaujem bola vykonana");

            var obsah = new Dictionary<string, string>
            {
               { "email", pouzivatel.email },
               { "token", pouzivatel.token },
               { "idUdalost", idUdalost.ToString() }
            };

            HttpResponseMessage odpoved = await new Request().postRequestUdalostiServer(obsah, Nastavenia.SERVER_ZAUJEM);
            if (odpoved.IsSuccessStatusCode)
            {
                Dictionary<string, string> udaje = new Dictionary<string, string>();
                Akcia data = JsonConvert.DeserializeObject<Akcia>(await odpoved.Content.ReadAsStringAsync());

                if (data.uspech != null)
                {
                    udaje.Add("uspech", data.uspech);
                    this.odpovedeOdServera.odpovedServera(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.ZAUJEM, udaje);
                }

                if (data.chyba != null)
                {
                    udaje.Add("chyba", data.chyba);
                    this.odpovedeOdServera.odpovedServera(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.ZAUJEM, udaje);
                }
            }
            else
            {
                this.odpovedeOdServera.odpovedServera("Server je momentalne nedostupný!", Nastavenia.ZAUJEM, null);
            }
        }

        public async Task potvrdZaujem(Pouzivatelia pouzivatel, int idUdalost)
        {
            Debug.WriteLine("Metoda potvrdZaujem bola vykonana");

            var obsah = new Dictionary<string, string>
            {
               { "email", pouzivatel.email },
               { "token", pouzivatel.token },
               { "idUdalost", idUdalost.ToString() }
            };

            HttpResponseMessage odpoved = await new Request().postRequestUdalostiServer(obsah, Nastavenia.SERVER_POTVRD_ZAUJEM);
            if (odpoved.IsSuccessStatusCode)
            {
                Obsah data = JsonConvert.DeserializeObject<Obsah>(await odpoved.Content.ReadAsStringAsync());
                this.udajeZoServera.dataZoServera(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.ZAUJEM_POTVRD, data.udalosti);
            }
            else
            {
                this.udajeZoServera.dataZoServera("Server je momentalne nedostupný!", Nastavenia.ZAUJEM_POTVRD, null);
            }
        }

        public async Task odstranZaujem(Pouzivatelia pouzivatel, int idUdalost)
        {
            Debug.WriteLine("Metoda odstranZaujem bola vykonana");

            var obsah = new Dictionary<string, string>
            {
               { "email", pouzivatel.email },
               { "token", pouzivatel.token },
               { "idUdalost", idUdalost.ToString() }
            };

            HttpResponseMessage odpoved = await new Request().postRequestUdalostiServer(obsah, Nastavenia.SERVER_ODSTRAN_ZAUJEM);
            if (odpoved.IsSuccessStatusCode)
            {
                Dictionary<string, string> udaje = new Dictionary<string, string>();
                Akcia data = JsonConvert.DeserializeObject<Akcia>(await odpoved.Content.ReadAsStringAsync());

                if (data.uspech != null)
                {
                    udaje.Add("uspech", data.uspech);
                    this.odpovedeOdServera.odpovedServera(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.ZAUJEM_ODSTRANENIE, udaje);
                }

                if (data.chyba != null)
                {
                    udaje.Add("chyba", data.chyba);
                    this.odpovedeOdServera.odpovedServera(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.ZAUJEM_ODSTRANENIE, udaje);
                }
            }
            else
            {
                this.odpovedeOdServera.odpovedServera("Server je momentalne nedostupný!", Nastavenia.ZAUJEM_ODSTRANENIE, null);
            }
        }

        public void automatickePrihlasenieVypnute(Pouzivatelia pouzivatel)
        {
            Debug.WriteLine("Metoda automatickePrihlasenieVypnute bola vykonana");

            this.sqliteDatabaza.odstranPouzivatela(pouzivatel);
        }

        public async Task odhlasenie(Pouzivatelia pouzivatel)
        {
            Debug.WriteLine("Metoda odhlasenie bola vykonana");

            var obsah = new Dictionary<string, string>
            {
               { "email", pouzivatel.email }
            };

            HttpResponseMessage odpoved = await new Request().postRequestUdalostiServer(obsah, Nastavenia.SERVER_ODHLASENIE);
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