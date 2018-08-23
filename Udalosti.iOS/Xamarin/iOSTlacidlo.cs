using System.ComponentModel;
using Udalosti.iOS.Xamarin;
using Udalosti.Nastroje.Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Tlacidlo), typeof(iOSTlacidlo))]
namespace Udalosti.iOS.Xamarin
{
    public class iOSTlacidlo : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            var tlacidlo = (Tlacidlo)Element;
            if (tlacidlo == null) return;
            Paint(tlacidlo);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == Tlacidlo.nastavUhol.PropertyName ||
               e.PropertyName == Tlacidlo.nastavSirku.PropertyName)
            {
                if (Element != null)
                {
                    Paint((Tlacidlo)Element);
                }
            }
        }
        private void Paint(Tlacidlo view)
        {
            this.Layer.CornerRadius = (float)view.uhol;
            this.Layer.BackgroundColor = view.nastavFarbuTlacidla.ToCGColor();
            this.Layer.BorderWidth = (int)view.sirka;
        }
    }
}