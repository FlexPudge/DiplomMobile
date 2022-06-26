using SmolenskTravel.ToolKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace SmolenskTravel.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdersPage : ContentPage
    {
        private Voucher voucher;
        public Voucher Voucher { get => voucher; set { voucher = value; OnPropertyChanged(); } }

        private List<Voucher> vouchers;
        public List<Voucher> Vouchers
        {
            get => vouchers;
            set
            {
                vouchers = value;
                OnPropertyChanged();
            }
        }
        public OrdersPage()
        {
            InitializeComponent();
            LoadData();
            this.BindingContext = this;
        }
        private void Sort()
        {
            var id = App.IDCLient;
            Vouchers = Vouchers.Where(x => x.Idclients == id && x.Status == 1).ToList();
        }
        private async void LoadData()
        {
            var a = await HttpRequest.GetListAsync<Voucher>(App.AddressHome + "Home/Voucher");
            Vouchers = a;
            Sort();
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            var content = ((Button)sender).BindingContext;
            await Navigation.PushAsync(new DetailsOrderPage(content));
        }
        private async void lvOrders_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Voucher = e.SelectedItem as Voucher;
            if (Voucher != null)
            {
                await Navigation.PushAsync(new AboutOrdersPage(Voucher));
            }
        }
        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                var content = ((Button)sender).BindingContext as Voucher;
                int id = content.Id;
                var request = await HttpRequest.DeleteAsync<Voucher>(App.AddressHome + $"Home/DeleteVoucher.id={id}");
                if (request != null)
                {
                    await DisplayAlert("Сообщение", "Бронировение снято", "ОК");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Упс", ex.Message, "Ок");
            }
        }
    }
}