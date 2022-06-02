
using RofloBulumbula.ToolKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RofloBulumbula.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutTourPage : ContentPage
    {
        private Tour tour;
        public Tour Tours { get=> tour; set { tour = value; OnPropertyChanged(); } }

        private string lvl;
        public string Lvl { get => lvl;set { lvl = value;OnPropertyChanged();  } }
        public AboutTourPage(Tour selectedTour)
        {
            InitializeComponent();
            Tours = selectedTour;
            this.BindingContext = Tours;
            ComplextySwitch();
        }

        private  void ComplextySwitch()
        {
            switch (Tours.Complexity)
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

            }
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
                    await DisplayAlert("Уведомление", "Тур куплен!", "Ок");
                }
            }
            catch
            {
                await DisplayAlert("Недоступно", "Перед тем как купить тур авторизируйтесь в системе, пожалуйста", "Ок");
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