using System;
using System.Diagnostics;
using Udalosti.Autentifikacia.UI;
using Udalosti.Nastroje.Xamarin;
using Udalosti.Udaje.Nastavenia;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Udalosti.RychlaUkazkaAplikacie
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UkazkaAplikacie : Kolotoc
	{
        public UkazkaAplikacie()
        {
            InitializeComponent();
        }

        private void rychlaUkazkaAplikaciePrecitana(object sender, EventArgs e)
        {
            Debug.WriteLine("Metoda rychlaUkazkaAplikaciePrecitana bola vykonana");

            Application.Current.MainPage = (new NavigationPage(new Prihlasenie(Nastavenia.USPECH)) { BarBackgroundColor = Color.FromHex("#275881"), BarTextColor = Color.White });
        }
    }
}