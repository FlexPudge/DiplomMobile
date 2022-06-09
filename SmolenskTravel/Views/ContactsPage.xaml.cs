﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
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
            //var url = Android.Net.Uri.Parse("tel:+79088888888");
            //var inet = new Intent(Intent.ActionCall, url);
            //StartActivity(intent);
        }
    }
}