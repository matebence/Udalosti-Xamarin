using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
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
        private UdalostiUdaje udalostiUdaje;
        private UvodnaObrazovkaUdaje uvodnaObrazovkaUdaje;

        private ObservableCollection<ObsahUdalosti> udalost;

        private Pouzivatelia pouzivatel;
        private Miesto miesto;

        public Objavuj()
		{
			InitializeComponent();
            init();
        }

        private void init()
        {
            this.udalostiUdaje = new UdalostiUdaje(this, this);
            this.uvodnaObrazovkaUdaje = new UvodnaObrazovkaUdaje();

            this.pouzivatel = uvodnaObrazovkaUdaje.prihlasPouzivatela().Result;
            this.miesto = udalostiUdaje.miestoPrihlasenia();

            this.udalost = new ObservableCollection<ObsahUdalosti>();
        }

        public async Task dataZoServeraAsync(string odpoved, string od, List<ObsahUdalosti> udaje)
        {
            nacitavanie.IsVisible = false;

            switch (od)
            {
                case Nastavenia.UDALOSTI_OBJAVUJ:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        if (udaje != null)
                        {
                            await this.nacitaveniaUdalostiAsync(udaje);
                        }
                        else
                        {
                            ziadneUdalosti.IsVisible = true;
                            zoznamUdalosti.IsVisible = false;
                        }
                    }
                    break;
            }
        }

        public Task odpovedServera(string odpoved, string od, Dictionary<string, string> udaje)
        {
            throw new System.NotImplementedException();
        }

        protected override async void OnAppearing()
        {
            Debug.WriteLine("Metoda nacitajZoznam bola vykonana");

            nacitavanie.IsVisible = true;
            zoznamUdalosti.IsVisible = true;

            await this.udalostiUdaje.zoznamUdalostiAsync(pouzivatel, miesto);
        }

        private async Task ziskajUdalostiAsync(List<ObsahUdalosti> udaje, ListView zoznam, ObservableCollection<ObsahUdalosti> obsah)
        {
            Debug.WriteLine("Metoda ziskajUdalostiAsync bola vykonana");

            foreach (ObsahUdalosti __o in udaje)
            {
                string obrazok = (string)__o.obrazok;
                if (!(await obrazokJeDostupnnyAsync(obrazok)))
                {
                    __o.obrazok = "../../Assets/Images/udalosti_chyba_obrazka.jpg";
                }
                else
                {
                    __o.obrazok = App.udalostiAdresa + __o.obrazok;
                }
                obsah.Add(__o);
            }
            zoznam.ItemsSource = obsah;
        }

        public async Task<bool> obrazokJeDostupnnyAsync(string adresa)
        {
            Debug.WriteLine("Metoda obrazokJeDostupnnyAsync bola vykonana");

            WebRequest request = WebRequest.Create(App.udalostiAdresa + adresa);
            WebResponse odpoved;
            try
            {
                odpoved = await request.GetResponseAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        private async Task nacitaveniaUdalostiAsync(List<ObsahUdalosti> udaje)
        {
            Debug.WriteLine("Metoda nacitaveniaUdalostiAsync bola vykonana");

            await ziskajUdalostiAsync(udaje, zoznamUdalosti, udalost);
        }
    }
}