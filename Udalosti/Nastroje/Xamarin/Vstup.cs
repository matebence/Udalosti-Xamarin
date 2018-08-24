using Xamarin.Forms;

#pragma warning disable CS0618 // Type or member is obsolete

namespace Udalosti.Nastroje.Xamarin
{
    public class Vstup : Entry
    {
        public static readonly BindableProperty nastavVstupElementu =
            BindableProperty.Create(nameof(vstupElementu),
                typeof(Color), typeof(Vstup), Color.Gray);

        public Color vstupElementu
        {
            get => (Color)GetValue(nastavVstupElementu);
            set => SetValue(nastavVstupElementu, value);
        }

        public static readonly BindableProperty nastavSirku =
        BindableProperty.Create(nameof(sirka), typeof(int),
            typeof(Vstup), Device.OnPlatform<int>(1, 2, 2));

        public int sirka
        {
            get => (int)GetValue(nastavSirku);
            set => SetValue(nastavSirku, value);
        }
        public static readonly BindableProperty nastavUhol =
        BindableProperty.Create(nameof(uhol),
            typeof(double), typeof(Vstup), Device.OnPlatform<double>(6, 7, 7));

        public double uhol
        {
            get => (double)GetValue(nastavUhol);
            set => SetValue(nastavUhol, value);
        }
        public static readonly BindableProperty nastavPomer =
        BindableProperty.Create(nameof(pomer),
            typeof(bool), typeof(Vstup), true);

        public bool pomer
        {
            get => (bool)GetValue(nastavPomer);
            set => SetValue(nastavPomer, value);
        }
    }
}