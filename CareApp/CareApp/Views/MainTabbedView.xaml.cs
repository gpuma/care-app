using Xamarin.Forms;

namespace CareApp.Views
{
    public partial class MainTabbedView : TabbedPage
    {
        public MainTabbedView()
        {
            InitializeComponent();
            this.Children.Add(new BeaconsView { Title = "Beacons" });
        }
    }
}
