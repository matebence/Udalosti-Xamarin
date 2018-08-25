using System;
using System.Diagnostics;
using Udalosti.Autentifikacia.UI;
using Udalosti.Nastroje;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Udalosti.RychlaUkazkaAplikacie.UI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UkazkaAplikacie : CarouselPage
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

            Application.Current.MainPage = new NavigationPage(new Prihlasenie());
        }
    }
}