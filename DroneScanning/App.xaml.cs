using DroneScanning.Guards;
using DroneScanning.Models;
using DroneScanning.View.Layout;
using DroneScanning.View.Pages;

namespace DroneScanning
{
    public partial class App : Application
    {
        public static User user;
        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            base.OnStart();

            Routing.RegisterRoute("HomePage", typeof(HomePage));
            Routing.RegisterRoute("LoginLayout", typeof(LoginLayout));

            Guardian guardian = new Guardian(); 

            if (guardian.CanAccessPage())
            {
                //await Navigation.PushAsync(new MasterLayout());
                //Shell.Current.GoToAsync("//MasterLayout");
                MainPage.Navigation.PushAsync(new HomePage());
            }
            else
            {
                //Shell.Current.GoToAsync("//LoginLayout");
                //await Navigation.PushAsync(new LoginLayout());
                MainPage.Navigation.PushAsync(new LoginLayout());
            }
        }
    }
}
