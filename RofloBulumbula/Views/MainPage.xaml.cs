using Newtonsoft.Json;
using RofloBulumbula.ToolKit;
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

namespace RofloBulumbula.Views
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
        private int _selectedSFilter;
        public int SelectedFilter 
        { 
            get => _selectedSFilter;
            set 
            {
                _selectedSFilter = value;
               // Filter();
            }
        }
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = this;
            Tours = AllTours;
        }
        private void Sort()
        {
            if (SelectedSort == 0)
            {
                LoadData();
            }
            if (SelectedSort == 1)
            {
                AllTours = AllTours.OrderBy(x=>x.Price).ToList();
            }
            if (SelectedSort == 2)
            {
                AllTours = AllTours.OrderBy(x=>x.Price).ToList();
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
        private async void BuyButton_Clicked(object sender, EventArgs e)
        {
          
                var content = ((Button)sender).BindingContext as Tour;
                var idClient = App.IDCLient;
                var voucher = new Voucher
                {
                    Idclients = idClient,
                    Idtours = content.Id,
                    DateSale = DateTime.Now
                };
               var a = await HttpRequest.PostAsync<Voucher>(App.AddressHome + "Home/AddVoucher",voucher);
            if (a.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                await DisplayAlert("Ошибка","Перед тем как купить тур авторизируйтесь","Ок");
            }
            else if (a.StatusCode == System.Net.HttpStatusCode.OK)
            {
                await DisplayAlert("Уведомление", "Вы купили, посмотреть информацию о покупке можно в профиле!", "Ок");
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