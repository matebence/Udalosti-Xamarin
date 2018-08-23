using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Udalosti.Nastroje.Xamarin
{
    [ContentProperty(nameof(Zdroj))]
    public class XamarinObrazok : IMarkupExtension
    {
        public string Zdroj { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Zdroj == null)
            {
                return null;
            }
            var obrazok = ImageSource.FromResource(Zdroj, typeof(XamarinObrazok).GetTypeInfo().Assembly);
            return obrazok;
        }
    }
}