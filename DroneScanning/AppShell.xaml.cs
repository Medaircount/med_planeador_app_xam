using CommunityToolkit.Mvvm.Input;
using DroneScanning.Guards;
using DroneScanning.View.Layout;
using DroneScanning.View.Pages;

namespace DroneScanning
{
    public partial class AppShell : Shell
    {
        Guardian guardian = new Guardian();
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("HomePages", typeof(HomePage));
            Routing.RegisterRoute("LoginLayout", typeof(LoginLayout));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Desactivar TabBarIsVisible antes de navegar a HomePage
            Shell.SetTabBarIsVisible(this, false);
        }

        [RelayCommand]
        async void SignOut()
        {
            try
            {
                guardian.Clean();
                //Lo redirigimos al login
                await Shell.Current.GoToAsync("//LoginLayout");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }

        protected override void OnDisappearing() { 
            base.OnDisappearing();

            guardian.Clean();
        }
    }
}
