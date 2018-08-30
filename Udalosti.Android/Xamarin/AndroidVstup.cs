using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using System;
using Udalosti.Droid.Xamarin;
using Udalosti.Nastroje.Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

#pragma warning disable CS0618 // Type or member is obsolete

[assembly: ExportRenderer(typeof(Vstup), typeof(AndroidVstup))]
namespace Udalosti.Droid.Xamarin
{
    public class AndroidVstup : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                var vstup = (Vstup)Element;
                if (vstup.pomer)
                {

                    var farba = new GradientDrawable();
                    farba.SetShape(ShapeType.Rectangle);
                    farba.SetColor(vstup.BackgroundColor.ToAndroid());


                    farba.SetStroke(vstup.sirka, vstup.vstupElementu.ToAndroid());


                    farba.SetCornerRadius(
                        DPnaPX(this.Context, Convert.ToSingle(vstup.uhol)));


                    Control.SetBackground(farba);
                }

                Control.SetPadding(
                    (int)DPnaPX(this.Context, Convert.ToSingle(12)), Control.PaddingTop,
                    (int)DPnaPX(this.Context, Convert.ToSingle(12)), Control.PaddingBottom);
            }
        }
        public static float DPnaPX(Context context, float hodnota)
        {
            DisplayMetrics velkost = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, hodnota, velkost);
        }
    }
}