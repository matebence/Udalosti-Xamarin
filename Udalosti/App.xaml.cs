using System;
using System.Diagnostics;
using System.IO;
using Udalosti.Autentifikacia.UI;
using Udalosti.RychlaUkazkaAplikacie.UI;
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
            uvodnaObrazovkaUdaje = new UvodnaObrazovkaUdaje();
            startAplikacie();
        }

        private void startAplikacie()
        {
            Debug.WriteLine("Metoda startAplikacie bola vykonana");

            if (uvodnaObrazovkaUdaje.prvyStart())
            {
                MainPage = new UvodnaObrazovka();
            }
            else
            {
                if (AndroidiOS())
                {
                    MainPage = new NavigationPage(new UkazkaAplikacie());
                }
                else
                {
                    MainPage = new NavigationPage(new Prihlasenie());
                }
            }
        }

        private bool AndroidiOS()
        {
            Debug.WriteLine("Metoda AndroidiOS bola vykonana");

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    return true;
                case Device.Android:
                    return true;
                case Device.UWP:
                    return false;
                default:
                    return false;
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