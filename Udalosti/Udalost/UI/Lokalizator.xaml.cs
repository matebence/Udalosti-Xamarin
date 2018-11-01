using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Udalosti.Autentifikacia.Data;
using Udalosti.Nastroje;
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
	public partial class Lokalizator : ContentPage, KommunikaciaOdpoved, KommunikaciaData
	{
        private UvodnaObrazovkaUdaje uvodnaObrazovkaUdaje;
        private AutentifikaciaUdaje autentifikaciaUdaje;
        private UdalostiUdaje udalostiUdaje;
        private SpravcaDat spravcaDat;

        private Pouzivatelia pouzivatel;
        private Miesto miesto;

        public Lokalizator()
        {
            Debug.WriteLine("Metoda Lokalizator bola vykonana");

            InitializeComponent();
            init();
        }

        private void init()
        {
            Debug.WriteLine("Metoda Lokalizator - init bola vykonana");

            nacitavanie.IsVisible = true;

            this.uvodnaObrazovkaUdaje = new UvodnaObrazovkaUdaje();
            this.autentifikaciaUdaje = new AutentifikaciaUdaje(this);
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
            Debug.WriteLine("Metoda Lokalizator - OnAppearing bola vykonana");

            if (miesto.pozicia == null)
            {
                Title = "Pozícia neurčená";
            }
            else
            {
                Title = "Okolie "+miesto.pozicia;
            }

            spravcaDat.nacitajDataUdalosti("Lokalizator", udalostiUdaje, pouzivatel, miesto, SpravcaDat.getUdalostiPodlaPozicie(), zoznamUdalosti, nacitavanie, ziadneUdalosti, ziadneSpojenie);
        }

        void podrobnostiUdalosti(object sender, SelectedItemChangedEventArgs e)
        {
            Debug.WriteLine("Metoda Objavuj - PodrobnostiUdalosti bola vykonana");

            if (zoznamUdalosti.SelectedItem != null)
            {
                zoznamUdalosti.SelectedItem = null;

                Device.BeginInvokeOnMainThread(async () => {
                    ObsahUdalosti udalost = e.SelectedItem as ObsahUdalosti;
                    await Navigation.PushAsync(new Podrobnosti(udalost), true);
                });
            }
        }

        private void aktualizujUdalosti()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                zoznamUdalosti.IsVisible = false;

                SpravcaDat.getUdalostiPodlaPozicie().Clear();
                SpravcaDat.setUdalostiPodlaPozicie(false);

                Dictionary<string, double> poloha = await GeoCoder.zistiPolohuAsync(nacitavanie, this);
                if (poloha != null)
                {
                    await this.autentifikaciaUdaje.miestoPrihlaseniaAsync(pouzivatel, poloha["zemepisnaSirka"], poloha["zemepisnaDlzka"], true);
                }

                await udalostiUdaje.zoznamUdalostiPodlaPozicieAsync(pouzivatel, miesto);
            });
        }

        public void dataZoServera(string odpoved, string od, List<ObsahUdalosti> udaje)
        {
            Debug.WriteLine("Metoda Objavuj - dataZoServera bola vykonana");

            switch (od)
            {
                case Nastavenia.UDALOSTI_PODLA_POZICIE:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        SpravcaDat.setUdalostiPodlaPozicie(true);

                        if (udaje != null)
                        {
                            this.spravcaDat.nacitavanieUdalostiAsync(this.udalostiUdaje, udaje, zoznamUdalosti, SpravcaDat.getUdalostiPodlaPozicie());
                            zoznamUdalosti.ItemsSource = SpravcaDat.getUdalostiPodlaPozicie();

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

            switch (od)
            {
                case Nastavenia.UDALOSTI_AKTUALIZUJ:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        this.miesto = this.udalostiUdaje.miestoPrihlasenia();
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () => { await Application.Current.MainPage.DisplayAlert("Chyba", "Pri určení pozície došlo chybe!", "Zatvoriť"); });
                    }
                    break;
            }
        }
    }
}