using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Xamarin.Essentials;
using DroneScanning.Interface;
using Android.Graphics;
using Java.Util;
using Microsoft.Maui.Platform;
using Android.Content.Res;
using DroneScanning;


[assembly: Dependency(typeof(FloatingWindowService))]

namespace DroneScanning
{
    public class FloatingWindowService : IFloatingWindowService
    {
        private WindowManagerLayoutParams layoutParams;
        private Android.Views.View floatingView;
        private IWindowManager windowManager;

        public void ShowFloatingWindow()
        {
            if (floatingView == null)
            {
                var activity = Xamarin.Essentials.Platform.CurrentActivity;

                windowManager = activity.GetSystemService(Context.WindowService) as IWindowManager;

                // Crear el diseño de la ventana flotante
                layoutParams = new WindowManagerLayoutParams(
                    ViewGroup.LayoutParams.WrapContent,
                    ViewGroup.LayoutParams.WrapContent,
                    WindowManagerTypes.ApplicationOverlay,
                    WindowManagerFlags.NotFocusable,
                    Format.Translucent)
                {
                    Gravity = GravityFlags.Top | GravityFlags.Left,
                    X = 0,
                    Y = 0
                };

                // Inflar el diseño de la ventana flotante
                floatingView = LayoutInflater.From(activity).Inflate(Microsoft.Maui.Resource.Layout.floating_window_layout, null);

                // Agregar la vista flotante a la ventana del sistema
                windowManager.AddView(floatingView, layoutParams);
            }
        }

        public void HideFloatingWindow()
        {
            if (floatingView != null)
            {
                // Eliminar la vista flotante de la ventana del sistema
                windowManager.RemoveViewImmediate(floatingView);
                floatingView = null;
            }
        }
    }
}
