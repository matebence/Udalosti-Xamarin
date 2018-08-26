using System.Collections.Generic;
using Udalosti.Udaje.Zdroje;
using Udalosti.Udalost.UI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Udalosti.Udalost.Nav
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigaciaUdalosti : ContentPage
    {
        public ListView ListView { get { return listView; } }

        public NavigaciaUdalosti()
        {
            var masterPageItems = new List<Navigacia>();
            masterPageItems.Add(new Navigacia
            {
                Nazov = "Contacts",
                Obrazok = "contacts.png",
                Stranka = typeof(Objavuj)
            });
            masterPageItems.Add(new Navigacia
            {
                Nazov = "TodoList",
                Obrazok = "todo.png",
                Stranka = typeof(PodlaPozicie)
            });
            masterPageItems.Add(new Navigacia
            {
                Nazov = "Reminders",
                Obrazok = "reminders.png",
            });

            listView = new ListView
            {
                ItemsSource = masterPageItems,
                ItemTemplate = new DataTemplate(() =>
                {
                    var grid = new Grid { Padding = new Thickness(5, 10) };
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

                    var image = new Image();
                    image.SetBinding(Image.SourceProperty, "IconSource");
                    var label = new Label { VerticalOptions = LayoutOptions.FillAndExpand };
                    label.SetBinding(Label.TextProperty, "Title");

                    grid.Children.Add(image);
                    grid.Children.Add(label, 1, 0);

                    return new ViewCell { View = grid };
                }),
                SeparatorVisibility = SeparatorVisibility.None
            };

            Icon = "hamburger.png";
            Title = "Personal Organiser";
            Content = new StackLayout
            {
                Children = { listView }
            };
        }
    }
}