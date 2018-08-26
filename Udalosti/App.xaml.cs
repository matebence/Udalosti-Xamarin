using System;
using System.Diagnostics;
using System.IO;
using Udalosti.Autentifikacia.UI;
using Udalosti.Nastroje.Xamarin;
using Udalosti.RychlaUkazkaAplikacie.UI;
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
        public static string udalostiAdresa = "https://bmate18.student.ki.fpv.ukf.sk/udalosti/";
        public static string geoAdresa = "http://ip-api.com/";

        private UvodnaObrazovkaUdaje uvodnaObrazovkaUdaje;

        public App()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            this.uvodnaObrazovkaUdaje = new UvodnaObrazovkaUdaje();
            this.startAplikacie();
        }

        private void startAplikacie()
        {
            Debug.WriteLine("Metoda startAplikacie bola vykonana");

            if (uvodnaObrazovkaUdaje.prvyStart())
            {
                if (uvodnaObrazovkaUdaje.zistiCiPouzivatelskoKontoExistuje())
                {
                    MainPage = new UvodnaObrazovka();
                }
                else
                {
                    MainPage = new NavigationPage(new Prihlasenie(Nastavenia.USPECH));
                }
            }
            else
            {
                if (Platforma.nastavPlatformu(true, true, false))
                {
                    MainPage = new NavigationPage(new UkazkaAplikacie());
                }
                else
                {
                    MainPage = new NavigationPage(new Prihlasenie(Nastavenia.USPECH));
                }
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}