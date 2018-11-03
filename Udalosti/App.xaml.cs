using System;
using System.Diagnostics;
using System.IO;
using Udalosti.Autentifikacia.UI;
using Udalosti.Nastroje.Xamarin;
using Udalosti.RychlaUkazkaAplikacie;
using Udalosti.Token;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Uvod.Data;
using Udalosti.Uvod.UI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Udalosti
{
    public partial class App : Application
    {
        public static string databaza = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "udalosti.db3");

        public static string udalostiAdresa = "http://app-udalosti.8u.cz/";
        public static string ipAdresa = "http://ip-api.com/";
        public static string geoAdresa = "https://eu1.locationiq.com/v1/reverse.php?key=" + Nastavenia.POZICIA_TOKEN;

        private UvodnaObrazovkaUdaje uvodnaObrazovkaUdaje;
        private TokenUdaje tokenUdaje;

        public App()
        {
            Debug.WriteLine("Metoda App bola vykonana");

            InitializeComponent();
            init();
        }

        private void init()
        {
            Debug.WriteLine("Metoda App - init bola vykonana");

            this.uvodnaObrazovkaUdaje = new UvodnaObrazovkaUdaje();
            this.tokenUdaje = new TokenUdaje();
            this.startAplikacie();
        }

        private void startAplikacie()
        {
            Debug.WriteLine("Metoda startAplikacie bola vykonana");

            if (this.uvodnaObrazovkaUdaje.prvyStart())
            {
                if (this.uvodnaObrazovkaUdaje.zistiCiPouzivatelskoKontoExistuje())
                {
                    MainPage = new UvodnaObrazovka();
                }
                else
                {
                    MainPage = (new NavigationPage(new Prihlasenie(Nastavenia.USPECH)) { BarBackgroundColor = Color.FromHex("#275881"), BarTextColor = Color.White });
                }
            }
            else
            {
                this.uvodnaObrazovkaUdaje.vytvorDatabazu();

                if (Platforma.nastavPlatformu(true, true, false))
                {
                    MainPage = (new NavigationPage(new UkazkaAplikacie()) { BarBackgroundColor = Color.FromHex("#275881"), BarTextColor = Color.White });
                }
                else
                {
                    MainPage = (new NavigationPage(new Prihlasenie(Nastavenia.USPECH)) { BarBackgroundColor = Color.FromHex("#275881"), BarTextColor = Color.White });
                }
            }
        }

        protected override async void OnSleep()
        {
            Debug.WriteLine("Metoda OnSleep bola vykonana");

            try
            {
                await this.tokenUdaje.zrusToken();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("CHYBA: " + ex.Message);
            }
        }

        protected override async void OnResume()
        {
            Debug.WriteLine("Metoda OnResume bola vykonana");

            try
            {
                await this.tokenUdaje.novyToken();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("CHYBA: " + ex.Message);
            }
        }

        protected override void OnStart()
        {
            Debug.WriteLine("Metoda OnStart bola vykonana");
        }
    }
}