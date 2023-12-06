using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sistemas_de_Calificacion_baez
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            // Establecer el título de la página con formato HTML
            this.Title = "Sistema de Calificación Universitaria";

        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            // Validar que los campos no estén vacíos y sean números válidos
            if (string.IsNullOrWhiteSpace(AsistenciaEntry.Text) ||
                string.IsNullOrWhiteSpace(TrabajoPracticoEntry.Text) ||
                string.IsNullOrWhiteSpace(ExamenParcialEntry.Text) ||
                string.IsNullOrWhiteSpace(ExamenFinalEntry.Text))
            {
                // Mostrar un mensaje de error si algún campo está vacío
                await DisplayAlert("Error", "Por favor, ingrese todas las notas.", "Aceptar");
                return; // Salir del evento para evitar cálculos incorrectos
            }

            // Convertir los valores ingresados en cada campo de entrada a números (asistencia, trabajoPractico, examenParcial y examenFinal)
            if (!double.TryParse(AsistenciaEntry.Text, out double asistencia) ||
                !double.TryParse(TrabajoPracticoEntry.Text, out double trabajoPractico) ||
                !double.TryParse(ExamenParcialEntry.Text, out double examenParcial) ||
                !double.TryParse(ExamenFinalEntry.Text, out double examenFinal))
            {
                // Mostrar un mensaje de error si algún campo no es un número válido
                await DisplayAlert("Error", "Por favor, ingrese números válidos en todas las notas.", "Aceptar");
                return; // Salir del evento para evitar cálculos incorrectos
            }

            // Validar que las notas no sean negativas
            if (asistencia < 0 || trabajoPractico < 0 || examenParcial < 0 || examenFinal < 0)
            {
                // Mostrar un mensaje de error si alguna nota es negativa
                await DisplayAlert("Error", "Por favor, ingrese notas válidas (no negativas).", "Aceptar");
                return; // Salir del evento para evitar cálculos incorrectos
            }
            // Validar que las notas no sean mayores que los máximos permitidos
            const double notaMaximaAsistencia = 10;
            const double notaMaximaTrabajoPractico = 20;
            const double notaMaximaExamenParcial = 20;
            const double notaMaximaExamenFinal = 50;

            if (asistencia > notaMaximaAsistencia || trabajoPractico > notaMaximaTrabajoPractico ||
                examenParcial > notaMaximaExamenParcial || examenFinal > notaMaximaExamenFinal)
            {
                // Mostrar un mensaje de error si alguna nota es mayor que el límite máximo
                await DisplayAlert("Error", "Por favor, ingrese notas válidas (no mayores que los máximos permitidos).", "Aceptar");
                return; // Salir del evento para evitar cálculos incorrectos
            }

            // Calcular la nota final sumando las notas de asistencia, trabajoPractico, examenParcial y examenFinal
            double notaFinal = asistencia + trabajoPractico + examenParcial + examenFinal;

            // Asegurar que cada nota no sea mayor que su nota máxima permitida
            if (asistencia > notaMaximaAsistencia)
                asistencia = notaMaximaAsistencia;
            if (trabajoPractico > notaMaximaTrabajoPractico)
                trabajoPractico = notaMaximaTrabajoPractico;
            if (examenParcial > notaMaximaExamenParcial)
                examenParcial = notaMaximaExamenParcial;
            if (examenFinal > notaMaximaExamenFinal)
                examenFinal = notaMaximaExamenFinal;

            // Recalcular la nota final después de asegurarse de que cada nota esté dentro del rango permitido
            notaFinal = asistencia + trabajoPractico + examenParcial + examenFinal;

            // Asegurar que la nota final no sea mayor que 100
            if (notaFinal > 100)
                notaFinal = 100;

            // Asignar la nota final a la etiqueta correspondiente para mostrarla en la interfaz gráfica
            NotaFinalLabel.Text = notaFinal.ToString();

            // Asignar la equivalencia en letra a la etiqueta correspondiente basándose en la nota final
            if (notaFinal >= 90)
                EquivalenciaLabel.Text = "A";
            else if (notaFinal >= 80)
                EquivalenciaLabel.Text = "B";
            else if (notaFinal >= 75)
                EquivalenciaLabel.Text = "C";
            else if (notaFinal >= 70)
                EquivalenciaLabel.Text = "D";
            else if (notaFinal >= 60)
                EquivalenciaLabel.Text = "FE";
            else if (notaFinal >= 50)
                EquivalenciaLabel.Text = "FI";
            else
                EquivalenciaLabel.Text = "F";

            // Nuevo Label para mostrar "APROBADO" o "DESAPROBADO" y cambiar el color de fondo
            resultadoLabel.FontSize = 24;
            resultadoLabel.HorizontalOptions = LayoutOptions.CenterAndExpand;

            if (notaFinal >= 70)
            {
                resultadoLabel.Text = "🆗 APROBADO";
                resultadoLabel.TextColor = Color.Green;
            }
            else
            {
                resultadoLabel.Text = "⛔ REPROBADO";
                resultadoLabel.TextColor = Color.Red;
            }
        }

        private async void TestConnection_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Establecemos la Cadena de Conexion con el SQL Server y Nuestros datos:
                string connectionString = "Server=10.0.0.7,1433;Database=NOTAS;User Id=JUANCITO; Password=123456;";

                // Crear una conexión a la base de datos
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    // Si la conexión se abre con éxito, mostramos un mensaje
                    await DisplayAlert("🌐 Conexión Exitosa", "👍 La conexión a la base de datos se ha establecido correctamente.", "Aceptar");
                }
            }
            catch (Exception ex)
            {
                // Si hay algún error, mostramos un mensaje de error
                await DisplayAlert("Error de Conexión", "No se pudo establecer la conexión a la base de datos. Error: " + ex.Message, "Aceptar");
            }


        }

        private async void Guardanota_Clicked(object sender, EventArgs e)
        {
            // Realiza los cálculos y la validación de entrada como en tu código original

            try
            {
                // Establecemos la Cadena de Conexion con el SQL Server y Nuestros datos:
                string connectionString = "Server=xxx xxx xxx,xxx xxx ;Database=xxxx;User Id=xxxx; Password= xxxxx;";

                // Crear una conexión a la base de datos
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    // Crear una consulta SQL de inserción
                    string insertQuery = "INSERT INTO Calificaciones (Profesor, Matricula, Nombre, Asistencia, TrabajoPractico, ExamenParcial, ExamenFinal, NotaFinal) " +
                                         "VALUES (@Profesor, @Matricula, @Nombre, @Asistencia, @TrabajoPractico, @ExamenParcial, @ExamenFinal, @NotaFinal)";

                    // Crear un comando SQL
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        // Asignar valores a los parámetros de la consulta (como en tu código original)

                        command.Parameters.AddWithValue("@Profesor", ProfesorEntry.Text);
                        command.Parameters.AddWithValue("@Matricula", MatriculaEntry.Text);
                        command.Parameters.AddWithValue("@Nombre", NombreEntry.Text);
                        command.Parameters.AddWithValue("@Asistencia", AsistenciaEntry.Text);
                        command.Parameters.AddWithValue("@TrabajoPractico", TrabajoPracticoEntry.Text);
                        command.Parameters.AddWithValue("@ExamenParcial", ExamenParcialEntry.Text);
                        command.Parameters.AddWithValue("@ExamenFinal", ExamenFinalEntry.Text);
                        command.Parameters.AddWithValue("@NotaFinal", NotaFinalLabel.Text);

                        // Ejecutar la consulta SQL de inserción
                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            // Inserción exitosa
                            await DisplayAlert("Éxito", "Datos guardados en la base de datos.", "Aceptar");
                        }
                        else
                        {
                            // Error al guardar los datos
                            await DisplayAlert("Error", "No se pudieron guardar los datos en la base de datos.", "Aceptar");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Error de conexión o inserción
                await DisplayAlert("Error", "Error al conectar con la base de datos o al guardar los datos. Detalles: " + ex.Message, "Aceptar");
            }

        }

        private async void ReporteButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Reporte());

        }
    }
}
    


