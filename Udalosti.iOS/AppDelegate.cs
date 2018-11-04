using CoreLocation;
using Foundation;
using UIKit;

namespace Udalosti.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            var coreLocation = new CLLocationManager();
            coreLocation.RequestWhenInUseAuthorization();

            return base.FinishedLaunching(app, options);
        }
    }
}