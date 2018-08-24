using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Udalosti.Autentifikacia.UI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Registracia : ContentPage
	{
		public Registracia ()
		{
			InitializeComponent();
		}

        private async void registrovatSa(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}