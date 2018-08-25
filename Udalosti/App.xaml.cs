using System.Diagnostics;
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
                MainPage = new NavigationPage(new UkazkaAplikacie());
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