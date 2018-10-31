using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Udalosti.Udaje.Zdroje;
using Udalosti.Udalost.Data;
using Xamarin.Forms;

namespace Udalosti.Udalost
{
    class SpravcaDat
    {
        private static ObservableCollection<ObsahUdalosti> udalosti, udalostiPodlaPozicie, zaujmy = null;

        public void nacitavanieUdalostiAsync(UdalostiUdaje udalostiUdaje,List<ObsahUdalosti> data, ListView zoznam, ObservableCollection<ObsahUdalosti> obsah)
        {
            Debug.WriteLine("Metoda nacitavanieUdalostiAsync bola vykonana");

            foreach (ObsahUdalosti __o in data)
            {
                __o.obrazok = App.udalostiAdresa + __o.obrazok;
                __o.nacitavanieObrazka = false;

                __o.mesiac = dlzkaSlova(__o.mesiac, 4);
                __o.mesto = __o.mesto + ",";
                __o.den = __o.den + ".";

                obsah.Add(__o);
            }
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