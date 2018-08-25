using System;
using System.Diagnostics;
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
            Debug.WriteLine("Metoda registrovatSa bola vykonana");
        }
    }
}