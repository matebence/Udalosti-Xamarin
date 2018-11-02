using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Udalosti.Udaje.Data.Tabulka;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Udaje.Siet.Model;
using Udalosti.Udaje.Zdroje;
using Udalosti.Udalost.Data;
using Udalosti.Udalost.SpracovanieDat;
using Udalosti.Uvod.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Udalosti.Udalost.UI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Zaujmy : ContentPage, KommunikaciaData, KommunikaciaOdpoved, Aktualizator

    {
        private UvodnaObrazovkaUdaje uvodnaObrazovkaUdaje;
        private UdalostiUdaje udalostiUdaje;
        private SpravcaDat spravcaDat;

        private Pouzivatelia pouzivatel;
        private Miesto miesto;

        public Zaujmy ()
		{
            Debug.WriteLine("Metoda Zaujmy bola vykonana");

            InitializeComponent();
            init();
		}

        private void init()
        {
            Debug.WriteLine("Metoda Zaujmy - init bola vykonana");

            nacitavanie.IsVisible = true;

            this.uvodnaObrazovkaUdaje = new UvodnaObrazovkaUdaje();
            this.udalostiUdaje = new UdalostiUdaje(this, this);
            this.spravcaDat = new SpravcaDat();

            AktualizatorObsahu.zaujmy().nastav(this);

            this.pouzivatel = this.uvodnaObrazovkaUdaje.prihlasPouzivatela();
            this.miesto = this.udalostiUdaje.miestoPrihlasenia();
        }

        protected override void OnAppearing()
        {
            Debug.WriteLine("Metoda Zaujmy - OnAppearing bola vykonana");

            try
            {
                this.spravcaDat.nacitajDataUdalosti("Zaujmy", this.udalostiUdaje, this.pouzivatel, this.miesto, SpravcaDat.getZaujmy(), zoznamZaujmov, nacitavanie, ziadneZaujmy, ziadneSpojenie);
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
            Debug.WriteLine("Metoda Zaujmy - podrobnostiUdalosti bola vykonana");

            if (zoznamZaujmov.SelectedItem != null)
            {
                zoznamZaujmov.SelectedItem = null;

                Device.BeginInvokeOnMainThread(async () => {
                    ObsahUdalosti udalost = e.SelectedItem as ObsahUdalosti;
                    await Navigation.PushAsync(new Podrobnosti(udalost), true);
                });
            }
        }

        private void odstranitZaujem(object sender, System.EventArgs e)
        {
            Debug.WriteLine("Metoda odstranitZaujem bola vykonana");

            var prvok = sender as MenuItem;
            if (prvok != null)
            {
                Device.BeginInvokeOnMainThread(async () => {
                    var udalost = prvok.BindingContext as ObsahUdalosti;

                    var odpoved = await DisplayAlert("Odstránenie záujmov", "Naozaj chcete odstránit záujem " + udalost.nazov + "?", "Áno odstrániť", "Nie");
                    if (odpoved)
                    {
                        try
                        {
                            await this.udalostiUdaje.odstranZaujem(this.pouzivatel, udalost.idUdalost);
                        }
                        catch (Exception ex)
                        {
                            Device.BeginInvokeOnMainThread(async () => {
                                await DisplayAlert("Chyba", "Server je momentalne nedostupný!", "Zatvoriť");
                            });

                            Debug.WriteLine("CHYBA: " + ex.Message);
                        }
                        SpravcaDat.getZaujmy().Remove(udalost);
                    }
                });
            }
        }

        public void aktualizujObsahZaujmov()
        {
            Debug.WriteLine("Metoda Zaujmy - aktualizujObsahZaujmov bola vykonana");

            SpravcaDat.getZaujmy().Clear();
            SpravcaDat.setZaujmy(false);

            try
            {
                this.spravcaDat.nacitajDataUdalosti("Zaujmy", this.udalostiUdaje, this.pouzivatel, this.miesto, SpravcaDat.getZaujmy(), zoznamZaujmov, nacitavanie, ziadneZaujmy, ziadneSpojenie);
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

        public void odpovedServera(string odpoved, string od, Dictionary<string, string> udaje)
        {
            Debug.WriteLine("Metoda Zaujmy - odpovedServera bola vykonana");

            switch (od)
            {
                case Nastavenia.ZAUJEM_ODSTRANENIE:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        if (udaje["uspech"] != null)
                        {
                            if (SpravcaDat.getZaujmy().Count == 1)
                            {
                                ziadneZaujmy.IsVisible = true;

                                zoznamZaujmov.IsVisible = false;
                                ziadneSpojenie.IsVisible = false;
                            }
                        }
                        else if (udaje["chyba"] != null)
                        {
                            Device.BeginInvokeOnMainThread(async () => { await Application.Current.MainPage.DisplayAlert("Chyba", udaje["chyba"], "Zatvoriť"); });
                        }
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () => { await Application.Current.MainPage.DisplayAlert("Chyba", odpoved, "Zatvoriť"); });
                    }
                    break;
            }
        }

        public void dataZoServera(string odpoved, string od, List<ObsahUdalosti> udaje)
        {
            Debug.WriteLine("Metoda Zaujmy - dataZoServera bola vykonana");

            switch (od)
            {
                case Nastavenia.ZAUJEM_ZOZNAM:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        SpravcaDat.setZaujmy(true);

                        if (udaje != null)
                        {
                            this.spravcaDat.nacitavanieUdalosti(this.udalostiUdaje, udaje, zoznamZaujmov, SpravcaDat.getZaujmy());
                            zoznamZaujmov.ItemsSource = SpravcaDat.getZaujmy();

                            zoznamZaujmov.IsVisible = true;

                            ziadneZaujmy.IsVisible = false;
                            ziadneSpojenie.IsVisible = false;
                        }
                        else
                        {
                            ziadneZaujmy.IsVisible = true;

                            zoznamZaujmov.IsVisible = false;
                            ziadneSpojenie.IsVisible = false;
                        }
                    }
                    break;
            }

            nacitavanie.IsVisible = false;
        }

        public Task odpovedServeraAsync(string odpoved, string od, Dictionary<string, string> udaje)
        {
            Debug.WriteLine("Metoda Zaujmy - odpovedServeraAsync bola vykonana");

            throw new System.NotImplementedException();
        }


        public Task dataZoServeraAsync(string odpoved, string od, List<ObsahUdalosti> udaje)
        {
            Debug.WriteLine("Metoda Zaujmy - dataZoServeraAsync bola vykonana");

            throw new System.NotImplementedException();
        }
    }
}