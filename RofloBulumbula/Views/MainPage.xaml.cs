using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Security;
using System.Runtime.CompilerServices;
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
        public List<Tour> AllTours
        {
            get => tours;
            set
            {
                tours = value;
                OnPropertyChanged();
            }
        }
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = this;
            LoadData();
            var countries = GetCity();
            picker.ItemsSource = countries;
            picker.SelectedIndex = 0;
        }
        private int _selectedSFilter;
        public int SelectedFilter { get => _selectedSFilter; set { _selectedSFilter = value; Filter(); } }
        private void Filter()
        {
           /* switch (SelectedFilter)
            {
                case 0:
                    LoadData();
                    break;
                case 1:
                    Tours = AllTours.Where(s => s.DepartureCity == "Смоленск").ToList();
                    break;
                case 2:
                    Tours = AllTours.Where(s => s.DepartureCity == "Самара").ToList();
                    break;
                case 3:
                    Tours = AllTours.Where(s => s.DepartureCity == "Чебоксары").ToList();
                    break;
                case 4:
                    Tours = AllTours.Where(s => s.DepartureCity == "Адлер").ToList();
                    break;
                case 5:
                    Tours = AllTours.Where(s => s.DepartureCity == "Тюмень").ToList();
                    break;
                default:
                    break;
            }*/
        }
       
        private async void LoadData()
        {
            try
            {
                var clientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = Cerf };
                var client = new HttpClient(clientHandler);
                var response = await client.GetAsync(App.AddressHome + "Home/Tour");
                var content = await response.Content.ReadAsStringAsync();
                Tours = JsonConvert.DeserializeObject<List<Tour>>(content);               
            }
            catch 
            {
                await DisplayAlert("Ошибка", "Сервер не отвечает", "Ок");
            }
        }

        
        private List<string> GetCity()
        {
            return new List<string>
            {
                "Все",
                "Смоленск",
                "Самара",
                "Чебоксары",
                "Адлер",
                "Тюмень"
            };
        }
        private bool Cerf(HttpRequestMessage arg1, X509Certificate2 arg2, X509Chain arg3, SslPolicyErrors arg4)
        {
            return true;
        }
        private async void BuyButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var content = ((Button)sender).BindingContext as Tour;
                var idClient = App.IDCLient;
                var voucher = new Voucher
                {
                    Idclients = idClient,
                    Idtours = content.Id,
                    DateSale = DateTime.Now
                };
                var json = JsonConvert.SerializeObject(voucher);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                var clientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = Cerf };
                var client = new HttpClient(clientHandler);
                var response = await client.PostAsync(App.AddressHome + "Home/AddVoucher", stringContent);
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Voucher>(responseContent);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Недоступно", "Перед тем как купить тур авторизируйтесь в системе, пожалуйста", "Ок");
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