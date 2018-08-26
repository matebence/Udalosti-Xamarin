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
            this.odpovedeOdServera = odpovedeOdServera;
            this.udajeZoServera = udajeZoServera;
            this.sqliteDatabaza = new SQLiteDatabaza();
        }

        public void automatickePrihlasenieVypnute(Pouzivatelia pouzivatel)
        {
            Debug.WriteLine("Metoda automatickePrihlasenieVypnute bola vykonana");

            sqliteDatabaza.odstranPouzivatelskeUdaje(pouzivatel);
        }

        public Miesto miestoPrihlasenia()
        {
            Debug.WriteLine("Metoda miestoPrihlasenia bola vykonana");

            Miesto miestoPrihlasenia = sqliteDatabaza.vratMiestoPrihlasenia().Result;
            if (miestoPrihlasenia != null)
            {
                return miestoPrihlasenia;
            }
            else
            {
                return null;
            }
        }

        public async Task odhlasenieAsync(string email)
        {
            Debug.WriteLine("Metoda odhlasenieAsync bola vykonana");

            var obsah = new Dictionary<string, string>
            {
               { "email", email }
            };

            HttpResponseMessage odpoved = await new Request().novyPostRequestAsync(obsah, "index.php/prihlasenie/odhlasit_sa");
            if (odpoved.IsSuccessStatusCode)
            {
                Autentifikator autentifikator = JsonConvert.DeserializeObject<Autentifikator>(await odpoved.Content.ReadAsStringAsync());
                Dictionary<string, string> udaje = new Dictionary<string, string>();

                if (!autentifikator.chyba)
                {
                    await odpovedeOdServera.odpovedServera(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.AUTENTIFIKACIA_ODHLASENIE, null);
                }
            }
            else
            {
                await odpovedeOdServera.odpovedServera("Server je momentalne nedostupný!", Nastavenia.AUTENTIFIKACIA_ODHLASENIE, null);
            }
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

            HttpResponseMessage odpoved = await new Request().novyPostRequestAsync(obsah, "index.php/udalosti");
            if (odpoved.IsSuccessStatusCode)
            {
                Obsah data = JsonConvert.DeserializeObject<Obsah>(await odpoved.Content.ReadAsStringAsync());
                await udajeZoServera.dataZoServeraAsync(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.UDALOSTI_OBJAVUJ, data.udalosti);
            }
            else
            {
                await udajeZoServera.dataZoServeraAsync("Server je momentalne nedostupný!", Nastavenia.UDALOSTI_OBJAVUJ, null);
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
               { "mesto", miesto.mesto },
               { "token", pouzivatel.token }
            };

            HttpResponseMessage odpoved = await new Request().novyPostRequestAsync(obsah, "index.php/udalosti/udalosti_podla_pozicie");
            if (odpoved.IsSuccessStatusCode)
            {
                Obsah data = JsonConvert.DeserializeObject<Obsah>(await odpoved.Content.ReadAsStringAsync());
                await udajeZoServera.dataZoServeraAsync(Nastavenia.VSETKO_V_PORIADKU, Nastavenia.UDALOSTI_PODLA_POZICIE, data.udalosti);
            }
            else
            {
                await udajeZoServera.dataZoServeraAsync("Server je momentalne nedostupný!", Nastavenia.UDALOSTI_PODLA_POZICIE, null);
            }
        }
    }
}