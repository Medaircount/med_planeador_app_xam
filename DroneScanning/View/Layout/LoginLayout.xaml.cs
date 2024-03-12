using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using DroneScanning.Models;
using DroneScanning.Services;
using DroneScanning.Interface;
using DroneScanning.View.Pages;


namespace DroneScanning.View.Layout;

public partial class LoginLayout : ContentPage
{
    readonly ILoginRepository loginRepository = new LoginService();
    public LoginLayout()
	{
		InitializeComponent();
	}

    private void FrameTapped(object sender, EventArgs e)
    {
        if (sender is Frame tappedFrame)
        {
            var entry = tappedFrame.BindingContext as Entry;
            entry?.Focus();
        }
    }
    private void ImageTapped(object sender, EventArgs e)
    {
        password.Focus();
    }

    public async void SignIn(object sender, EventArgs e)
    {
        string un = userName.Text;
        string pw = password.Text;

        try
        {
            // Validar si el campo está vacío o no
            if (string.IsNullOrWhiteSpace(un) && string.IsNullOrWhiteSpace(pw))
            {
                // Mostrar mensaje de error si el campo está vacío
                //DisplayAlert("Error", "Por favor, diligencia correctamente el usuario y la contraseña", "Aceptar");
                await GlobalService.SweetAlert("Error", "Por favor, diligencia correctamente el usuario y la contraseña");
            }
            else
            {
                User userInfo = await loginRepository.Login(un, pw);
                if (userInfo != null) { 
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
                        await Navigation.PushAsync(new HomePage());
                    }
                }
                else
                {
                    userName.Text = "";
                    password.Text = "";
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message.ToString());
        }
    }
}