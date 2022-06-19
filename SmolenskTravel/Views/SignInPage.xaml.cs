using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmolenskTravel.Views
{
    public class LoginRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            InitializeComponent();
            this.BindingContext = this;
        }

        private string _login;

        public string Login
        {
            get => _login; set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        private string _password;
        public string Password
        {
            get => _password; set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        private ICommand _signInCommand;
        public ICommand SignInCommand => _signInCommand ?? (_signInCommand = new Command(execute: () => TrySignIn()));

        bool check = true;
        private async void TrySignIn()
        {
            try
            {
                var clientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = Cerf };
                var client = new HttpClient(clientHandler);
                var response = await client.GetAsync(App.AddressHome + $"Home/Login.login={LoginTextBox.Text}.password={PasswordTextBox.Text}");
                if (response != null)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Client datalist = JsonConvert.DeserializeObject<Client>(content);
                    App.Auth = check;
                    App.IDCLient = datalist.Id;
                    App.Client = datalist;
                    // await Navigation.PushModalAsync(new ProfilePage(datalist)); открывает окно убирая боттом меню и тоол бар
                    await Navigation.PushAsync(new ProfilePage(datalist));
                }
            }
            catch
            {
                await DisplayAlert("Ошибка", "Неправильно введен логин или пароль", "Ок");
            }
        }
        private bool Cerf(HttpRequestMessage arg1, X509Certificate2 arg2, X509Chain arg3, SslPolicyErrors arg4)
        {
            return true;
        }

        private void LoginButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}