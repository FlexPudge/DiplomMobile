
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
    public partial class AboutTourPage : ContentPage
    {
        public Tour Tours { get; set; }
        public AboutTourPage(Tour selectedTour)
        {
            InitializeComponent();
            Tours = selectedTour;
            this.BindingContext = Tours;
        }
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("1","1","1");
        }
    }
}