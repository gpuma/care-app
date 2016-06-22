using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using FAB.Forms;

namespace CareApp.Views
{
    public class Caca : ContentPage
    {
        private FloatingActionButton normalFab;

        public Caca()
        {
            this.Title = "C# Example";

            var layout = new RelativeLayout();

            var green = new Button()
            {
                Text = "White",
                Command = new Command(() => { this.UpdateButtonColor(Color.White); })
            };

            var red = new Button()
            {
                Text = "Red",
                Command = new Command(() => { this.UpdateButtonColor(Color.Red); })
            };

            var blue = new Button()
            {
                Text = "Blue",
                Command = new Command(() => { this.UpdateButtonColor(Color.Blue); })
            };

            Button disable = null;
            disable = new Button()
            {
                Text = "Disabled",
                Command = new Command(() =>
                {
                    this.normalFab.IsEnabled = !this.normalFab.IsEnabled;
                })
            };

            layout.Children.Add(
                new StackLayout
                {
                    Padding = new Thickness(15),
                    Children =
                    {
                        green,
                        red,
                        blue,
                        disable
                    }
                },
                xConstraint: Constraint.Constant(0),
                yConstraint: Constraint.Constant(0),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height)
            );

            normalFab = new FloatingActionButton();
            normalFab.Source = "plus.png";
            normalFab.Size = FabSize.Normal;

            layout.Children.Add(
                normalFab,
                xConstraint: Constraint.RelativeToParent((parent) => { return (parent.Width - normalFab.Width) - 16; }),
                yConstraint: Constraint.RelativeToParent((parent) => { return (parent.Height - normalFab.Height) - 16; })
            );

            normalFab.SizeChanged += (sender, args) => { layout.ForceLayout(); };

            normalFab.Clicked += (sender, e) =>
            {
                this.DisplayAlert("Floating Action Button", "You clicked the normal FAB!", "Awesome!");
            };

            this.Content = layout;
        }

        private void UpdateButtonColor(Color color)
        {
            var normal = color;
            var disabled = color.MultiplyAlpha(0.25);
            var pressed = color.MultiplyAlpha(0.8);

            normalFab.NormalColor = normal;
            normalFab.DisabledColor = disabled;
            normalFab.PressedColor = pressed;
        }
    }
}
