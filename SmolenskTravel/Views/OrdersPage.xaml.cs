using SmolenskTravel.ToolKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmolenskTravel.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdersPage : ContentPage
    {
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
            this.BindingContext = this;
            Sort();
        }
        private async void Sort()
        {
            await LoadData();
            var id = App.IDCLient;
            Vouchers = Vouchers.Where(x => x.Idclients == id && x.Status == 1).ToList();
        }
        private async Task LoadData()
        {
            var a = await HttpRequest.GetListAsync<Voucher>(App.AddressHome + "Home/Voucher");
            Vouchers = a;
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            var content = ((Button)sender).BindingContext;
            await Navigation.PushAsync(new DetailsOrderPage(content));
        }
    }
}