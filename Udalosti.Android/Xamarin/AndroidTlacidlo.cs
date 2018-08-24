using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using Udalosti.Droid.Xamarin;
using Udalosti.Nastroje.Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

#pragma warning disable CS0618 // Type or member is obsolete

[assembly: ExportRenderer(typeof(Tlacidlo), typeof(AndroidTlacidlo))]
namespace Udalosti.Droid.Xamarin
{
    public class AndroidTlacidlo : ButtonRenderer
    {
        private GradientDrawable farba;
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
            farba = new GradientDrawable();
            farba.SetShape(ShapeType.Rectangle);
            farba.SetColor(view.nastavFarbuTlacidla.ToAndroid());
            farba.SetCornerRadius(
                DPnaPX(this.Context, Convert.ToSingle(view.uhol)));
            Control.SetBackground(farba);
        }

        public static float DPnaPX(Context context, float hodnota)
        {
            DisplayMetrics velkost = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, hodnota, velkost);
        }
    }
}