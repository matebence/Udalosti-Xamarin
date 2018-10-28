using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Udalosti.Autentifikacia.Data;
using Udalosti.Nastroje;
using Udalosti.Nastroje.Xamarin;
using Udalosti.Udaje.Data.Tabulka;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Udaje.Siet.Model;
using Udalosti.Udalost.Nav;
using Udalosti.Uvod.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Udalosti.Autentifikacia.UI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Prihlasenie : ContentPage, KommunikaciaOdpoved
    {
        private AutentifikaciaUdaje autentifkaciaUdaje;
        private UvodnaObrazovkaUdaje uvodnaObrazovkaUdaje;

        private Preferencie preferencie;
        private string odpoved;

        public Prihlasenie (String odpoved)
		{
            Debug.WriteLine("Metoda Prihlasenie bola vykonana");

            InitializeComponent();
            init(odpoved);
            ovladanie();
        }

        private void init(string odpoved)
        {
            Debug.WriteLine("Metoda Prihlasenie - init bola vykonana");

            this.odpoved = odpoved;

            this.preferencie = new Preferencie();
            this.autentifkaciaUdaje = new AutentifikaciaUdaje(this);
            this.uvodnaObrazovkaUdaje = new UvodnaObrazovkaUdaje();
        }

        protected override async void OnAppearing()
        {
            Debug.WriteLine("Metoda Prihlasenie - OnAppearing bola vykonana");

            base.OnAppearing();

            spracovanieChyby(this.odpoved);
            await this.preferencie.novaPreferencia<bool>(Nastavenia.START, true);
        }

        private async void prihlasitSa(object sender, EventArgs e)
        {
            Debug.WriteLine("Metoda prihlasitSa bola vykonana");

            if (Spojenie.existuje())
            {
                nacitavanie.IsVisible = true;

                await this.autentifkaciaUdaje.miestoPrihlaseniaAsync(new Pouzivatelia(email.Text, heslo.Text, null));
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () => { await DisplayAlert("Chyba", "Žiadné spojenie!", "Zatvoriť"); });
            }
        }

        private async void tlacidloRegistrovat(object sender, EventArgs e)
        {
            Debug.WriteLine("Metoda tlacidloRegistrovat bola vykonana");

            await Navigation.PushAsync(new Registracia(), true);
        }

        private void ovladanie()
        {
            Debug.WriteLine("Metoda Prihlasenie - ovladanie bola vykonana");

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

        public async Task odpovedServera(string odpoved, string od, Dictionary<string, string> udaje)
        {
            Debug.WriteLine("Metoda Prihlasenie - odpovedServera bola vykonana");

            nacitavanie.IsVisible = false;

            switch (od)
            {
                case Nastavenia.AUTENTIFIKACIA_PRIHLASENIE:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        this.autentifkaciaUdaje.ulozPrihlasovacieUdajeDoDatabazy(new Pouzivatelia(email.Text, heslo.Text, udaje["token"]));
                        Application.Current.MainPage = new UdalostiNavigacia();
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () => { await DisplayAlert("Chyba", odpoved, "Zatvoriť"); });
                    }
                    break;
            }
        }

        private void spracovanieChyby(String odopoved)
        {
            Debug.WriteLine("Metoda spracovanieChyby bola vykonana");

            if (odopoved.Equals(Nastavenia.MOZNA_CHYBA))
            {
                this.autentifkaciaUdaje.ucetJeNePristupny(this.uvodnaObrazovkaUdaje.prihlasPouzivatela());
                Device.BeginInvokeOnMainThread(async () => { await Application.Current.MainPage.DisplayAlert("Chyba", "Prosím prihláste sa!", "Zatvoriť"); });
            }
            else if (odopoved.Equals(Nastavenia.CHYBA))
            {
                Device.BeginInvokeOnMainThread(async () => { await Application.Current.MainPage.DisplayAlert("Chyba", "Nastala chyba, prosím prihláste sa!", "Zatvoriť"); });
            }
        }

    }
}