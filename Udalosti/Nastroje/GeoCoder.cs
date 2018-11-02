using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Udalosti.Udaje.Nastavenia;
using Xamarin.Forms;

namespace Udalosti.Nastroje
{
    class GeoCoder
    {
        private static Page page;

        public static async Task<Dictionary<string, double>> zistiPolohu(ActivityIndicator nacitavanie, Page page)
        {
            Debug.WriteLine("Metoda zistiPolohu(ActivityIndicator nacitavanie, Page page) bola vykonana");

            GeoCoder.page = page;

            var geo = (IGeolocator)null;
            var pozicia = (Position)null;

            try
            {
                var pravoPrePoziciu = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (pravoPrePoziciu != PermissionStatus.Granted)
                {
                    var odpoved = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    pravoPrePoziciu = odpoved[Permission.Location];
                }

                if (pravoPrePoziciu == PermissionStatus.Granted)
                {
                    if (nacitavanie != null)
                    {
                        nacitavanie.IsVisible = true;
                    }

                    geo = CrossGeolocator.Current;
                    geo.DesiredAccuracy = 50;

                    pozicia = await geo.GetPositionAsync(TimeSpan.FromSeconds(Nastavenia.DLZKA_REQUESTU));

                }
                else if (pravoPrePoziciu != PermissionStatus.Unknown)
                {
                    geo = null;
                    Device.BeginInvokeOnMainThread(async () => {
                        await GeoCoder.page.DisplayAlert("Chyba", "Prístup k pozície bol odmietnutý!", "Zatvoriť");
                    });
                }
            }
            catch (Exception e)
            {
                geo = null;
                Debug.WriteLine("Lokalizator.cs CHYBA:" + e.Message);
                Device.BeginInvokeOnMainThread(async () => {
                    await GeoCoder.page.DisplayAlert("Chyba", "GPS je vypnutý alebo prístup k aktuálnej polohe bol odmietnutý", "Zatvoriť");
                });
            }


            Dictionary<string, double> poloha;
            if (geo != null)
            {
                poloha = new Dictionary<string, double>();
                poloha.Add("zemepisnaSirka", pozicia.Latitude);
                poloha.Add("zemepisnaDlzka", pozicia.Longitude);
            }
            else
            {
                poloha = null;
            }

            return poloha;
        }

        public static async Task<Dictionary<string, double>> zistiPolohu(Page page)
        {
            Debug.WriteLine("Metoda zistiPolohu(Page page) bola vykonana");

            GeoCoder.page = page;

            var geo = (IGeolocator)null;
            var pozicia = (Position)null;

            try
            {
                var pravoPrePoziciu = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (pravoPrePoziciu != PermissionStatus.Granted)
                {
                    var odpoved = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    pravoPrePoziciu = odpoved[Permission.Location];
                }

                if (pravoPrePoziciu == PermissionStatus.Granted)
                {
                    geo = CrossGeolocator.Current;
                    geo.DesiredAccuracy = 50;

                    pozicia = await geo.GetPositionAsync(TimeSpan.FromSeconds(Nastavenia.DLZKA_REQUESTU));

                }
                else if (pravoPrePoziciu != PermissionStatus.Unknown)
                {
                    geo = null;
                    Device.BeginInvokeOnMainThread(async () => {
                        await GeoCoder.page.DisplayAlert("Chyba", "Prístup k pozície bol odmietnutý!", "Zatvoriť");
                    });
                }
            }
            catch (Exception e)
            {
                geo = null;
                Debug.WriteLine("Lokalizator.cs CHYBA:" + e.Message);

                Device.BeginInvokeOnMainThread(async () => {
                    await GeoCoder.page.DisplayAlert("Chyba", "GPS je vypnutý alebo prístup k aktuálnej polohe bol odmietnutý", "Zatvoriť");
                });
            }


            Dictionary<string, double> poloha;
            if (geo != null)
            {
                poloha = new Dictionary<string, double>();
                poloha.Add("zemepisnaSirka", pozicia.Latitude);
                poloha.Add("zemepisnaDlzka", pozicia.Longitude);
            }
            else
            {
                poloha = null;
            }

            return poloha;
        }
    }
}