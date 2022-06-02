using Newtonsoft.Json;
using RofloBulumbula.ToolKit;
using System;
using System.Collections.Generic;
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
    public partial class HeartPage : ContentPage
    {
        private List<Favorite> favorites;
        public List<Favorite> Favorites
        {
            get => favorites;
            set
            {
                favorites = value;
                OnPropertyChanged();
            }
        }
        public HeartPage()
        {
            InitializeComponent();
            this.BindingContext = this; 
        }
        private async Task Sort()
        {
            await LoadData();
            var id = App.IDCLient;
            Favorites = Favorites.Where(x => x.Idclient == id).ToList();
        }
        private async Task LoadData()
        {
            try
            {
                var clientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = Cerf };
                var client = new HttpClient(clientHandler);
                var response = await client.GetAsync(App.AddressHome + "Home/Favorite");
                var content = await response.Content.ReadAsStringAsync();
                Favorites = JsonConvert.DeserializeObject<List<Favorite>>(content);
            }
            catch(Exception e)
            {
                await DisplayAlert("Ошибка", e.Message, "Ок");
            }
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
                var a = await HttpRequest.PostAsync<Voucher>(App.AddressHome + "Home/AddVoucher", voucher);
                if (a.IsSuccessStatusCode == true)
                {
                    await DisplayAlert("Уведомление", "Тур куплен!", "Ок");
                }
            }
            catch
            {
                await DisplayAlert("Недоступно", "Перед тем как купить тур авторизируйтесь в системе, пожалуйста", "Ок");
            }
        }
        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            await Sort();
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Favorite selectedFavoritetour = e.SelectedItem as Favorite;
            if (selectedFavoritetour != null)
            {
                await Navigation.PushAsync(new AboutHeartPage(selectedFavoritetour));
            }
        }
    }
}