using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace SmolenskTravel.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactsPage : ContentPage
    {
        public ContactsPage()
        {
            InitializeComponent();
        }

        public async Task OpenBrowser(Uri uri)
        {
            try
            {
                await Browser.OpenAsync(uri);
            }
            catch 
            {
                await DisplayAlert("101","201","301");
            }
        }
        private async void VkButton_Clicked(object sender, EventArgs e)
        {
            Uri uri = new Uri("https://vk.com/smolensktravel");
            await OpenBrowser(uri);
        }

        private async void WhatButton_Clicked(object sender, EventArgs e)
        {
            Uri uri = new Uri("https://www.whatsapp.com/?lang=ru");
            await OpenBrowser(uri);
        }

        private async void ViberButton_Clicked(object sender, EventArgs e)
        {
            Uri uri = new Uri("https://www.viber.com/ru/");
            await OpenBrowser(uri);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            string phoneNumber = "8 (4812) 70-15-60";
            try
            {
                PhoneDialer.Open(phoneNumber);
            }
            catch (Exception)
            {
                DisplayAlert("Ошибка", "Произошла непредвиденная ошибка, извините, мы пытаемся устранить эту ошибку", "Ок");
            }
        }
    }
}