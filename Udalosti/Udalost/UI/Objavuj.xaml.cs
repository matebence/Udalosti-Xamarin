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
        }

        protected override void OnAppearing()
        {
            Debug.WriteLine("Metoda Objavuj - OnAppearing bola vykonana");

            Title = miesto.stat;

            try
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (SpravcaDat.getUdalosti() == null)
                    {
                        await this.udalostiUdaje.zoznamUdalostiAsync(this.pouzivatel, this.miesto);
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
                            if (SpravcaDat.getUdalosti().Count < 1)
                            {
                                ziadneUdalosti.IsVisible = true;

                                ziadneSpojenie.IsVisible = false;
                                zoznamUdalosti.IsVisible = false;
                                nacitavanie.IsVisible = false;
                            }
                            else
                            {
                                zoznamUdalosti.ItemsSource = SpravcaDat.getUdalosti();
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

        void podrobnostiUdalosti(object sender, SelectedItemChangedEventArgs e)
        {
            Debug.WriteLine("Metoda Objavuj - PodrobnostiUdalosti bola vykonana");

            if (zoznamUdalosti.SelectedItem != null) { 
                zoznamUdalosti.SelectedItem = null;

                Device.BeginInvokeOnMainThread(async () => {
                    await Navigation.PushAsync(new Podrobnosti(), true);
                });
            }
        }

        public void dataZoServera(string odpoved, string od, List<ObsahUdalosti> udaje)
        {
            Debug.WriteLine("Metoda Objavuj - dataZoServera bola vykonana");

            switch (od)
            {
                case Nastavenia.UDALOSTI_OBJAVUJ:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        SpravcaDat.setUdalosti();

                        if (udaje != null)
                        {
                            this.spravcaDat.nacitavanieUdalostiAsync(this.udalostiUdaje, udaje, zoznamUdalosti, SpravcaDat.getUdalosti());
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