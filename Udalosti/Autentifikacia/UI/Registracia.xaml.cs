using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Udalosti.Autentifikacia.Data;
using Udalosti.Nastroje;
using Udalosti.Nastroje.Xamarin;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Udaje.Siet.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Udalosti.Autentifikacia.UI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Registracia : ContentPage, KommunikaciaOdpoved
    {
        private AutentifikaciaUdaje autentifkaciaUdaje;

        public Registracia ()
		{
			InitializeComponent();
            init();
            ovladanie();
        }

        private void init()
        {
            autentifkaciaUdaje = new AutentifikaciaUdaje(this);
        }

        public async Task odpovedServera(string odpoved, string od, Dictionary<string, string> udaje)
        {
            nacitavanie.IsVisible = false;
            switch (od)
            {
                case Nastavenia.AUTENTIFIKACIA_REGISTRACIA:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        Device.BeginInvokeOnMainThread(async () => { await DisplayAlert("Úspech", "Registrácia prebehla úspesne! Možete sa prihlásiť.", "Ďalej"); });
                        await Navigation.PopAsync(true);
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () => { await DisplayAlert("Chyba", odpoved, "Zatvoriť"); });
                    }
                    break;
            }
        }

        private void ovladanie()
        {
            Debug.WriteLine("Metoda ovladanie-Registracia bola vykonana");

            if (Platforma.nastavPlatformu(true, false, false))
            {
                NavigationPage.SetHasNavigationBar(this, true);
            }
            else if (Platforma.nastavPlatformu(false, true, false))
            {
                NavigationPage.SetHasNavigationBar(this, false);
            }
            else if (Platforma.nastavPlatformu(false, false, true))
            {
                NavigationPage.SetHasNavigationBar(this, false);
            }
        }

        private async void registrovatSa(object sender, EventArgs e)
        {
            Debug.WriteLine("Metoda registrovatSa bola vykonana");

            if (Spojenie.existuje())
            {
                nacitavanie.IsVisible = true;

                await autentifkaciaUdaje.registraciaAsync(meno.Text, email.Text, heslo.Text, potvrd.Text);
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () => { await DisplayAlert("Chyba", "Žiadné spojenie!", "Zatvoriť"); });
            }
        }
    }
}