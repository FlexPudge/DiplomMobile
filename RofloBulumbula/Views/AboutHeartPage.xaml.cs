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
    public partial class AboutHeartPage : ContentPage
    {
        private Favorite selected;
        public Favorite SelectedFavoritetour { get=> selected; set { selected = value;OnPropertyChanged(); } }
        public AboutHeartPage()
        {
            InitializeComponent();
        }

        public AboutHeartPage(Favorite selectedFavoritetour)
        {
            InitializeComponent();
            this.BindingContext = this;
            SelectedFavoritetour = selectedFavoritetour;
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
    }
}