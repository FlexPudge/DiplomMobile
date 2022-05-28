using Newtonsoft.Json;
using RofloBulumbula.ToolKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RofloBulumbula.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage, INotifyPropertyChanged
    {
        private List<Tour> tours;
        public List<Tour> Tours
        {
            get => tours;
            set
            {
                tours = value;
                OnPropertyChanged();
            }
        }
        private List<Tour> allTours;
        public List<Tour> AllTours
        {
            get => allTours;
            set
            {
                allTours = value;
                OnPropertyChanged();
            }
        }
        private Tour selectedDate;
        public Tour SelectedDate
        {
            get => selectedDate;
            set
            {
                selectedDate = value;
                OnPropertyChanged();
            }
        }
        public SearchPage()
        {
            InitializeComponent();
            this.BindingContext = this;
            LoadData();
        }
        public async void SelectedTour()
        {
            Tours = AllTours;
            try
            {
                string city = cityP.Items[cityP.SelectedIndex];
                string type = typeP.Items[typeP.SelectedIndex];
                var date = SelectedDate.Date;
               /* if (city != null && type != null && date != null)
                {
                Tours = Tours.Where(x => x.Location == city && x.Date == date && x.TypeTour == type).ToList();
                }
                if (city != null || type != null || date != null)
                {
                 Tours = Tours.Where(x => x.Location == city || x.Date == date || x.TypeTour == type).ToList();
                }*/
                Tours = Tours.Where(x => x.Location==(city ?? x.Location)
                && x.TypeTour == (type ?? x.TypeTour)
                && x.Date==(date ?? x.Date)).ToList();
                if (Tours.Count != 0)
                {
                   await Navigation.PushAsync(new SearchTourPage(Tours));
                }
                if (Tours.Count == 0)
                {
                    await DisplayAlert("Уведомление","Поиск не дал результатов. Попробуйте поискать по другим параматерам","Ок");
                }
            }
            catch
            {
               await Navigation.PushAsync(new MainPage());
            }

        }
        private async void LoadData()
        {
            try
            {
                var clientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = Cerf };
                var client = new HttpClient(clientHandler);
                var response = await client.GetAsync(App.AddressHome + "Home/Tour");
                var content = await response.Content.ReadAsStringAsync();
                AllTours = JsonConvert.DeserializeObject<List<Tour>>(content);
            }
            catch
            {
                await DisplayAlert("Ошибка", "Сервер не отвечает", "Ок");
            }
        }
        private bool Cerf(HttpRequestMessage arg1, X509Certificate2 arg2, X509Chain arg3, SslPolicyErrors arg4)
        {
            return true;
        }
        private  void buttonSearchTours_Clicked(object sender, EventArgs e)
        {
            SelectedTour();
        }
        private async void buttonShowTour_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}