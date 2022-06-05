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
    public partial class DocumentsPage : ContentPage
    {
        private Client client;
        public Client Client { get=>client;set { client = value; OnPropertyChanged(); } }
        public DocumentsPage()
        {
            InitializeComponent();
        }
        public DocumentsPage(Client client)
        {
            InitializeComponent();
            this.BindingContext = this;
            Client = client;
        }

       
    }
}