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
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Client content = new Client()
            {
                Login = LoginTextBox.Text,
                Password = PasswordTextBox.Text,
                Fio = FIOTextBox.Text,
                Phone = PhoneTextBox.Text,
                Email = PhoneTextBox.Text,
                Gender = GenderTextBox.Text
            };

            var a = await HttpRequest.PostAsync<Client>(App.AddressHome + "Home/Registration", content);
            if (a.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                await DisplayAlert("Ошибка", "Гляньте получше, кажется вы где то ошиблись!", "Ок");
            }
            else if (a.StatusCode == System.Net.HttpStatusCode.OK)
            {
                await DisplayAlert("Уведомление", "Поздравляю!", "Ок");
            }
        }
    }
}