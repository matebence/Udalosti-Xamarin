﻿using System.Collections.Generic;
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

        private Operacie operacie;

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

            this.pouzivatel = uvodnaObrazovkaUdaje.prihlasPouzivatela();
            this.miesto = udalostiUdaje.miestoPrihlasenia();

            this.operacie = new Operacie();
        }

        protected override async void OnAppearing()
        {
            nacitavanie.IsVisible = true;
            zoznamUdalosti.IsVisible = true;

            Title = miesto.stat;

            if (UdalostiUdaje.udalosti.Count < 1)
            {
                await this.udalostiUdaje.zoznamUdalostiAsync(pouzivatel, miesto);
            }
            else
            {
                zoznamUdalosti.ItemsSource = UdalostiUdaje.udalosti;
                nacitavanie.IsVisible = false;
            }
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
                            await operacie.nacitaveniaUdalostiAsync(udaje, zoznamUdalosti, UdalostiUdaje.udalosti);
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
    }
}