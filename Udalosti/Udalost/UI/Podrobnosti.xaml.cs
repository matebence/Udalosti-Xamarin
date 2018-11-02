using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Udalosti.Nastroje;
using Udalosti.Udaje.Data.Tabulka;
using Udalosti.Udaje.Nastavenia;
using Udalosti.Udaje.Siet.Model;
using Udalosti.Udaje.Zdroje;
using Udalosti.Udalost.Data;
using Udalosti.Udalost.SpracovanieDat;
using Udalosti.Uvod.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Udalosti.Udalost.UI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Podrobnosti : ContentPage, KommunikaciaData, KommunikaciaOdpoved
    {
        private UdalostiUdaje udalostiUdaje;
        private UvodnaObrazovkaUdaje uvodnaObrazovkaUdaje;

        private Pouzivatelia pouzivatel;
        private ObsahUdalosti udalost;

        private int stavTlacidla;
        private int idUdalost;
        private int pocetZaujemcov;

        public Podrobnosti (ObsahUdalosti udalost)
		{
            Debug.WriteLine("Metoda Podrobnosti bola vykonana");

            InitializeComponent();
            init(udalost);
        }

        private void init(ObsahUdalosti udalost)
        {
            Debug.WriteLine("Metoda init - Podrobnosti bola vykonana");

            this.udalostiUdaje = new UdalostiUdaje(this, this);
            this.uvodnaObrazovkaUdaje = new UvodnaObrazovkaUdaje();

            this.udalost = udalost;
            this.pouzivatel = this.uvodnaObrazovkaUdaje.prihlasPouzivatela();
        }

        protected async override void OnAppearing()
        {
            Debug.WriteLine("Metoda Podrobnosti - OnAppearing bola vykonana");

            await spracujZvolenuUdalost(this.udalost);
        }

        private async void zaujem(object sender, EventArgs e)
        {
            Debug.WriteLine("Metoda zaujem bola vykonana");

            if (this.stavTlacidla == 1)
            {
                try
                {
                    this.stavTlacidla = 0;
                    await this.udalostiUdaje.odstranZaujem(this.pouzivatel, this.idUdalost);
                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(async () => {
                        await DisplayAlert("Chyba", "Server je momentalne nedostupný!", "Zatvoriť");
                    });

                    Debug.WriteLine("CHYBA: " + ex.Message);
                }

            }
            else
            {
                try
                {
                    this.stavTlacidla = 1;
                    await this.udalostiUdaje.zaujem(this.pouzivatel, this.idUdalost);
                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(async () => {
                        await DisplayAlert("Chyba", "Server je momentalne nedostupný!", "Zatvoriť");
                    });

                    Debug.WriteLine("CHYBA: " + ex.Message);
                }
            }
        }

        public void dataZoServera(string odpoved, string od, List<ObsahUdalosti> udaje)
        {
            Debug.WriteLine("Metoda Podrobnosti - dataZoServera bola vykonana");

            switch (od)
            {
                case Nastavenia.ZAUJEM_POTVRD:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        if (udaje != null)
                        {
                            if (udaje.Count == 1)
                            {
                                ObsahUdalosti udalost = (ObsahUdalosti)udaje[0];
                                nacitajUdalosti(udalost, false, App.udalostiAdresa);
                            }
                        }
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () => {
                            await DisplayAlert("Chyba", odpoved, "Zatvoriť");
                        });
                    }
                    break;
            }
        }

        public void odpovedServera(string odpoved, string od, Dictionary<string, string> udaje)
        {
            Debug.WriteLine("Metoda Podrobnosti - odpovedServera bola vykonana");

            switch (od)
            {
                case Nastavenia.ZAUJEM_ODSTRANENIE:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        if (udaje["uspech"] != null)
                        {
                            this.pocetZaujemcov--;
                            pocetZaujemcovZvolenejUdalosti.Text = this.pocetZaujemcov.ToString();
                            AktualizatorObsahu.zaujmy().hodnota();

                            tlacidloZvolenejUdalosti.Text = "Mám záujem";
                            tlacidloZvolenejUdalosti.BackgroundColor = Color.FromHex("#005ba6");
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () => {
                                await DisplayAlert("Chyba", udaje["chyba"], "Zatvoriť");
                            });
                        }
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () => {
                            await DisplayAlert("Chyba", odpoved, "Zatvoriť");
                        });
                    }
                    break;

                case Nastavenia.ZAUJEM:
                    if (odpoved.Equals(Nastavenia.VSETKO_V_PORIADKU))
                    {
                        if (udaje["uspech"] != null)
                        {
                            this.pocetZaujemcov++;
                            pocetZaujemcovZvolenejUdalosti.Text = this.pocetZaujemcov.ToString();
                            AktualizatorObsahu.zaujmy().hodnota();

                            tlacidloZvolenejUdalosti.Text = "Odstrániť zo záujmov";
                            tlacidloZvolenejUdalosti.BackgroundColor = Color.FromHex("#ab2727");
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () => {
                                await DisplayAlert("Chyba", udaje["chyba"], "Zatvoriť");
                            });
                        }
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () => {
                            await DisplayAlert("Chyba", odpoved, "Zatvoriť");
                        });
                    }
                    break;
            }
        }

        public Task odpovedServeraAsync(string odpoved, string od, Dictionary<string, string> udaje)
        {
            Debug.WriteLine("Metoda Podrobnosti - odpovedServeraAsync bola vykonana");

            throw new NotImplementedException();
        }

        public Task dataZoServeraAsync(string odpoved, string od, List<ObsahUdalosti> udaje)
        {
            Debug.WriteLine("Metoda Podrobnosti - dataZoServeraAsync bola vykonana");

            throw new NotImplementedException();
        }

        private async Task spracujZvolenuUdalost(ObsahUdalosti udalost)
        {
            Debug.WriteLine("Metoda spracujZvolenuUdalost bola vykonana");

            if (udalost != null)
            {
                this.stavTlacidla = udalost.zaujem;
                this.idUdalost = udalost.idUdalost;
                this.pocetZaujemcov = udalost.zaujemcovia;

                nacitavanie.IsVisible = true;

                tlacidloZvolenejUdalosti.IsVisible = false;
                chybaObrazka.IsVisible = false;
                obrazokZvolenejUdalosti.IsVisible = false;
                titulUdalosti.IsVisible = false;
                odDelovac.IsVisible = false;
                zaujemcovia.IsVisible = false;
                vstupenka.IsVisible = false;
                cas.IsVisible = false;
                tlacidloZvolenejUdalosti.IsVisible = false;

                if (Spojenie.existuje())
                {
                    try
                    {
                        await this.udalostiUdaje.potvrdZaujem(this.pouzivatel, udalost.idUdalost);
                    }
                    catch (Exception ex)
                    {
                        nacitavanie.IsVisible = false;
                        Device.BeginInvokeOnMainThread(async () => {
                            await DisplayAlert("Chyba", "Server je momentalne nedostupný!", "Zatvoriť");
                        });

                        Debug.WriteLine("CHYBA: " + ex.Message);
                    }
                }
                else
                {
                    nacitajUdalosti(udalost, true, "");
                }
            }
        }

        private void nacitajUdalosti(ObsahUdalosti udalost, bool server, String host)
        {
            Debug.WriteLine("Metoda nacitajUdalosti bola vykonana");

            this.stavTlacidla = udalost.zaujem;

            obrazokZvolenejUdalosti.Source = ImageSource.FromUri(new Uri(host+udalost.obrazok));
            chybaObrazka.IsVisible = false;

            denZvolenejUdalosti.Text = udalost.den;
            mesiacZvolenejUdalosti.Text = udalost.mesiac.Substring(0, 3) + ".";
            nazovZvolenejUdalosti.Text = udalost.nazov;
            pocetZaujemcovZvolenejUdalosti.Text = udalost.zaujemcovia.ToString();
            vstupenkaZvolenejUdalosti.Text = udalost.vstupenka.ToString() + "€";
            casZvolenejUdalosti.Text = udalost.cas;

            if (server)
            {
                miestoZvolenejUdalosti.Text = udalost.mesto + udalost.ulica;
            }
            else
            {
                miestoZvolenejUdalosti.Text = udalost.mesto + ", " + udalost.ulica;
            }

            nastavTlacidloPodrobnosti(this.stavTlacidla);
        }

        private void nastavTlacidloPodrobnosti(int stavTlacidla)
        {
            Debug.WriteLine("Metoda nastavTlacidloPodrobnosti bola vykonana");

            if (stavTlacidla == 1)
            {
                tlacidloZvolenejUdalosti.Text = "Odstrániť zo záujmov";
                tlacidloZvolenejUdalosti.BackgroundColor = Color.FromHex("#ab2727");
            }
            else
            {
                tlacidloZvolenejUdalosti.Text = "Mám záujem";
                tlacidloZvolenejUdalosti.BackgroundColor = Color.FromHex("#005ba6");
            }

            tlacidloZvolenejUdalosti.IsVisible = true;
            chybaObrazka.IsVisible = true;
            obrazokZvolenejUdalosti.IsVisible = true;
            titulUdalosti.IsVisible = true;
            odDelovac.IsVisible = true;
            zaujemcovia.IsVisible = true;
            vstupenka.IsVisible = true;
            cas.IsVisible = true;
            tlacidloZvolenejUdalosti.IsVisible = true;

            nacitavanie.IsVisible = false;
        }
    }
}