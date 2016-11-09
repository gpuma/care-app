using System;
using System.Collections;
using System.Collections.Generic;
using CareApp.Views;
using Xamarin.Forms;

namespace CareApp
{
    public class App : Application
    {
        Color mainColor = Color.Purple;
        Color otherColor = Color.White;
        Color thirdColor = Color.Gray;
        int normalFontSize = 20;
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
                    new Setter { Property = Button.TextColorProperty, Value = otherColor},
                    new Setter { Property = Button.FontSizeProperty, Value = normalFontSize},
                    new Setter { Property = Button.BackgroundColorProperty, Value = mainColor}
                }
            };

            var pageStyle = new Style(typeof(ContentPage))
            {
                Setters =
                {
                    new Setter { Property = ContentPage.BackgroundColorProperty, Value = otherColor }
                }
            };

            var stackStyle = new Style(typeof(ContentPage))
            {
                Setters =
                {
                    new Setter { Property = StackLayout.VerticalOptionsProperty, Value = LayoutOptions.FillAndExpand },
                    new Setter { Property = StackLayout.MarginProperty, Value = new Thickness(10) }
                }
            };

            var lblStyle = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter { Property = Label.FontSizeProperty, Value = normalFontSize },
                    new Setter { Property = Label.TextColorProperty, Value = mainColor }
                }
            };

            var bitStyle = new Style(typeof(Switch))
            {
                Setters =
                {
                    new Setter { Property = Switch.BackgroundColorProperty, Value = mainColor }
                }
            };

            var txtStyle = new Style(typeof(Entry))
            {
                Setters =
                {
                    new Setter { Property = Entry.FontSizeProperty, Value = normalFontSize },
                    new Setter { Property = Entry.PlaceholderColorProperty, Value = mainColor },
                    new Setter { Property = Entry.TextColorProperty, Value = mainColor },
                    new Setter { Property = Entry.BackgroundColorProperty, Value = otherColor }
                }
            };

            var lstStyle = new Style(typeof(ListView))
            {
                Setters =
                {
                    new Setter { Property = ListView.BackgroundColorProperty, Value = otherColor },
                    new Setter { Property = ListView.SeparatorColorProperty, Value = mainColor }
                }
            };
            var pckStyle = new Style(typeof(Picker))
            {
                Setters =
                {
                    new Setter { Property = Picker.BackgroundColorProperty, Value = otherColor },
                    new Setter { Property = Picker.TextColorProperty, Value = mainColor }
                }
            };

            Resources.Add(nameof(btnStyle), btnStyle);
            Resources.Add(nameof(pageStyle), pageStyle);
            Resources.Add(nameof(stackStyle), stackStyle);
            Resources.Add(nameof(lblStyle), lblStyle);
            Resources.Add(nameof(txtStyle), txtStyle);
            Resources.Add(nameof(bitStyle), bitStyle);
            Resources.Add(nameof(lstStyle), lstStyle);
            Resources.Add(nameof(pckStyle), pckStyle);
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
