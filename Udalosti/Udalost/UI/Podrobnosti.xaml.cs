using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Udalosti.Udalost.UI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Podrobnosti : ContentPage
	{
		public Podrobnosti ()
		{
			InitializeComponent();
		}

        private void zaujem(object sender, EventArgs e)
        {
            Debug.WriteLine("Metoda zaujem bola vykonana");
        }
    }
}