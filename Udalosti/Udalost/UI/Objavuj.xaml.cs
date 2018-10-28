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
	public partial class Objavuj : ContentPage, KommunikaciaData, KommunikaciaOdpoved
	{
        private UvodnaObrazovkaUdaje uvodnaObrazovkaUdaje;
        private UdalostiUdaje udalostiUdaje;

        private ObservableCollection<ObsahUdalosti> udalosti;
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

            this.udalostiUdaje = new UdalostiUdaje(this, this);
            this.uvodnaObrazovkaUdaje = new UvodnaObrazovkaUdaje();

            this.pouzivatel = this.uvodnaObrazovkaUdaje.prihlasPouzivatela();
            this.miesto = this.udalostiUdaje.miestoPrihlasenia();

            this.spravcaDat = new SpravcaDat();
        }

        protected override async void OnAppearing()
        {
            Debug.WriteLine("Metoda Objavuj - OnAppearing bola vykonana");

            nacitavanie.IsVisible = true;
            Title = miesto.stat;

            if (this.udalosti == null)
            {
                this.udalosti = new ObservableCollection<ObsahUdalosti>();
                await this.udalostiUdaje.zoznamUdalostiAsync(this.pouzivatel, this.miesto);
            }
        }

        public async Task dataZoServeraAsync(string odpoved, string od, List<ObsahUdalosti> udaje)
        {
            Debug.WriteLine("Metoda Objavuj - dataZoServeraAsync bola vykonana");

            switch (od)
            {
                case Nastavenia.UDALOSTI_OBJAVUJ:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        if (udaje != null)
                        {
                            await this.spravcaDat.nacitavanieUdalostiAsync(this.udalostiUdaje, udaje, zoznamUdalosti, this.udalosti);
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
            Debug.WriteLine("Metoda Objavuj - odpovedServera bola vykonana");

            throw new System.NotImplementedException();
        }
    }
}