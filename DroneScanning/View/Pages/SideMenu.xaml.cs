using CommunityToolkit.Mvvm.Input;
using DroneScanning.Guards;
using DroneScanning.View.Layout;

namespace DroneScanning.View.Pages;

public partial class SideMenu : ContentPage
{
	public SideMenu()
	{
		InitializeComponent();
	}

    async void SignOut(object sender, EventArgs e)
    {
        try
        {
            Guardian guardian = new Guardian();
            guardian.Clean();
            //Lo redirigimos al login
            await Navigation.PushAsync(new LoginLayout());
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex);
        }
    }
}