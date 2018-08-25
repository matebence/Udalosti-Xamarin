using Plugin.Connectivity;
using System.Diagnostics;

namespace Udalosti.Nastroje
{
    class Spojenie
    {
        public static bool existuje()
        {
            Debug.WriteLine("Metoda Spojenie existuje bola vykonana");

            if (!CrossConnectivity.Current.IsConnected)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}