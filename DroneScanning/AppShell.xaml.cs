using DroneScanning.Guards;
using DroneScanning.View.Layout;

namespace DroneScanning
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            Guardian guardian = new Guardian();

            if (guardian.CanAccessPage())
            {
                await Navigation.PushAsync(new MasterLayout());
            }
            else { 
                await Navigation.PushAsync(new LoginLayout());
            }
        }
    }
}
