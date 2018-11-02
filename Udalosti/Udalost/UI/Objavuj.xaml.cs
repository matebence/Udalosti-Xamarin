using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Udalosti.Udaje.Data.Tabulka;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Udaje.Siet.Model;
using Udalosti.Udaje.Zdroje;
using Udalosti.Udalost.Data;
using Udalosti.Uvod.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Udalosti.Udalost.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Objavuj : ContentPage, KommunikaciaData, KommunikaciaOdpoved
    {
        private UvodnaObrazovkaUdaje uvodnaObrazovkaUdaje;
        private UdalostiUdaje udalostiUdaje;
        private SpravcaDat spravcaDat;

        private Pouzivatelia pouzivatel;
        private Miesto miesto;

        public Objavuj()
        {
            Debug.WriteLine("Metoda Objavuj bola vykonana");

            InitializeComponent();
            init();
        }

        private void init()
        {
            Debug.WriteLine("Metoda Objavuj - init bola vykonana");

            nacitavanie.IsVisible = true;

            this.uvodnaObrazovkaUdaje = new UvodnaObrazovkaUdaje();
            this.udalostiUdaje = new UdalostiUdaje(this, this);
            this.spravcaDat = new SpravcaDat();

            this.pouzivatel = this.uvodnaObrazovkaUdaje.prihlasPouzivatela();
            this.miesto = this.udalostiUdaje.miestoPrihlasenia();

            zoznamUdalosti.RefreshCommand = new Command(() => {
                aktualizujUdalosti();
                zoznamUdalosti.IsRefreshing = false;
            });
        }

        protected override void OnAppearing()
        {
            Debug.WriteLine("Metoda Objavuj - OnAppearing bola vykonana");

            Title = miesto.stat;

            try
            {
                spravcaDat.nacitajDataUdalosti("Objavuj", udalostiUdaje, pouzivatel, miesto, SpravcaDat.getUdalosti(), zoznamUdalosti, nacitavanie, ziadneUdalosti, ziadneSpojenie);
            }
            catch (Exception ex)
            {
                nacitavanie.IsVisible = false;

                Device.BeginInvokeOnMainThread(async () => {
                    await DisplayAlert("Chyba", "Server je momentalne nedostupný!", "Zatvoriť");
                });

                Debug.WriteLine("CHYBA: " + ex.Message);
            }
        }

        void podrobnostiUdalosti(object sender, SelectedItemChangedEventArgs e)
        {
            Debug.WriteLine("Metoda Objavuj - podrobnostiUdalosti bola vykonana");

            if (zoznamUdalosti.SelectedItem != null) { 
                zoznamUdalosti.SelectedItem = null;

                Device.BeginInvokeOnMainThread(async () => {
                    ObsahUdalosti udalost = e.SelectedItem as ObsahUdalosti;
                    await Navigation.PushAsync(new Podrobnosti(udalost), true);
                });
            }
        }

        private void aktualizujUdalosti()
        {
            Debug.WriteLine("Metoda Objavuj - aktualizujUdalosti bola vykonana");

            Device.BeginInvokeOnMainThread(async () =>
            {
                zoznamUdalosti.IsVisible = false;
                nacitavanie.IsVisible = true;

                SpravcaDat.getUdalosti().Clear();
                SpravcaDat.setUdalosti(false);

                try
                {
                    await udalostiUdaje.zoznamUdalosti(pouzivatel, miesto);
                }
                catch (Exception ex)
                {
                    nacitavanie.IsVisible = false;

                    Device.BeginInvokeOnMainThread(async () => {
                        await DisplayAlert("Chyba", "Server je momentalne nedostupný!", "Zatvoriť");
                    });

                    Debug.WriteLine("CHYBA: " + ex.Message);
                }
            });
        }

        public void dataZoServera(string odpoved, string od, List<ObsahUdalosti> udaje)
        {
            Debug.WriteLine("Metoda Objavuj - dataZoServera bola vykonana");

            switch (od)
            {
                case Nastavenia.UDALOSTI_OBJAVUJ:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        SpravcaDat.setUdalosti(true);

                        if (udaje != null)
                        {
                            this.spravcaDat.nacitavanieUdalosti(this.udalostiUdaje, udaje, zoznamUdalosti, SpravcaDat.getUdalosti());
                            zoznamUdalosti.ItemsSource = SpravcaDat.getUdalosti();

                            zoznamUdalosti.IsVisible = true;

                            ziadneUdalosti.IsVisible = false;
                            ziadneSpojenie.IsVisible = false;
                        }
                        else
                        {
                            ziadneUdalosti.IsVisible = true;

                            zoznamUdalosti.IsVisible = false;
                            ziadneSpojenie.IsVisible = false;
                        }
                    }
                    break;
            }

            nacitavanie.IsVisible = false;
        }

        public Task dataZoServeraAsync(string odpoved, string od, List<ObsahUdalosti> udaje)
        {
            Debug.WriteLine("Metoda Objavuj - dataZoServeraAsync bola vykonana");

            throw new NotImplementedException();
        }

        public Task odpovedServeraAsync(string odpoved, string od, Dictionary<string, string> udaje)
        {
            Debug.WriteLine("Metoda Objavuj - odpovedServeraAsync bola vykonana");

            throw new NotImplementedException();
        }

        public void odpovedServera(string odpoved, string od, Dictionary<string, string> udaje)
        {
            Debug.WriteLine("Metoda Objavuj - odpovedServera bola vykonana");

            throw new NotImplementedException();
        }
    }
}