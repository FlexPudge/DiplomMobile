﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmolenskTravel
{
    public partial class App : Application
    {
        //public static string AddressHome => "https://169.254.74.101:5001/";




        public static string AddressHome => "https://192.168.1.45:5001/";
        public static string Tours => "Home/Tour";

        public static bool Auth;

        public static int IDCLient;

        public static Client Client;



        public App()
        {
            InitializeComponent();

            MainPage = new MenuPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
