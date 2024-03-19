using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using Application = Microsoft.Maui.Controls.Application;

namespace DroneScanning
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        public static MainActivity Instance { get; private set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            FormsApplicationActivity.Instance = this;
            //MauiProgram.CreateMauiApp();
        }

        //public void SetWindowScale(double scale, bool? back = false)
        //{
        //    // Obener el tamaño de la pantalla
        //    var metrics = Resources.DisplayMetrics;
        //    int screenWidth = metrics.WidthPixels;
        //    int screenHeight = metrics.HeightPixels;

        //    //Calcular el nuevo tamaño de la actividad(75 % de la pantalla mas)
        //    int newWidth = (int)(screenWidth * scale);

        //    // Aplicar transformación de escala a la actividad principal
        //    Window.SetLayout(newWidth, screenHeight);
        //}

        // Método para minimizar la aplicación cuando se llama

        public static class FormsApplicationActivity
        {
            public static MainActivity Instance { get; set; }
        }

        public void MinimizarApp()
        {
            MoveTaskToBack(true);
        }
    }
}
