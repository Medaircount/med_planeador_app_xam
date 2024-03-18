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
            Routing.RegisterRoute("LoginPage", typeof(LoginPage));
            Routing.RegisterRoute("HomePages", typeof(HomePage));

            Routing.RegisterRoute("Page 1", typeof(NewPage1));
            Routing.RegisterRoute("Page 2", typeof(NewPage2));
            Routing.RegisterRoute("Page 3", typeof(NewPage3));

            AddFlyoutItem("Home", "home.png", typeof(HomePage));
            AddFlyoutItem("Page 1", "setting.svg", typeof(NewPage1));
            AddFlyoutItem("Page 2", "setting.svg", typeof(NewPage1));
            AddFlyoutItem("Page 3", "setting.svg", typeof(NewPage1));
        }

        void AddFlyoutItem(string title, string icon, Type pageType)
        {
            var shellContent = new ShellContent
            {
                Title = title,
                Content = (Page)Activator.CreateInstance(pageType)
            };

            Items.Add(shellContent);
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }

        //[RelayCommand]
        public async void SignOut(object sender, EventArgs e)
        {
            try
            {
                guardian.Clean();
                //Lo redirigimos al login
                //await Shell.Current.GoToAsync("//LoginPage");
                await Navigation.PushAsync(new LoginPage());
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
        }

        protected override void OnDisappearing() { 
            base.OnDisappearing();

            guardian.Clean();
        }
    }
}
