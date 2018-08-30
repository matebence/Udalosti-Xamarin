using System;
using System.Diagnostics;
using Udalosti.Autentifikacia.UI;
using Udalosti.Nastroje;
using Udalosti.Nastroje.Xamarin;
using Udalosti.Udaje.Nastavenia;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Udalosti.RychlaUkazkaAplikacie
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UkazkaAplikacie : Kolotoc
	{
        private Preferencie preferencie;

		public UkazkaAplikacie()
		{
			InitializeComponent();
            init();
		}

        private void init()
        {
            this.preferencie = new Preferencie();
        }

        private void rychlaUkazkaAplikaciePrecitana(object sender, EventArgs e)
        {
            Debug.WriteLine("Metoda rychlaUkazkaAplikaciePrecitana bola vykonana");

            Application.Current.MainPage = new NavigationPage(new Prihlasenie(Nastavenia.USPECH));
        }
    }
}