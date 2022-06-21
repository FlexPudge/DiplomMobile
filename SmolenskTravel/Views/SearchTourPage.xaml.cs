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
    public partial class SearchTourPage : ContentPage
    {
        private List<Tour> tours;
        public List<Tour> Tours
        {
            get => tours;
            set
            {
                tours = value;
                OnPropertyChanged();
            }
        }
        public SearchTourPage()
        {
            InitializeComponent();
        }

        public SearchTourPage(List<Tour> tours)
        {
            InitializeComponent();
            this.BindingContext = this;
            Tours = tours;
        }

        private async void BuyButton_Clicked(object sender, EventArgs e)
        {
            var content = ((Button)sender).BindingContext as Tour;
            await Navigation.PushAsync(new BookingPage(content));
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (App.IDCLient != 0)
            {
                Tour selectedTour = e.SelectedItem as Tour;
                if (selectedTour != null)
                {
                    await Navigation.PushAsync(new AboutTourPage(selectedTour));
                }
            }
            if (App.IDCLient == 0)
            {
                await DisplayAlert("Ошибка", "Авторизируйтесь в системе", "Ок");
            }

        }
    }
}