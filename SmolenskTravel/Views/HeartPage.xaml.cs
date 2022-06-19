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
            if (App.IDCLient != 0)
            {
                lvFavorite.IsVisible = true;
                lvFavorite.RefreshCommand = new Command(() =>
                {
                    RefreshData();
                    lvFavorite.IsRefreshing = false;
                });
            }
            else
            {
                lvFavorite.IsVisible = false;
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
                    await DisplayAlert("Уведомление", "Вы забронировали тур!", "Ок");
                }
            }
            catch
            {
                await DisplayAlert("Недоступно", "Перед тем как забронировать тур авторизируйтесь в системе, пожалуйста", "Ок");
            }
        }
        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            await Sort();
        }

        private  void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            /*Favorite selectedFavoritetour = e.SelectedItem as Favorite;
            if (selectedFavoritetour != null)
            {
                await Navigation.PushAsync(new AboutHeartPage(selectedFavoritetour));
            }*/
        }
        private void OnMore(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("More Context Action", mi.CommandParameter + " more context action", "OK");
        }

        private void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        }

       /* private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var content = ((Button)sender).BindingContext as Favorite;
            //var deleteFavorit = Favorites.Find(x => x.Id == content.Id);
            var resp = await HttpRequest.PostAsyncNotCode<Favorite>(App.AddressHome + "Home/DeleteFavorite", content);
            Favorites = resp;
        }*/

        private async void OnDeleteSwipeItemInvoked(object sender, EventArgs e)
        {
            var content = ((MenuItem)sender).BindingContext as Favorite;
            //var deleteFavorit = Favorites.Find(x => x.Id == content.Id);
            var resp = await HttpRequest.PostAsyncNotCode<Favorite>(App.AddressHome + "Home/DeleteFavorite", content);
            Favorites = resp;
        }
    }
}