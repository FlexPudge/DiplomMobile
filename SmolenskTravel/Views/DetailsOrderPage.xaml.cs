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
    public partial class DetailsOrderPage : ContentPage
    {
        private Client client;
        public Client Client { get => client; set { client = value; OnPropertyChanged(); } }

        private object _content;
        public object Contents { get=>_content; set { _content = value; OnPropertyChanged(); } }
        public DetailsOrderPage()
        {
            InitializeComponent();
            this.BindingContext = this;
        }

        public DetailsOrderPage(object content)
        {
            InitializeComponent();
            this.BindingContext = this;
            Contents = content;
            Client = App.Client;
        }

        
    }
}