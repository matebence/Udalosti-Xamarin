using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Udalosti.Autentifikacia.UI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Prihlasenie : ContentPage
	{
		public Prihlasenie ()
		{
			InitializeComponent();
		}

        private async void prihlasitSa(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void tlacidloRegistrovat(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registracia());
        }
    }
}