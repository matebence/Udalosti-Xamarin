using System;
using Udalosti.Autentifikacia.UI;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Udaje.Zdroje;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Udalosti.Udalost.Nav
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UdalostiNavigacia : MasterDetailPage
    {
        public UdalostiNavigacia()
        {
            InitializeComponent();
            init();

            navigacia.zoznam.ItemSelected += OnItemSelected;
        }

        private void init()
        {
            if (Device.RuntimePlatform == Device.UWP)
            {
                MasterBehavior = MasterBehavior.Popover;
            }
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var prvokNavigacie = e.SelectedItem as MasterPageItem;
            if (prvokNavigacie != null)
            {
                if (prvokNavigacie.Title.Equals("Odlhásiť sa"))
                {
                    Application.Current.MainPage = new NavigationPage(new Prihlasenie(Nastavenia.USPECH));
                }
                else
                {
                    Detail = new NavigationPage((Page)Activator.CreateInstance(prvokNavigacie.TargetType));
                    navigacia.zoznam.SelectedItem = null;
                    IsPresented = false;
                }
            }
        }
    }
}