using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Udalosti.Nastroje;
using Udalosti.Udaje.Data.Tabulka;
using Udalosti.Udaje.Zdroje;
using Udalosti.Udalost.Data;
using Xamarin.Forms;

namespace Udalosti.Udalost
{
    class SpravcaDat
    {
        private static ObservableCollection<ObsahUdalosti> udalosti, udalostiPodlaPozicie, zaujmy = null;

        public void nacitavanieUdalosti(UdalostiUdaje udalostiUdaje, List<ObsahUdalosti> data, ListView zoznam, ObservableCollection<ObsahUdalosti> obsah)
        {
            Debug.WriteLine("Metoda nacitavanieUdalosti bola vykonana");

            foreach (ObsahUdalosti __o in data)
            {
                __o.obrazok = App.udalostiAdresa + __o.obrazok;

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

        public void nacitajDataUdalosti(string karta, UdalostiUdaje udalostiUdaje, Pouzivatelia pouzivatel, Miesto miesto, ObservableCollection<ObsahUdalosti> obsah, ListView zoznam, ActivityIndicator nacitavanie, Image ziadneData, Image ziadneSpojenie)
        {
            Debug.WriteLine("Metoda nacitajDataUdalosti bola vykonana");

            try
            {
                if (obsah == null)
                {
                   nacitaj(karta, udalostiUdaje, pouzivatel, miesto);
                }
                else
                {
                    if (!(Spojenie.existuje()))
                    {
                        spojenieNeExistuje(ziadneSpojenie, ziadneData, zoznam, nacitavanie);
                    }
                    else
                    {
                        if (obsah.Count < 1)
                        {
                            zoznamJePrazdny(ziadneSpojenie, ziadneData, zoznam, nacitavanie);
                        }
                        else
                        {
                            naplZoznam(ziadneSpojenie, ziadneData, zoznam, obsah, nacitavanie);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);

                if (!(Spojenie.existuje()))
                {
                    spojenieNeExistuje(ziadneSpojenie, ziadneData, zoznam, nacitavanie);
                }
            }
        }

        private async void nacitaj(string karta, UdalostiUdaje udalostiUdaje, Pouzivatelia pouzivatel, Miesto miesto)
        {
            Debug.WriteLine("Metoda nacitaj bola vykonana");

            if (karta.Equals("Objavuj"))
            {
                await udalostiUdaje.zoznamUdalosti(pouzivatel, miesto);
            }
            else if (karta.Equals("Lokalizator"))
            {
                await udalostiUdaje.zoznamUdalostiPodlaPozicie(pouzivatel, miesto);
            }
            else if (karta.Equals("Zaujmy"))
            {
                await udalostiUdaje.zoznamZaujmov(pouzivatel);
            }
        }

        private void naplZoznam(Image ziadneSpojenie, Image ziadneData, ListView zoznam, ObservableCollection<ObsahUdalosti> obsah, ActivityIndicator nacitavanie)
        {
            Debug.WriteLine("Metoda naplZoznam bola vykonana");

            zoznam.ItemsSource = obsah;
            zoznam.IsVisible = true;

            ziadneSpojenie.IsVisible = false;
            ziadneData.IsVisible = false;
            nacitavanie.IsVisible = false;
        }

        private void zoznamJePrazdny(Image ziadneSpojenie, Image ziadneData, ListView zoznam, ActivityIndicator nacitavanie)
        {
            Debug.WriteLine("Metoda zoznamJePrazdny bola vykonana");

            ziadneData.IsVisible = true;

            ziadneSpojenie.IsVisible = false;
            zoznam.IsVisible = false;
            nacitavanie.IsVisible = false;
        }

        private void spojenieNeExistuje(Image ziadneSpojenie, Image ziadneData, ListView zoznam, ActivityIndicator nacitavanie)
        {
            Debug.WriteLine("Metoda spojenieNeExistuje bola vykonana");

            ziadneSpojenie.IsVisible = true;

            zoznam.IsVisible = false;
            ziadneData.IsVisible = false;
            nacitavanie.IsVisible = false;
        }

        public static ObservableCollection<ObsahUdalosti> getUdalosti()
        {
            Debug.WriteLine("Metoda getUdalosti bola vykonana");

            return SpravcaDat.udalosti;
        }

        public static void setUdalosti(bool reset)
        {
            Debug.WriteLine("Metoda setUdalosti bola vykonana");

            if (reset)
            {
                SpravcaDat.udalosti = new ObservableCollection<ObsahUdalosti>();
            }
            else
            {
                SpravcaDat.udalosti = null;
            }
        }

        public static ObservableCollection<ObsahUdalosti> getUdalostiPodlaPozicie()
        {
            Debug.WriteLine("Metoda getUdalostiPodlaPozicie bola vykonana");

            return SpravcaDat.udalostiPodlaPozicie;
        }

        public static void setUdalostiPodlaPozicie(bool reset)
        {
            Debug.WriteLine("Metoda setUdalostiPodlaPozicie bola vykonana");

            if (reset)
            {
                SpravcaDat.udalostiPodlaPozicie = new ObservableCollection<ObsahUdalosti>();
            }
            else
            {
                SpravcaDat.udalostiPodlaPozicie = null;
            }
        }

        public static ObservableCollection<ObsahUdalosti> getZaujmy()
        {
            Debug.WriteLine("Metoda getZaujmy bola vykonana");

            return SpravcaDat.zaujmy;
        }

        public static void setZaujmy(bool reset)
        {
            Debug.WriteLine("Metoda setZaujmy bola vykonana");

            if (reset)
            {
                SpravcaDat.zaujmy = new ObservableCollection<ObsahUdalosti>();
            }
            else
            {
                SpravcaDat.zaujmy = null;
            }
        }
    }
}