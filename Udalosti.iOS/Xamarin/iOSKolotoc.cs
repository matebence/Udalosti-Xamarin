using System.Linq;
using Udalosti.iOS.Xamarin;
using Udalosti.Nastroje.Xamarin;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Kolotoc), typeof(iOSKolotoc))]
namespace Udalosti.iOS.Xamarin
{
    public class iOSKolotoc : CarouselPageRenderer
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
            {
                View.Subviews.OfType<UIScrollView>().Single().ContentInsetAdjustmentBehavior =
                    UIScrollViewContentInsetAdjustmentBehavior.Never;
            }
        }
    }
}