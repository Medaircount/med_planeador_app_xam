using DroneScanning.Guards;
using DroneScanning.Interface;
using DroneScanning.Models;
using DroneScanning.Services;
using DroneScanning.Platforms.Android;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace DroneScanning.View.Pages;

public partial class HomePage : ContentPage
{
    public ObservableCollection<Record> Registros { get; } = new ObservableCollection<Record>();
    TimeSpan scanningDelay = TimeSpan.FromMilliseconds(300);
    readonly ILogistics logistics = new LogisticsService();
    private bool isMinimized = false;

    private readonly IBluetoothManager _bluetoothManager;
    private readonly IFloatingWindowService floatingWindowService;
    public HomePage()
    {
        InitializeComponent();
        BindingContext = this;
        floatingWindowService = DependencyService.Get<IFloatingWindowService>();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        Shell.SetTabBarIsVisible(this, true);
        try
        {
            List<Record> records = await logistics.GetRecords();

            foreach (var rc in records)
            {
                Registros.Add(new Record { UserId = rc.UserId, RecordName = rc.RecordName, RecordId = rc.RecordId });
            }

        }
        catch (Exception e)
        {

            Console.WriteLine(e.Message.ToString());
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        Guardian guardian = new Guardian();
        guardian.Clean();
    }

    private void OnMinimizeClicked(object sender, EventArgs e)
    {
        // Obtener el tamaño de la pantalla
        try
        {
            isMinimized = !isMinimized;
            var androidWindow = new AndroidWindow();

            if (isMinimized)
            {
                // Minimiza
                androidWindow.SetWindowScale(0.5);
                miniActionButton.Text = "Normalizar";
            }
            else
            {
                // Restaurar el tamaño original
                androidWindow.SetWindowScale(1.0);
                miniActionButton.Text = "Minimizar";
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
        }
    }

    public async void AddRecordText(object sender, TextChangedEventArgs e) {
        await Task.Delay(scanningDelay);

        // Guarda solo si viene del scaner 
        if (e.NewTextValue != codeRecord.Text && !String.IsNullOrEmpty(codeRecord.Text))
        {
            string cr = codeRecord.Text;
            // Lo limpiamos por seguridad
            codeRecord.Text = string.Empty;
            AddRecord(cr);
        }
    }

    public void AddRecordButton(object sender, EventArgs e)
    {
        string parameter = (sender as Button).CommandParameter as string;
        string cr = codeRecord.Text;
        AddRecord(cr, parameter);
    }
    
    public async void AddRecord(string text, string? parameter = "") 
    {
        try
        {
            string cr = text;
            // Verificar si es novedad
            if (!String.IsNullOrEmpty(parameter))
            {
                cr = parameter == "1" ? parameter : cr;
            }

            if (string.IsNullOrWhiteSpace(cr))
            {
                // Mostrar mensaje de error si el campo está vacío
                DisplayAlert("Error", "Por favor, ingresar serial", "Aceptar");
            }
            else
            {

                string nuevoGuid = Guid.NewGuid().ToString();
                string now = DateTime.Now.ToString("yyyyMMddTHHmmss");
                string userid = Preferences.Get("userId", string.Empty);

                Record record = await logistics.Register(userid, cr, nuevoGuid, now);

                // Agregar el nuevo registro a la colección
                Registros.Insert(0, new Record { UserId = record.UserId, RecordName = record.RecordName, RecordId = record.RecordId });
                // Nos aseguramos que solo se muestren los 5 primeros
                if (Registros.Count()>4) { 
                    Record record1 = Registros.Last();
                    Registros.Remove(record1);
                }
                // Limpiar los campos de entrada después de agregar el registro
                codeRecord.Text = string.Empty;
            }          
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message.ToString());
        }
    }
}