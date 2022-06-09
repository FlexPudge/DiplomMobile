using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmolenskTravel.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoreTourPage : ContentPage
    {
        private Tour tours;
        public Tour Tours 
        {
            get => tours;
            set
            {
                tours = value;
                OnPropertyChanged();
            }
        }
        public MoreTourPage()
        {
            InitializeComponent();
            this.BindingContext = this;
        }
        public MoreTourPage(Tour tours)
        {
            InitializeComponent();
            Tours = tours;
            this.BindingContext = this;
            LoadLabelDays();
        }

        public void LoadLabelDays()
        {
            if (Tours.IdprogrammTourNavigation.Day3 == null)
            {
              //  FrameDay3.IsVisible = false;
            }
            if (Tours.IdprogrammTourNavigation.Day4 == null)
            {
                FrameDay4.IsVisible = false;
            }
            if (Tours.IdprogrammTourNavigation.Day5 == null)
            {
                FrameDay5.IsVisible = false;
            }
            if (Tours.IdprogrammTourNavigation.Day6 == null)
            {
                FrameDay6.IsVisible = false;
            }
            if (Tours.IdprogrammTourNavigation.Day7 == null)
            {
                FrameDay7.IsVisible = false;
            }
        }

        
    }
}