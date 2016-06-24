using System;
using System.Collections;
using System.Collections.Generic;
using CareApp.Views;
using Xamarin.Forms;

namespace CareApp
{
    public class App : Application
    {
        public App()
        {
            SetStyle();
            MainPage = new NavigationPage(new LoginView());
        }

        private void SetStyle()
        {
            Resources = new ResourceDictionary();
            var btnStyle = new Style(typeof(Button))
            {
                Setters =
                {
                    new Setter { Property = Button.TextColorProperty,  Value = Color.White},
                    new Setter { Property = Button.FontSizeProperty}
                }
            };

            var pageStyle = new Style(typeof(ContentPage))
            {
                Setters =
                {
                    new Setter { Property = ContentPage.BackgroundColorProperty, Value = Color.White }
                }
            };

            var stackStyle = new Style(typeof(ContentPage))
            {
                Setters =
                {
                    new Setter { Property = StackLayout.VerticalOptionsProperty, Value = LayoutOptions.CenterAndExpand }
                }
            };

            Resources.Add(nameof(btnStyle), btnStyle);
            Resources.Add(nameof(pageStyle), pageStyle);
            Resources.Add(nameof(stackStyle), stackStyle);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
