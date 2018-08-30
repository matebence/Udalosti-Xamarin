using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Udalosti.Nastroje
{
    class Preferencie
    {
        public async Task novaPreferencia<T>(string kluc, T hodnota)
        {
            Debug.WriteLine("Metoda novaPreferencia bola vykonana");

            Application.Current.Properties[kluc] = hodnota;
            await Application.Current.SavePropertiesAsync();
        }

        public T nacitajPreferenciu<T>(string kluc)
        {
            Debug.WriteLine("Metoda nacitajPreferenciu bola vykonana");

            return (T)Application.Current.Properties[kluc];
        }

        public bool preferenciaExistuje(string kluc)
        {
            Debug.WriteLine("Metoda preferenciaExistuje bola vykonana");

            if (Application.Current.Properties.ContainsKey(kluc))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}