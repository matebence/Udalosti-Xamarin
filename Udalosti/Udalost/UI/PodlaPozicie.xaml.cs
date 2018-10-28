using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	public partial class PodlaPozicie : ContentPage, KommunikaciaOdpoved, KommunikaciaData
	{
        private UvodnaObrazovkaUdaje uvodnaObrazovkaUdaje;
        private UdalostiUdaje udalostiUdaje;

        private ObservableCollection<ObsahUdalosti> udalostiPozicia;
        private SpravcaDat spravcaData;

        private Pouzivatelia pouzivatel;
        private Miesto miesto;

        public PodlaPozicie()
        {
            Debug.WriteLine("Metoda PodlaPozicie bola vykonana");

            InitializeComponent();
            init();
        }

        private void init()
        {
            Debug.WriteLine("Metoda PodlaPozicie - init bola vykonana");

            this.udalostiUdaje = new UdalostiUdaje(this, this);
            this.uvodnaObrazovkaUdaje = new UvodnaObrazovkaUdaje();

            this.pouzivatel = this.uvodnaObrazovkaUdaje.prihlasPouzivatela();
            this.miesto = this.udalostiUdaje.miestoPrihlasenia();

            this.spravcaData = new SpravcaDat();
        }

        protected override async void OnAppearing()
        {
            Debug.WriteLine("Metoda PodlaPozicie - init bola vykonana");

            nacitavanie.IsVisible = true;
            Title = miesto.pozicia;

            if (this.udalostiPozicia == null)
            {
                this.udalostiPozicia = new ObservableCollection<ObsahUdalosti>();
                await this.udalostiUdaje.zoznamUdalostiPodlaPozicieAsync(this.pouzivatel, this.miesto);
            }
        }

        public async Task dataZoServeraAsync(string odpoved, string od, List<ObsahUdalosti> udaje)
        {
            Debug.WriteLine("Metoda PodlaPozicie - init bola vykonana");

            switch (od)
            {
                case Nastavenia.UDALOSTI_PODLA_POZICIE:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        if (udaje != null)
                        {
                            await this.spravcaData.nacitavanieUdalostiAsync(this.udalostiUdaje, udaje, zoznamUdalosti,  this.udalostiPozicia);
                            zoznamUdalosti.IsVisible = true;
                        }
                        else
                        {
                            ziadneUdalosti.IsVisible = true;
                            zoznamUdalosti.IsVisible = false;
                        }
                    }
                    break;
            }
            nacitavanie.IsVisible = false;
        }

        public Task odpovedServera(string odpoved, string od, Dictionary<string, string> udaje)
        {
            Debug.WriteLine("Metoda PodlaPozicie - init bola vykonana");

            throw new System.NotImplementedException();
        }
    }
}