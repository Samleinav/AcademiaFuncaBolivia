using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xamarin.Forms;

namespace AcademiaFuncaBolivia.Controls
{
    public class CookieWebView : Xamarin.Forms.WebView
    {
        public static new readonly BindableProperty CookiesProperty = BindableProperty.Create(
        propertyName: "Cookies",
            returnType: typeof(CookieContainer),
            declaringType: typeof(CookieWebView),
          defaultValue: default(string));
    }
}
