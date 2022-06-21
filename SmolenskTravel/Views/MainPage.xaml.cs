using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using SmolenskTravel.ToolKit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmolenskTravel.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
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
        private List<Tour> alltours;
        public List<Tour> AllTours
        {
            get => alltours;
            set
            {
                alltours = value;
                OnPropertyChanged();
            }
        }
        private Tour tour;
        public Tour Tour { get => tour; set { tour = value; OnPropertyChanged(); } }

        private int selectedSort;
        public int SelectedSort
        {
            get => selectedSort;
            set
            {
                selectedSort = value;
                OnPropertyChanged();
                Sort();
            }
        }
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = this;
            Tours = AllTours;
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
        private void Sort()
        {
            Tours = AllTours;
            if (SelectedSort == 0)
            {
                LoadData();
            }
            if (SelectedSort == 1)
            {
                AllTours = AllTours.OrderBy(x => x.Price).ToList();
            }
            if (SelectedSort == 2)
            {
                AllTours = AllTours.OrderByDescending(x => x.Price).ToList();
            }
            if (SelectedSort == 3)
            {
                AllTours = AllTours.OrderBy(x => x.Duration).ToList();
            }
            if (SelectedSort == 4)
            {
                AllTours = AllTours.OrderByDescending(x => x.Duration).ToList();
            }
        }
        private bool Cerf(HttpRequestMessage arg1, X509Certificate2 arg2, X509Chain arg3, SslPolicyErrors arg4)
        {
            return true;
        }
        private async void BuyButton_Clicked(object sender, EventArgs e)
        {
            if (App.IDCLient != 0)
            {
                var content = ((Button)sender).BindingContext as Tour;
                await Navigation.PushAsync(new BookingPage(content));
            }
            if (App.IDCLient == 0)
            {
                await DisplayAlert("Уведомление", "Авторизируйтесь в системе", "Ок");
            }
        }
        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Tour selectedTour = e.SelectedItem as Tour;
            if (selectedTour != null)
            {
                await Navigation.PushAsync(new AboutTourPage(selectedTour));
            }
        }
    }
}