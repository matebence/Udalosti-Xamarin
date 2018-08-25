using System;
using System.Diagnostics;
using Udalosti.Nastroje;
using Udalosti.Udaje.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Udalosti.Autentifikacia.UI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Prihlasenie : ContentPage
	{
        private Preferencie preferencie;
        private SQLiteDatabaza sqliteDatabaza;

		public Prihlasenie ()
		{
			InitializeComponent();
            init();
        }

        private void init()
        {
            this.preferencie = new Preferencie();
            this.sqliteDatabaza = new SQLiteDatabaza();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            sqliteDatabaza.VyvorDatabazu();
            await preferencie.novaPreferencia<bool>("prvyStart", true);
        }

        private async void prihlasitSa(object sender, EventArgs e)
        {
            Debug.WriteLine("Metoda prihlasitSa bola vykonana");

        }

        private async void tlacidloRegistrovat(object sender, EventArgs e)
        {
            Debug.WriteLine("Metoda tlacidloRegistrovat bola vykonana");

            await Navigation.PushAsync(new Registracia(), true);
        }
    }
}