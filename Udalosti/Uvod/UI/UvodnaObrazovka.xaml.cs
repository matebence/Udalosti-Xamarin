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
            Debug.WriteLine("Metoda UvodnaObrazovka bola vykonana");

            InitializeComponent();
            init();
        }

        private void init()
        {
            Debug.WriteLine("Metoda UvodnaObrazovka - init bola vykonana");

            this.uvodnaObrazovkaUdaje = new UvodnaObrazovkaUdaje();
            this.autentifkaciaUdaje = new AutentifikaciaUdaje(this);
        }

        protected override async void OnAppearing()
        {
            Debug.WriteLine("Metoda UvodnaObrazovka - OnAppearing bola vykonana");

            base.OnAppearing();
            if (Spojenie.existuje())
            {
                Pouzivatelia pouzivatelskeUdaje = this.uvodnaObrazovkaUdaje.prihlasPouzivatela();

                Dictionary<string, double> poloha = await GeoCoder.zistiPolohuAsync(this);
                if (poloha == null)
                {
                    await this.autentifkaciaUdaje.miestoPrihlaseniaAsync(pouzivatelskeUdaje);
                }
                else
                {
                    await this.autentifkaciaUdaje.miestoPrihlaseniaAsync(pouzivatelskeUdaje, poloha["zemepisnaSirka"], poloha["zemepisnaDlzka"], false);
                }
            }
            else
            {
                Application.Current.MainPage = (new NavigationPage(new Prihlasenie(Nastavenia.CHYBA)) { BarBackgroundColor = Color.FromHex("#275881"), BarTextColor = Color.White });
            }
        }

        public void odpovedServera(string odpoved, string od, Dictionary<string, string> udaje)
        {
            Debug.WriteLine("Metoda UvodnaObrazovka - odpovedServera bola vykonana");

            switch (od)
            {
                case Nastavenia.AUTENTIFIKACIA_PRIHLASENIE:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        this.autentifkaciaUdaje.ulozPrihlasovacieUdajeDoDatabazy(new Pouzivatelia(udaje["email"], udaje["heslo"], udaje["token"]));
                        Application.Current.MainPage = new UdalostiNavigacia();
                    }
                    else
                    {
                        Application.Current.MainPage = (new NavigationPage(new Prihlasenie(Nastavenia.MOZNA_CHYBA)) { BarBackgroundColor = Color.FromHex("#275881"), BarTextColor = Color.White });
                    }
                    break;
            }
        }

        public Task odpovedServeraAsync(string odpoved, string od, Dictionary<string, string> udaje)
        {
            Debug.WriteLine("Metoda UvodnaObrazovka - odpovedServeraAsync bola vykonana");

            throw new System.NotImplementedException();
        }
    }
}