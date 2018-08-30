using Xamarin.Forms;

namespace Udalosti.Nastroje.Xamarin
{
    public class Tlacidlo : Button
    {
        public static readonly BindableProperty nastavFarbuOkraju =
                BindableProperty.Create(
                    nameof(farbaOkraju),
                    typeof(Color),
                    typeof(Tlacidlo),
                    Color.Default);

        public Color farbaOkraju
        {
            get { return (Color)GetValue(nastavFarbuOkraju); }
            set { SetValue(nastavFarbuOkraju, value); }
        }

        public static readonly BindableProperty nastavUhol = 
             BindableProperty.Create(
                 nameof(uhol),
                 typeof(int),
                 typeof(Tlacidlo),
                 4);

        public int uhol
        {
            get { return (int)GetValue(nastavUhol); }
            set { SetValue(nastavUhol, value); }
        }

        public static readonly BindableProperty nastavSirku =
             BindableProperty.Create(
                 nameof(sirka),
                 typeof(double),
                 typeof(Tlacidlo),
                 4.0);

        public double sirka
        {
            get { return (double)GetValue(nastavSirku); }
            set { SetValue(nastavSirku, value); }
        }

        public static readonly BindableProperty nastavFarbu =
           BindableProperty.Create(
               nameof(farbaOkraju),
               typeof(Color),
               typeof(Tlacidlo),
               Color.Default
              );

        public Color farba
        {
            get { return (Color)GetValue(nastavFarbu); }
            set { SetValue(nastavFarbu, value); }
        }
    }
}