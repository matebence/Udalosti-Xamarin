using System;
using System.Collections.Generic;
using System.Diagnostics;
using Udalosti.Nastroje.Xamarin;
using Udalosti.Udaje.Zdroje;
using Udalosti.Udalost.UI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Udalosti.Udalost.Nav
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UdalostiObsah : ContentPage
    {
        public ListView ListView { get { return zoznam; } }
        private List<MasterPageItem> prvkyNavigacie = new List<MasterPageItem>();

        public UdalostiObsah()
        {
            nastavNavigaciu(init());
        }

        private List<MasterPageItem> init()
        {
            nacitajPolozkyPodlaPlatformy(prvkyNavigacie, "Objavujte udalosti", "nav_objavuj.png", typeof(Objavuj));
            nacitajPolozkyPodlaPlatformy(prvkyNavigacie, "Najblizšie udalosti", "nav_podla_pozicie.png", typeof(PodlaPozicie));
            nacitajPolozkyPodlaPlatformy(prvkyNavigacie, "Odlhásiť sa", "nav_odhlasit_sa.png", typeof(NullExtension));

            return prvkyNavigacie;
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

        private void nastavNavigaciu(List<MasterPageItem> prvky)
        {
            Debug.WriteLine("Metoda nastavNavigaciu bola vykonana");

            zoznam = new ListView
            {
                ItemsSource = prvky,
                ItemTemplate = new DataTemplate(() =>
                {
                    var grid = new Grid { Padding = new Thickness(5, 10) };
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

                    var obrazok = new Image();
                    obrazok.SetBinding(Image.SourceProperty, "IconSource");
                    var nazov = new Label { VerticalOptions = LayoutOptions.FillAndExpand };
                    nazov.SetBinding(Label.TextProperty, "Title");

                    grid.Children.Add(obrazok);
                    grid.Children.Add(nazov, 1, 0);

                    return new ViewCell { View = grid };
                }),
                SeparatorVisibility = SeparatorVisibility.None
            };

            if (Platforma.nastavPlatformu(true, false, false))
            {
                Icon = "Images/nav.png";
            }
            else
            {
                Icon = "nav.png";
            }

            Title = "Udalosti";
            Padding = new Thickness(0, 40, 0, 0);
            Content = new StackLayout
            {
                Children = { zoznam }
            };
        }
    }
}