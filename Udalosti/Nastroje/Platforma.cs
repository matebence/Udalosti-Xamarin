using System.Diagnostics;
using Xamarin.Forms;

namespace Udalosti.Nastroje.Xamarin
{
    class Platforma
    {
        public static bool nastavPlatformu(bool ios, bool android, bool uwp)
        {
            Debug.WriteLine("Metoda Platforma nastavPlatformu bola vykonana");

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    return ios;
                case Device.Android:
                    return android;
                case Device.UWP:
                    return uwp;
                default:
                    return false;
            }
        }
    }
}