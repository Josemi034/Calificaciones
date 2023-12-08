using Sistemas_de_Calificacion_baez;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace Sistema_de_Calificacicion_baez
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Reporte : ContentPage
	{
        private List<Calificaciones> calificaciones;    
        public Reporte ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Mostrar el ActivityIndicator
            LoadingActivityIndicator.IsRunning = true;
            LoadingActivityIndicator.IsVisible = true;

            // Realizar una consulta a la base de datos para obtener las calificaciones
            calificaciones = await ObtenerCalificacionesDesdeBaseDeDatosAsync();

            // Establecer la lista de calificaciones como origen de datos del ListView
            NotasListView.ItemsSource = calificaciones;

            // Ocultar el ActivityIndicator
            LoadingActivityIndicator.IsRunning = false;
            LoadingActivityIndicator.IsVisible = false;
        }

        private async Task<List<Calificaciones>> ObtenerCalificacionesDesdeBaseDeDatosAsync()
        {
            try
            {
                string connectionString = "Server=10.0.0.9;1433;Database=NOTAS;User Id=josem; Password=josemiguelbaezmendez;";
                List<Calificaciones> calificaciones = new List<Calificaciones>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    string query = "SELECT * FROM Calificaciones";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Calificaciones calificacion = new Calificaciones 
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                Profesor = reader.GetString(reader.GetOrdinal("Profesor")),
                                Matricula = reader.GetString(reader.GetOrdinal("Matricula")),
                                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                Asistencia = reader.GetDecimal(reader.GetOrdinal("Asistencia")),
                                TrabajoPractico = reader.GetDecimal(reader.GetOrdinal("TrabajoPractico")),
                                ExamenParcial = reader.GetDecimal(reader.GetOrdinal("ExamenParcial")),
                                ExamenFinal = reader.GetDecimal(reader.GetOrdinal("ExamenFinal")),
                                NotaFinal = reader.GetDecimal(reader.GetOrdinal("NotaFinal"))
                            };
                            calificaciones.Add(calificacion);
                        }
                    }
                }

                return calificaciones;
            }
            catch (Exception ex)
            {
                // Manejar errores aquí
                await DisplayAlert("Error", "Error al obtener las calificaciones desde la base de datos: " + ex.Message, "Aceptar");
                return new List<Calificaciones>();
            }
        }



    }
}