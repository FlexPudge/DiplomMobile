using Newtonsoft.Json;
using SmolenskTravel.ToolKit;
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

namespace SmolenskTravel.Views
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
            lvFavorite.SelectedItem = null;
            CheckLoadList();
        }
        private void CheckLoadList()
        {
            if (App.IDCLient != 0)
            {
                //lvFavorite.IsVisible = true;
                lvFavorite.RefreshCommand = new Command(() =>
                {
                    RefreshData();
                    lvFavorite.IsRefreshing = false;
                });
            }
            if (App.IDCLient == 0)
            {
                //lvFavorite.IsVisible = false;
            }
        }

        public async void RefreshData()
        {
            await Sort();
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
            catch (Exception e)
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
        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            await Sort();
        }
        private async void OnDeleteSwipeItemInvoked(object sender, EventArgs e)
        {
            var content = ((MenuItem)sender).BindingContext as Favorite;
            //var deleteFavorit = Favorites.Find(x => x.Id == content.Id);
            var resp = await HttpRequest.PostAsyncNotCode<Favorite>(App.AddressHome + "Home/DeleteFavorite", content);
            Favorites = resp;
        }
    }
}