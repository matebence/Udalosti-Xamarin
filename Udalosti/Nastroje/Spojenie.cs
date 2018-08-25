using Plugin.Connectivity;

namespace Udalosti.Nastroje
{
    class Spojenie
    {
        public static bool existuje()
        {
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