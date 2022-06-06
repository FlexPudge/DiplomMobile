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

namespace RofloBulumbula.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            this.BindingContext = this;
            if (App.Auth != true)
            {
                NonAuth.IsVisible = true;
                Aut.IsVisible = false;
            }
        }
        public ProfilePage(Client datalist)
        {
            InitializeComponent();
            this.BindingContext = this;
            Client = datalist;
            if (App.Auth == true)
            {
                NonAuth.IsVisible = false;
                Aut.IsVisible = true;
            }
        }
        private Client client { get; set; }
        public Client Client 
        {
            get => client; 
            set 
            {
                client = value;
                OnPropertyChanged();
            } 
        }
        private async void SignInButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignInPage() );
        }
        private async void SignUpButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }
        private async void OrdersButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OrdersPage());
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HistoryOrdersPage());
        }

        private async void DocumentButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DocumentsPage(Client));
        }
    }
}