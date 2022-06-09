
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
    public partial class AboutTourPage : ContentPage
    {
        private Tour tour;
        public Tour Tours { get=> tour; set { tour = value; OnPropertyChanged(); } }

        private List<AboutPhoto> aboutPhotos;
        public List<AboutPhoto> AboutPhotos { get => aboutPhotos; set { aboutPhotos = value; OnPropertyChanged(); } }


        private string lvl;
        public string Lvl { get => lvl;set { lvl = value;OnPropertyChanged();  } }

        public AboutTourPage()
        {
            InitializeComponent();
            this.BindingContext = this;
        }

        public AboutTourPage(Tour selectedTour)
        {
            InitializeComponent();
            this.BindingContext = this;
            Tours = selectedTour;
            ComplextySwitch();
            LoadPhoto();
        }

        private  void ComplextySwitch()
        {
            /*switch (Tours.Complexity)
            {
                case 1:
                    var a = "Базовый";
                    lvlLabel.Text = $"Уровень физ. подготовки {a}";
                    return;
                case 2:
                    var b = "Средний";
                    lvlLabel.Text = $"Уровень физ. подготовки {b}";
                    return;
                case 3:
                    var c = "Продвинутый";
                    lvlLabel.Text = $"Уровень физ. подготовки {c}";
                    return;
                case 4:
                    var d = "Сложный";
                    lvlLabel.Text = $"Уровень физ. подготовки {d}";
                    return;

            }*/
        }
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                var idClient = App.IDCLient;
                var favorite = new Favorite
                {
                    Idclient=idClient,
                    Idtours = Tours.Id

                };
                string address = App.AddressHome + "Home/AddFavorite";
                var a = await HttpRequest.PostAsync<Favorite>(address, favorite);
                if (a.IsSuccessStatusCode == true)
                {
                    await DisplayAlert("Уведомление", "Тур добавлен в избранное!", "Ок");
                }
            }
            catch
            {
                await DisplayAlert("Недоступно", "Перед тем как добавить тур в избранное авторизируйтесь в системе, пожалуйста", "Ок");
            }
        }

        private async void LoadPhoto()
        {
            int id = Tours.Id;
            //var resp = await HttpRequest.GetAsync<AboutPhoto>(App.AddressHome + $"Home/LoadAboutPhoto.id={id}");
           // AboutPhotos = JsonConvert.DeserializeObject<List<AboutPhoto>>(resp.Photo);
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

        private async void BuyButton_Clicked(object sender, EventArgs e)
        {
            //var content = ((Button)sender).BindingContext as Tour;
            var idClient = App.IDCLient;
            var voucher = new Voucher
            {
                Idclients = idClient,
                Idtours = Tours.Id,
                DateSale = DateTime.Now
            };
            var a = await HttpRequest.PostAsync<Voucher>(App.AddressHome + "Home/AddVoucher", voucher);
            if (a.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                await DisplayAlert("Ошибка", "Перед тем как купить тур авторизируйтесь", "Ок");
            }
            else if (a.StatusCode == System.Net.HttpStatusCode.OK)
            {
                await DisplayAlert("Уведомление", "Вы купили, посмотреть информацию о покупке можно в профиле!", "Ок");
            }
        }
    }
}