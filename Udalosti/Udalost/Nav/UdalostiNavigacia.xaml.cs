using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Udalosti.Autentifikacia.UI;
using Udalosti.Nastroje.Xamarin;
using Udalosti.Udaje.Data.Tabulka;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Udaje.Siet.Model;
using Udalosti.Udaje.Zdroje;
using Udalosti.Udalost.Data;
using Udalosti.Udalost.UI;
using Udalosti.Uvod.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Udalosti.Udalost.Nav
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UdalostiNavigacia : MasterDetailPage, KommunikaciaOdpoved, KommunikaciaData
    {
        private UvodnaObrazovkaUdaje uvodnaObrazovkaUdaje;
        private UdalostiUdaje udalostiUdaje;

        private List<MasterPageItem> prvkyNavigacie { get; set; }

        private Pouzivatelia pouzivatel;

        public UdalostiNavigacia()
        {
            Debug.WriteLine("Metoda UdalostiNavigacia bola vykonana");

            InitializeComponent();
            init();
        }

        private void init()
        {
            Debug.WriteLine("Metoda UdalostiNavigacia - init bola vykonana");

            this.udalostiUdaje = new UdalostiUdaje(this, this);
            this.uvodnaObrazovkaUdaje = new UvodnaObrazovkaUdaje();

            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Objavuj)));
            pouzivatel = uvodnaObrazovkaUdaje.prihlasPouzivatela();

            nacitajPolozkyNavigacie();
            nastavPredvolenuPoziciu();
        }

        private void nacitajPolozkyNavigacie()
        {
            Debug.WriteLine("Metoda nacitajPolozkyNavigacie bola vykonana");

            prvkyNavigacie = new List<MasterPageItem>();

            nacitajPolozkyPodlaPlatformy(prvkyNavigacie, "Objavuj", "nav_objavuj.png", typeof(Objavuj));
            nacitajPolozkyPodlaPlatformy(prvkyNavigacie, "Lokalizátor", "nav_podla_pozicie.png", typeof(Lokalizator));
            nacitajPolozkyPodlaPlatformy(prvkyNavigacie, "Záujmy", "nav_zaujmy.png", typeof(Zaujmy));
            nacitajPolozkyPodlaPlatformy(prvkyNavigacie, "Odlhásiť sa", "nav_odhlasit_sa.png", typeof(NullExtension));

            zoznam.ItemsSource = prvkyNavigacie;
            email.Text = pouzivatel.email;

            if (Device.RuntimePlatform == Device.UWP)
            {
                MasterBehavior = MasterBehavior.Popover;
            }
        }

        private void nacitajPolozkyPodlaPlatformy(List<MasterPageItem> prvky, string nazov, string obrazok, Type stranka)
        {
            Debug.WriteLine("Metoda nacitajPolozkyPodlaPlatformy bola vykonana");

            if (Platforma.nastavPlatformu(true, false, false))
            {
                prvky.Add(new MasterPageItem
                {
                    Title = nazov,
                    IconSource = "Images/" + obrazok,
                    TargetType = stranka
                });
            }
            else if (Platforma.nastavPlatformu(false, true, false))
            {
                prvky.Add(new MasterPageItem
                {
                    Title = nazov,
                    IconSource = obrazok,
                    TargetType = stranka
                });
            }
            else if (Platforma.nastavPlatformu(false, false, true))
            {
                prvky.Add(new MasterPageItem
                {
                    Title = nazov,
                    IconSource = "Assets/Images/" + obrazok,
                    TargetType = stranka
                });
            }
        }

        private void nastavPredvolenuPoziciu()
        {
            Debug.WriteLine("Metoda nastavPredvolenuPoziciu bola vykonana");

            zoznam.ItemTapped += (object sender, ItemTappedEventArgs e) => {
                if (e.Item == null) return;
                Task.Delay(1);
                if (sender is ListView lv) lv.SelectedItem = null;
            };
        }


        void zvolenyPrvok(object sender, SelectedItemChangedEventArgs e)
        {
            var prvok = e.SelectedItem as MasterPageItem;
            if (prvok != null)
            {
                if (prvok.Title.Equals("Odlhásiť sa"))
                {
                    Device.BeginInvokeOnMainThread(async () => {
                        await udalostiUdaje.odhlasenieAsync(pouzivatel);
                    });
                }
                else
                {
                    Detail = new NavigationPage((Page)Activator.CreateInstance(prvok.TargetType));
                    zoznam.SelectedItem = null;
                    IsPresented = false;
                }
            }
        }

        public void odpovedServera(string odpoved, string od, Dictionary<string, string> udaje)
        {
            Debug.WriteLine("Metoda UdalostiNavigacia - odpovedServera bola vykonana");

            switch (od)
            {
                case Nastavenia.AUTENTIFIKACIA_ODHLASENIE:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        udalostiUdaje.automatickePrihlasenieVypnute(uvodnaObrazovkaUdaje.prihlasPouzivatela());
                        Application.Current.MainPage = (new NavigationPage(new Prihlasenie(Nastavenia.USPECH)) { BarBackgroundColor = Color.FromHex("#275881"), BarTextColor = Color.White });
                    }
                    break;
            }
        }

        public Task odpovedServeraAsync(string odpoved, string od, Dictionary<string, string> udaje)
        {
            Debug.WriteLine("Metoda UdalostiNavigacia - odpovedServeraAsync bola vykonana");

            throw new NotImplementedException();
        }

        public Task dataZoServeraAsync(string odpoved, string od, List<ObsahUdalosti> udaje)
        {
            Debug.WriteLine("Metoda UdalostiNavigacia - dataZoServeraAsync bola vykonana");

            throw new NotImplementedException();
        }

        public void dataZoServera(string odpoved, string od, List<ObsahUdalosti> udaje)
        {
            Debug.WriteLine("Metoda UdalostiNavigacia - dataZoServera bola vykonana");

            throw new NotImplementedException();
        }
    }
}