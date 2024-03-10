using DroneScanning.Guards;
using DroneScanning.Interface;
using DroneScanning.Models;
using DroneScanning.Services;
using System.Collections.ObjectModel;

namespace DroneScanning.View.Pages;

public partial class HomePage : ContentPage
{
    public ObservableCollection<Record> Registros { get; } = new ObservableCollection<Record>();
    readonly ILogistics logistics = new LogisticsService();
    public HomePage()
	{
		InitializeComponent();
        BindingContext = this;
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

    public async void AddRecord(object sender, EventArgs e)
    {
        string cr = !String.IsNullOrEmpty(codeRecord.Text) ? codeRecord.Text : ""; ;

        try
        {
            // Validar si el campo está vacío o no
            if (string.IsNullOrWhiteSpace(cr))
            {
                // Mostrar mensaje de error si el campo está vacío
                DisplayAlert("Error", "Por favor, ingresar serial", "Aceptar");
            }
            else
            {
                string nuevoGuid = Guid.NewGuid().ToString();


                string userid = Preferences.Get("userId", string.Empty);
                #region tmp
                if (String.IsNullOrEmpty(userid)) {
                    // Con el fin de hacer pruebas
                    userid = "6cba0e76-6f8e-4d68-b9f2-af14c2b2879b";
                }
                #endregion

                Record record = await logistics.Register(userid, cr, nuevoGuid);

                // Agregar el nuevo registro a la colección
                Registros.Add(new Record { UserId = record.UserId, RecordName= record.RecordName, RecordId = record.RecordId });

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