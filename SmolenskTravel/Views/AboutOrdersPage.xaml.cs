using Newtonsoft.Json;
using SmolenskTravel.ToolKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmolenskTravel.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutOrdersPage : ContentPage
    {
        private Tour tour;
        public Tour Tours { get => tour; set { tour = value; OnPropertyChanged(); } }

        private Voucher voucher;
        public Voucher Voucher { get => voucher; set { voucher = value; OnPropertyChanged(); } }

        private List<AboutPhoto> aboutPhotos;
        public List<AboutPhoto> AboutPhotos { get => aboutPhotos; set { aboutPhotos = value; OnPropertyChanged(); } }
        public AboutOrdersPage()
        {
            InitializeComponent();
            this.BindingContext = this;
        }

        public AboutOrdersPage(Voucher selectedVoucher)
        {
            InitializeComponent();
            this.BindingContext = this;
            Voucher = selectedVoucher;
            LoadData();
        }

        private async void LoadData()
        {
            var clientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (ao, f, s, a) => true };
            var client = new HttpClient(clientHandler);
            var response = await client.GetAsync(App.AddressHome + $"Home/Tour.id={Voucher.Idtours}");
            var content = await response.Content.ReadAsStringAsync();
            Tours = JsonConvert.DeserializeObject<Tour>(content);
            LoadPhotoTour();
        }
        private async void LoadPhotoTour()
        {
            int id = Tours.Id;
            try
            {
                var clientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (ao, f, s, a) => true };
                var client = new HttpClient(clientHandler);
                var response = await client.GetAsync(App.AddressHome + $"Home/LoadAboutPhoto.id={id}");
                var content = await response.Content.ReadAsStringAsync();
                AboutPhotos = JsonConvert.DeserializeObject<List<AboutPhoto>>(content);
            }
            catch
            {
                await DisplayAlert("Ошибка", "Сервер не отвечает", "Ок");
            }
        }

        private async void AboutButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MoreTourPage(Tours));
        }
    }
}