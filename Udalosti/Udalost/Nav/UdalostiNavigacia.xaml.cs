using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Udalosti.Autentifikacia.UI;
using Udalosti.Udaje.Data.Tabulka;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Udaje.Siet.Model;
using Udalosti.Udaje.Zdroje;
using Udalosti.Udalost.Data;
using Udalosti.Uvod.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Udalosti.Udalost.Nav
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UdalostiNavigacia : MasterDetailPage, KommunikaciaOdpoved, KommunikaciaData
    {
        private UdalostiUdaje udalostiUdaje;
        private UvodnaObrazovkaUdaje uvodnaObrazovkaUdaje;

        public UdalostiNavigacia()
        {
            InitializeComponent();
            init();
            navigaciaUWP();
        }

        private void init()
        {
            this.udalostiUdaje = new UdalostiUdaje(this, this);
            this.uvodnaObrazovkaUdaje = new UvodnaObrazovkaUdaje();
        }

        private void navigaciaUWP()
        {
            Debug.WriteLine("Metoda navigaciaUWP bola vykonana");

            navigacia.zoznam.ItemSelected += OnItemSelected;
            if (Device.RuntimePlatform == Device.UWP)
            {
                MasterBehavior = MasterBehavior.Popover;
            }
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var prvokNavigacie = e.SelectedItem as MasterPageItem;
            if (prvokNavigacie != null)
            {
                if (prvokNavigacie.Title.Equals("Odlhásiť sa"))
                {
                    Pouzivatelia pouzivatel = uvodnaObrazovkaUdaje.prihlasPouzivatela().Result;
                    await udalostiUdaje.odhlasenieAsync(pouzivatel.email);
                }
                else
                {
                    Detail = new NavigationPage((Page)Activator.CreateInstance(prvokNavigacie.TargetType));
                    navigacia.zoznam.SelectedItem = null;
                    IsPresented = false;
                }
            }
        }

        public async Task odpovedServera(string odpoved, string od, Dictionary<string, string> udaje)
        {
            switch (od)
            {
                case Nastavenia.AUTENTIFIKACIA_ODHLASENIE:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        udalostiUdaje.automatickePrihlasenieVypnute(uvodnaObrazovkaUdaje.prihlasPouzivatela().Result);
                        Application.Current.MainPage = new NavigationPage(new Prihlasenie(Nastavenia.USPECH));
                    }
                    break;
            }
        }

        public Task dataZoServeraAsync(string odpoved, string od, List<ObsahUdalosti> udaje)
        {
            throw new NotImplementedException();
        }
    }
}