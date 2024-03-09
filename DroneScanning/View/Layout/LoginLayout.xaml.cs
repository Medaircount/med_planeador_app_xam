using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using DroneScanning.Models;
using DroneScanning.Services;
using DroneScanning.Interface;


namespace DroneScanning.View.Layout;

public partial class LoginLayout : ContentPage
{
    readonly ILoginRepository loginRepository = new LoginService();
    public LoginLayout()
	{
		InitializeComponent();
	}
    public async void SignIn(object sender, EventArgs e)
    {
        string un = userName.Text;
        string pw = password.Text;

        try
        {
            // Validar si el campo est� vac�o o no
            if (string.IsNullOrWhiteSpace(un) && string.IsNullOrWhiteSpace(pw))
            {
                // Mostrar mensaje de error si el campo est� vac�o
                DisplayAlert("Error", "Por favor, diligencia correctamente el usuario y la contrase�a", "Aceptar");
            }
            else
            {
                User userInfo = await loginRepository.Login(un, pw);

                string us = Preferences.Get("userStorage", string.Empty);
                if (!String.IsNullOrEmpty(us))
                {
                    Preferences.Remove("userStorage");
                }
                //Nuevamente almacenamos en memoria
                string userDetails = JsonConvert.SerializeObject(userInfo);
                Preferences.Set("userStorage", userDetails);
                Preferences.Set("userId", userInfo.UserId);


                App.user = userInfo;

                if (!String.IsNullOrEmpty(App.user.UserId))
                {
                    //Ahora le decimos que vamos a Master
                    //await Shell.Current.GoToAsync("//HomePage");
                    await Navigation.PushAsync(new MasterLayout());
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message.ToString());
        }
    }
}