using Android.App;
using Android.Content.Res;
using Android.Util;
using Android.Views;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using static DroneScanning.MainActivity;

namespace DroneScanning.Platforms.Android
{
    public class AndroidWindow
    {
        public void SetWindowScale(double scale)
        {
            var activity = FormsApplicationActivity.Instance;
            if (activity != null)
            {
                activity.RunOnUiThread(() =>
                {
                    // Obtener el tamaño de la pantalla
                    var metrics = new DisplayMetrics();
                    activity.WindowManager.DefaultDisplay.GetMetrics(metrics);
                    int screenWidth = metrics.WidthPixels;
                    int screenHeight = metrics.HeightPixels;

                    int newWidth = (int)(screenWidth * scale);

                    // Aplicar la transformación de escala a la actividad principal
                    activity.Window.SetLayout(newWidth, screenHeight);
                });
            }
        }
    }
}
