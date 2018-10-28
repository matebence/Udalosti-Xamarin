using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Udalosti.Nastroje.Xamarin;
using Udalosti.Udaje.Zdroje;
using Udalosti.Udalost.Data;
using Xamarin.Forms;

namespace Udalosti.Udalost
{
    class SpravcaDat
    {
        public async Task nacitavanieUdalostiAsync(UdalostiUdaje udalostiUdaje,List<ObsahUdalosti> data, ListView zoznam, ObservableCollection<ObsahUdalosti> obsah)
        {
            Debug.WriteLine("Metoda nacitavanieUdalostiAsync bola vykonana");

            foreach (ObsahUdalosti __o in data)
            {
                string obrazok = (string)__o.obrazok;
                if (!(await udalostiUdaje.obrazokJeDostupnnyAsync(obrazok, false)))
                {
                    __o.obrazok = chybaObrazka("udalosti_chyba_obrazka.jpg");
                }
                else
                {
                    __o.obrazok = App.udalostiAdresa + __o.obrazok;
                }

                __o.mesiac = dlzkaSlova(__o.mesiac, 4);
                __o.mesto = __o.mesto + ", ";
                __o.den = __o.den + ".";

                obsah.Add(__o);
            }
            zoznam.ItemsSource = obsah;
        }

        private string dlzkaSlova(string slovo, int dlzka)
        {
            Debug.WriteLine("Metoda dlzkaSlova bola vykonana");

            if (slovo.Length > dlzka)
            {
                return slovo.Substring(0, 3) + ".";
            }
            else
            {
                return slovo;
            }
        }

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
    }
}