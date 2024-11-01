using BLL;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.Pages
{
    /// <summary>
    /// Lógica de interacción para AgregarBeneficiario.xaml
    /// </summary>
    public partial class AgregarBeneficiario : Page
    {
        private EstudianteBLL estudianteBLL = new EstudianteBLL();
        private TipoAyudaBLL tipoAyudaBLL = new TipoAyudaBLL();

        public AgregarBeneficiario()
        {
            InitializeComponent();
            CargarTipoAyudas();
        }

        // Cargar tipos de ayuda desde el archivo TipoAyuda.txt
        private void CargarTipoAyudas()
        {
            var tiposAyuda = tipoAyudaBLL.ObtenerBeneficios();
            cmbTipoAyuda.ItemsSource = tiposAyuda;
            cmbTipoAyuda.DisplayMemberPath = "Nombre";
            cmbTipoAyuda.SelectedValuePath = "Codigo";
        }

        // Buscar estudiante por identificación
        private void BuscarEstudiante_Click(object sender, RoutedEventArgs e)
        {
            var id = txtIdentificacion.Text.Trim();

            // Busca el estudiante en la BLL
            var estudiante = estudianteBLL.ObtenerEstudiantePorId(id);

            if (estudiante == null)
            {
                MessageBox.Show("Identificación no encontrada. Por favor, intente nuevamente.");
                cmbTipoAyuda.IsEnabled = false;
                btnRegistrar.IsEnabled = false;
                lblNombreEstudiante.Content = ""; // Limpiar el nombre
            }
            else
            {
                // Habilitar los controles si se encuentra el estudiante
                lblNombreEstudiante.Content = estudiante.Nombre; // Mostrar el nombre
                cmbTipoAyuda.IsEnabled = true;
                btnRegistrar.IsEnabled = false; // Deshabilitar hasta que se seleccione una ayuda
            }
        }

        // Habilitar el botón de registro si se selecciona un tipo de ayuda
        private void CmbTipoAyuda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnRegistrar.IsEnabled = cmbTipoAyuda.SelectedItem != null;
        }

        // Registrar la ayuda
        private void RegistrarAyuda_Click(object sender, RoutedEventArgs e)
        {
            var idEstudiante = txtIdentificacion.Text.Trim();
            var tipoAyudaSeleccionada = cmbTipoAyuda.SelectedItem as TipoAyuda;

            if (tipoAyudaSeleccionada == null)
            {
                MessageBox.Show("Por favor, seleccione un tipo de ayuda.");
                return;
            }

            // Actualizar el estado del estudiante
            var estudiante = estudianteBLL.ObtenerEstudiantePorId(idEstudiante);
            if (estudiante != null)
            {
                // Incrementar el número de beneficios recibidos
                estudiante.BeneficioRecibidos += 1; // Asumiendo que se quiere sumar uno más
                estudianteBLL.ActualizarEstudiante(estudiante); // Actualizar el estado en la DAL

                // Actualizar el conteo de beneficiarios para el tipo de ayuda
                tipoAyudaBLL.ActualizarBeneficiarios(tipoAyudaSeleccionada.Codigo);

                MessageBox.Show("Ayuda registrada exitosamente.");
            }
            else
            {
                MessageBox.Show("Estudiante no encontrado al registrar la ayuda.");
            }

        }

    }
}
