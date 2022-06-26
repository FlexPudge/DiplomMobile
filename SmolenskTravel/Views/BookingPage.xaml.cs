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
    public partial class BookingPage : ContentPage
    {
        private Client client;
        public Client Client { get => client; set { client = value; OnPropertyChanged(); } }

        private Tour tour;
        public Tour Tour { get => tour; set { tour = value; OnPropertyChanged(); } }

        public BookingPage()
        {
            InitializeComponent();
            this.BindingContext = this;
        }
        public BookingPage(Tour content)
        {
            InitializeComponent();
            this.BindingContext = this;
            Tour = content;
            Client = App.Client;
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FIO.Text) || FIO.Text == "0")
            {
                await DisplayAlert("Ошибка", "Заполните поле - ФИО", "Ок");
                return;
            }
            if (string.IsNullOrEmpty(Phone.Text) || Phone.Text == "0")
            {
                await DisplayAlert("Ошибка", "Заполните поле - Номер телефона", "Ок");
                return;
            }
            if (string.IsNullOrEmpty(Email.Text) || Email.Text == "0")
            {
                await DisplayAlert("Ошибка", "Заполните поле - Email", "Ок");
                return;
            }
            if (string.IsNullOrEmpty(HumanAmount.Text) || HumanAmount.Text == "0")
            {
                await DisplayAlert("Ошибка", "Заполните поле - количество человек", "Ок");
                return;
            }
            if (string.IsNullOrEmpty(SelTour.Text) || SelTour.Text == "0")
            {
                await DisplayAlert("Ошибка", "Заполните поле - Выбранный тур", "Ок");
                return;
            }
            if (string.IsNullOrEmpty(Comment.Text) || Comment.Text == "0")
            {
                await DisplayAlert("Ошибка", "Заполните поле - Комментарий", "Ок");
                return;
            }
            Random rnd = new Random();
            int randomNumberOrders = rnd.Next(999, 10000);
            var idClient = App.IDCLient;
            var voucher = new Voucher
            {
                Idclients = idClient,
                Idtours = Tour.Id,
                DateSale = DateTime.Now,
                Amount = Convert.ToInt32(HumanAmount.Text),
                NumberOrders = randomNumberOrders,
                Status = 1
            };
            var a = await HttpRequest.PostAsync<Voucher>(App.AddressHome + "Home/AddVoucher", voucher);
            if (a.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                await DisplayAlert("Ошибка", "Перед тем как забронировать тур авторизируйтесь", "Ок");
            }
            else if (a.StatusCode == System.Net.HttpStatusCode.OK)
            {
                await DisplayAlert("Уведомление", "Вы забронировали тур, посмотреть информацию можно в профиле в разделе заказы!", "Ок");
            }
        }
    }
}