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
                    new Setter { Property = Button.TextColorProperty, Value = Color.White},
                    new Setter { Property = Button.FontSizeProperty},
                    new Setter { Property = Button.BackgroundColorProperty, Value = Color.Teal}
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

            var lblStyle = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter { Property = Label.FontSizeProperty, Value = 18 },
                    new Setter { Property = Label.TextColorProperty, Value = Color.Teal }
                }
            };

            var txtStyle = new Style(typeof(Entry))
            {
                Setters =
                {
                    new Setter { Property = Entry.FontSizeProperty, Value = 18 },
                    new Setter { Property = Entry.TextColorProperty, Value = Color.Teal }
                }
            };

            Resources.Add(nameof(btnStyle), btnStyle);
            Resources.Add(nameof(pageStyle), pageStyle);
            Resources.Add(nameof(stackStyle), stackStyle);
            Resources.Add(nameof(lblStyle), lblStyle);
            Resources.Add(nameof(txtStyle), txtStyle);
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
