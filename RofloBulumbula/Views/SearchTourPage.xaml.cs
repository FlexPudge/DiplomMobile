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
    public partial class SearchTourPage : ContentPage
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
        public SearchTourPage()
        {
            InitializeComponent();
        }

        public SearchTourPage(List<Tour> tours)
        {
            InitializeComponent();
            this.BindingContext = this;
            Tours = tours;
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