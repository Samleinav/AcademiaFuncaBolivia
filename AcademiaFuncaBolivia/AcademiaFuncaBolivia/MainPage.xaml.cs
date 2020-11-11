﻿using AcademiaFuncaBolivia.Commons;
using Android.Webkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace AcademiaFuncaBolivia
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.academiaView.Source = new UrlWebViewSource() { Url = Contants.INDEX_URL };
        }


        public void SetPage(string url)
        {
            this.academiaView.Source = new UrlWebViewSource() { Url = url };
        }
    }
}
