using AcademiaFuncaBolivia.Commons;
using Android.Telephony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AcademiaFuncaBolivia
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
            this.loginview.Source = new UrlWebViewSource() { Url = Contants.LOGIN_URL };
        }
    }
}