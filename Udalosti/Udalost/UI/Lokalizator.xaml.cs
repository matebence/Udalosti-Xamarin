using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
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
            this.udalostiUdaje = new UdalostiUdaje(this, this);
            this.spravcaDat = new SpravcaDat();

            this.pouzivatel = this.uvodnaObrazovkaUdaje.prihlasPouzivatela();
            this.miesto = this.udalostiUdaje.miestoPrihlasenia();
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

            try
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if(SpravcaDat.getUdalostiPodlaPozicie() == null)
                    {
                        await this.udalostiUdaje.zoznamUdalostiPodlaPozicieAsync(this.pouzivatel, this.miesto);
                    }
                    else
                    {
                        if (!(Spojenie.existuje()))
                        {
                            ziadneSpojenie.IsVisible = true;

                            zoznamUdalosti.IsVisible = false;
                            ziadneUdalosti.IsVisible = false;
                            nacitavanie.IsVisible = false;
                        }
                        else
                        {
                            if (SpravcaDat.getUdalostiPodlaPozicie().Count < 1)
                            {
                                ziadneUdalosti.IsVisible = true;

                                zoznamUdalosti.IsVisible = false;
                                ziadneSpojenie.IsVisible = false;
                                nacitavanie.IsVisible = false;
                            }
                            else
                            {
                                zoznamUdalosti.ItemsSource = SpravcaDat.getUdalostiPodlaPozicie();
                                zoznamUdalosti.IsVisible = true;

                                ziadneSpojenie.IsVisible = false;
                                ziadneUdalosti.IsVisible = false;
                                nacitavanie.IsVisible = false;
                            }
                        }
                    }
                });
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);

                if (!(Spojenie.existuje()))
                {
                    ziadneSpojenie.IsVisible = true;

                    zoznamUdalosti.IsVisible = false;
                    ziadneUdalosti.IsVisible = false;
                    nacitavanie.IsVisible = false;
                }
            }
        }

        public void dataZoServera(string odpoved, string od, List<ObsahUdalosti> udaje)
        {
            Debug.WriteLine("Metoda Objavuj - dataZoServera bola vykonana");

            switch (od)
            {
                case Nastavenia.UDALOSTI_PODLA_POZICIE:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        SpravcaDat.setUdalostiPodlaPozicie();

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

            throw new NotImplementedException();
        }
    }
}