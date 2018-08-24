using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Udalosti.RychlaUkazkaAplikacie.UI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RychlaUkazkaAplikacie : CarouselPage
	{
		public RychlaUkazkaAplikacie()
		{
			InitializeComponent();
		}

        private void rychlaUkazkaAplikaciePrecitana(object sender, EventArgs e)
        {
        }
    }
}