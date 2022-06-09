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
    public partial class DocumentsPage : ContentPage
    {
        private Client client;
        public Client Client 
        {
            get => client;
            set 
            {
                client = value;
                OnPropertyChanged();
            } 
        }
        private List<Idpassport> pass;
        public List<Idpassport> Pass
        { 
            get => pass;
            set 
            { 
                pass = value;
                OnPropertyChanged();     
            }
        }
        public DocumentsPage()
        {
            InitializeComponent();
            this.BindingContext = this;
        }
        public DocumentsPage(Client client)
        {
            InitializeComponent();
            this.BindingContext = this;
            LoadPassportData();
            Client = client; 
        }
        private async void LoadPassportData()
        {
            var req = await HttpRequest.GetListAsync<Idpassport>(App.AddressHome + "Home/Passport");
            Pass = req.Where(x=>x.Idclient == Client.Id).ToList();
        }

       
    }
}