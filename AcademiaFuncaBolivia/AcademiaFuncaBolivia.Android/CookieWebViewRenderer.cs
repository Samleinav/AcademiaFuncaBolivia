using AcademiaFuncaBolivia;
using AcademiaFuncaBolivia.Commons;
using AcademiaFuncaBolivia.Controls;
using AcademiaFuncaBolivia.Droid;
using AcademiaFuncaBolivia.Models;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Views;
using Android.Webkit;
using DemoXamarin.Droid.Renderers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using static Android.Webkit.WebSettings;
using Cookie = System.Net.Cookie;
using WebView = Android.Webkit.WebView;

[assembly: ExportRenderer(typeof(CookieWebView), typeof(CookieWebViewRenderer))]
namespace DemoXamarin.Droid.Renderers
{
    public class CookieWebViewRenderer : WebViewRenderer
    {
        Context _context;

        public CookieWebViewRenderer(Context context) : base(context)
        {
            _context = context;
        }

        protected async override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);
            Control.Settings.JavaScriptEnabled = true;
            Control.Settings.SetAppCacheEnabled(true);
            Control.Settings.CacheMode = Android.Webkit.CacheModes.Normal;
            Control.Settings.SetRenderPriority(RenderPriority.High);
            Control.Settings.DomStorageEnabled = true;
            Control.Settings.BlockNetworkImage = true;

            var fileName = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Cookie.json");

            if (!File.Exists(fileName) && UserInfo.CookieContainer.Count < 1)
            {
                await App.Current.MainPage.Navigation.PushModalAsync(new LoginView());
                return;
            }

            var currentCookies = File.Exists(fileName) ? JsonConvert.DeserializeObject<IList<Cookie>>(File.ReadAllText(fileName))
                : CookieHelper.CastToListCookies(UserInfo.CookieContainer.GetCookies(new System.Uri(Contants.INDEX_URL)));

            if (File.Exists(fileName) && CookieHelper.IsExpiredCookie(currentCookies))
            {
                File.Delete(fileName);
                await App.Current.MainPage.Navigation.PushModalAsync(new LoginView());
                return;
            }

            var cookieManager = CookieManager.Instance;
            cookieManager.SetAcceptCookie(true);
            cookieManager.RemoveAllCookie();

            for (var i = 0; i < currentCookies.Count; i++)
            {
                string cookieValue = currentCookies[i].Value;
                string cookieName = currentCookies[i].Name;
                currentCookies[i].Expires = currentCookies[i].TimeStamp.AddMinutes(Contants.EXPIRED_TIME);
                cookieManager.SetCookie(Contants.DOMAIN_URL, $"{cookieName}={cookieValue}");
            }
            if (!File.Exists(fileName))
            {
                var jsonToOutput = JsonConvert.SerializeObject(currentCookies);
                File.WriteAllText(fileName, jsonToOutput);
            }

            this.LoadUrl(Contants.INDEX_URL);
        }


    }

    public class ClientWebView : WebViewClient
    {
        private Context context;
        public ClientWebView(Context context)
        {
            this.context = context;
        }
       
        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {
            if (url != null && url.StartsWith("zoomus://"))
            {
                Android.Net.Uri uri = Android.Net.Uri.Parse(url);
                var intent = new Intent(Intent.ActionView, uri);
                context.StartActivity(intent);
                
                return true;
            }
            
            view.LoadUrl(url);
            return true;
        }

      

        public override void OnPageStarted(WebView view, string url, Bitmap favicon)
        {
            if (url.Equals(Contants.INDEX_URL))
            {
                var intent = new Intent(context, typeof(MainActivity));
                context.StartActivity(intent);

                var cookieContainer = new CookieContainer();
                var cookies = cookieContainer.GetCookies(new System.Uri(Contants.LOGIN_URL));
                if (cookies.Count > 0)
                {
                    UserInfo.CookieContainer = cookieContainer;
                }  
            }
            base.OnPageStarted(view, url, favicon);
        }

        public override void OnPageFinished(WebView view, string url)
        {  
            base.OnPageFinished(view, url);
        }

        public override void OnReceivedError(WebView view, IWebResourceRequest request, WebResourceError error)
        {
            base.OnReceivedError(view, request, error);
        }

       
    }
}