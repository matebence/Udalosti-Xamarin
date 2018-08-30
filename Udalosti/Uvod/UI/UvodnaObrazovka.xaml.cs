using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Udalosti.Autentifikacia.Data;
using Udalosti.Autentifikacia.UI;
using Udalosti.Nastroje;
using Udalosti.Udaje.Data.Tabulka;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Udaje.Siet.Model;
using Udalosti.Udalost.Nav;
using Udalosti.Uvod.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Udalosti.Uvod.UI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UvodnaObrazovka : ContentPage, KommunikaciaOdpoved
    {
        private AutentifikaciaUdaje autentifkaciaUdaje;
        private UvodnaObrazovkaUdaje uvodnaObrazovkaUdaje;

        public UvodnaObrazovka()
		{
			InitializeComponent();
            init();
        }

        private void init()
        {
            this.uvodnaObrazovkaUdaje = new UvodnaObrazovkaUdaje();
            this.autentifkaciaUdaje = new AutentifikaciaUdaje(this);
        }

        public async Task odpovedServera(string odpoved, string od, Dictionary<string, string> udaje)
        {
            switch (od)
            {
                case Nastavenia.AUTENTIFIKACIA_PRIHLASENIE:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        this.autentifkaciaUdaje.ulozPrihlasovacieUdajeDoDatabazy(udaje["email"], udaje["heslo"], udaje["token"]);
                        Application.Current.MainPage = new UdalostiNavigacia();
                    }
                    else
                    {
                        Application.Current.MainPage = (new NavigationPage(new Prihlasenie(Nastavenia.MOZNA_CHYBA)) { BarBackgroundColor = Color.FromHex("#275881"), BarTextColor = Color.White });
                    }
                    break;
            }
        }

        protected override async void OnAppearing()
        {
            Debug.WriteLine("Metoda automatickePrihlasenie bola vykonana");

            base.OnAppearing();
            if (Spojenie.existuje())
            {
                Pouzivatelia pouzivatelskeUdaje = uvodnaObrazovkaUdaje.prihlasPouzivatela();
                await autentifkaciaUdaje.miestoPrihlaseniaAsync(pouzivatelskeUdaje.email, pouzivatelskeUdaje.heslo);
            }
            else
            {
                Application.Current.MainPage = (new NavigationPage(new Prihlasenie(Nastavenia.CHYBA)) { BarBackgroundColor = Color.FromHex("#275881"), BarTextColor = Color.White });
            }
        }
    }
}