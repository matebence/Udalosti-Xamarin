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
        private static ObservableCollection<ObsahUdalosti> udalosti, udalostiPodlaPozicie, zaujmy = null;

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
                __o.mesto = __o.mesto + ",";
                __o.den = __o.den + ".";

                obsah.Add(__o);
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

        public static ObservableCollection<ObsahUdalosti> getUdalosti()
        {
            Debug.WriteLine("Metoda getUdalosti bola vykonana");

            return SpravcaDat.udalosti;
        }

        public static void setUdalosti()
        {
            Debug.WriteLine("Metoda setUdalosti bola vykonana");

            SpravcaDat.udalosti = new ObservableCollection<ObsahUdalosti>();
        }

        public static ObservableCollection<ObsahUdalosti> getUdalostiPodlaPozicie()
        {
            Debug.WriteLine("Metoda getUdalostiPodlaPozicie bola vykonana");

            return SpravcaDat.udalostiPodlaPozicie;
        }

        public static void setUdalostiPodlaPozicie()
        {
            Debug.WriteLine("Metoda setUdalostiPodlaPozicie bola vykonana");

            SpravcaDat.udalostiPodlaPozicie = new ObservableCollection<ObsahUdalosti>();
        }

        public static ObservableCollection<ObsahUdalosti> getZaujmy()
        {
            Debug.WriteLine("Metoda getZaujmy bola vykonana");

            return SpravcaDat.zaujmy;
        }

        public static void setZaujmy()
        {
            Debug.WriteLine("Metoda setZaujmy bola vykonana");

            SpravcaDat.zaujmy = new ObservableCollection<ObsahUdalosti>();
        }
    }
}