using DroneScanning.Models;
using DroneScanning.View.Layout;

namespace DroneScanning
{
    public partial class App : Application
    {
        public static User user;
        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();
            MainPage = new MasterLayout();
        }
    }
}
