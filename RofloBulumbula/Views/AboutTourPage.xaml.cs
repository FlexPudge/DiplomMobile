
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
                await HttpRequest.PostAsync<Favorite>(address, favorite);
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

        private void BuyButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}