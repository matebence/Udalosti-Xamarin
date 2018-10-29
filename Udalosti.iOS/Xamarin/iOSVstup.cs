using System;
using CoreGraphics;
using Udalosti.iOS.Xamarin;
using UIKit;
using Udalosti.Nastroje.Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Vstup), typeof(iOSVstup))]
namespace Udalosti.iOS.Xamarin
{
    public class iOSVstup : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var vstup = (Vstup)Element;

                Control.LeftView = new UIView(new CGRect(0f, 0f, 9f, 20f));
                Control.LeftViewMode = UITextFieldViewMode.Always;

                Control.KeyboardAppearance = UIKeyboardAppearance.Dark;
                Control.ReturnKeyType = UIReturnKeyType.Done;

                Control.Layer.CornerRadius = Convert.ToSingle(vstup.uhol);
                Control.Layer.BorderColor = vstup.vstupElementu.ToCGColor();
                Control.Layer.BorderWidth = vstup.sirka;

                Control.ClipsToBounds = true;
            }
        }
    }
}