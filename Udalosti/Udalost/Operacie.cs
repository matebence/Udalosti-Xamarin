using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Udalosti.Nastroje.Xamarin;
using Udalosti.Udaje.Zdroje;
using Xamarin.Forms;

namespace Udalosti.Udalost
{
    class Operacie
    {
        private string chybaObrazka(string obrazok)
        {
            Debug.WriteLine("Metoda chybaObrazka bola vykonana");

            string nazovObrazka = "";
            if (Platforma.nastavPlatformu(true, false, false))
            {
                nazovObrazka = "Images/" + obrazok;
            }
            else if (Platforma.nastavPlatformu(false, true, false))
            {
                nazovObrazka = obrazok;
            }
            else if (Platforma.nastavPlatformu(false, false, true))
            {
                nazovObrazka = "Assets/Images/" + obrazok;
            }

            return nazovObrazka;
        }

        private async Task ziskajUdalostiAsync(List<ObsahUdalosti> udaje, ListView zoznam, ObservableCollection<ObsahUdalosti> obsah)
        {
            Debug.WriteLine("Metoda ziskajUdalostiAsync bola vykonana");

            foreach (ObsahUdalosti __o in udaje)
            {
                string obrazok = (string)__o.obrazok;
                if (!(await obrazokJeDostupnnyAsync(obrazok)))
                {
                    __o.obrazok = chybaObrazka("udalosti_chyba_obrazka.jpg");
                }
                else
                {
                    __o.obrazok = App.udalostiAdresa + __o.obrazok;
                }
                obsah.Add(__o);
            }
            zoznam.ItemsSource = obsah;
        }

        private async Task<bool> obrazokJeDostupnnyAsync(string adresa)
        {
            Debug.WriteLine("Metoda obrazokJeDostupnnyAsync bola vykonana");

            WebRequest request = WebRequest.Create(App.udalostiAdresa + adresa);
            WebResponse odpoved;
            try
            {
                odpoved = await request.GetResponseAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task nacitaveniaUdalostiAsync(List<ObsahUdalosti> udaje, ListView zoznamUdalosti, ObservableCollection<ObsahUdalosti> udalost)
        {
            Debug.WriteLine("Metoda nacitaveniaUdalostiAsync bola vykonana");

            await ziskajUdalostiAsync(udaje, zoznamUdalosti, udalost);
        }
    }
}