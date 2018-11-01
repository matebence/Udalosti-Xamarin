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
            Debug.WriteLine("Metoda Registracia bola vykonana");

            InitializeComponent();
            init();
            ovladanie();
        }

        private void init()
        {
            Debug.WriteLine("Metoda Registracia - init bola vykonana");

            this.autentifkaciaUdaje = new AutentifikaciaUdaje(this);
        }

        private void ovladanie()
        {
            Debug.WriteLine("Metoda Registracia - ovladanie bola vykonana");

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

            meno.IsEnabled = false;
            email.IsEnabled = false;
            heslo.IsEnabled = false;
            potvrd.IsEnabled = false;

            if (Spojenie.existuje())
            {
                nacitavanie.IsVisible = true;

                await this.autentifkaciaUdaje.registraciaAsync(meno.Text, email.Text, heslo.Text, potvrd.Text);
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () => { await DisplayAlert("Chyba", "Žiadné spojenie!", "Zatvoriť"); });

                meno.IsEnabled = true;
                email.IsEnabled = true;
                heslo.IsEnabled = true;
                potvrd.IsEnabled = true;
            }
        }

        public async Task odpovedServeraAsync(string odpoved, string od, Dictionary<string, string> udaje)
        {
            Debug.WriteLine("Metoda Registracia - odpovedServeraAsync bola vykonana");

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

                    meno.IsEnabled = true;
                    email.IsEnabled = true;
                    heslo.IsEnabled = true;
                    potvrd.IsEnabled = true;

                    break;
            }
        }

        public void odpovedServera(string odpoved, string od, Dictionary<string, string> udaje)
        {
            Debug.WriteLine("Metoda Registracia - odpovedServera bola vykonana");

            throw new NotImplementedException();
        }
    }
}