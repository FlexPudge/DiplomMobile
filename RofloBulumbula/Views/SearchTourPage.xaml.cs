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
    }
}