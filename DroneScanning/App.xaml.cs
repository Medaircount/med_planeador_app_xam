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
            Routing.RegisterRoute("LoginPage", typeof(LoginPage));

            Guardian guardian = new Guardian(); 

            if (guardian.CanAccessPage())
            {
                //await Navigation.PushAsync(new MasterLayout());
                //Shell.Current.GoToAsync("//MasterLayout");
                MainPage.Navigation.PushAsync(new HomePage());
            }
            else
            {
                //Shell.Current.GoToAsync("//LoginPage");
                //await Navigation.PushAsync(new LoginPage());
                MainPage.Navigation.PushAsync(new LoginPage());
            }
        }
    }
}
