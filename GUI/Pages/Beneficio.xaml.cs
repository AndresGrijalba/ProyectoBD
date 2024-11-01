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
    /// Lógica de interacción para Factura.xaml
    /// </summary>
    public partial class Beneficio : Page
    {
        private TipoAyudaBLL tipoAyudaBLL = new TipoAyudaBLL();

        public Beneficio()
        {
            InitializeComponent();
            CargarBeneficios();
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var nuevoBeneficio = new TipoAyuda
                {
                    Nombre = txtNombre.Text,
                    // No es necesario establecer CantidadBeneficiados aquí
                };

                tipoAyudaBLL.GuardarBeneficio(nuevoBeneficio);

                MessageBox.Show("Beneficio guardado exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el beneficio: {ex.Message}");
            }
        }

        private void CargarBeneficios()
        {
            //// Limpiar el ListBox antes de cargar los nuevos datos
            //lbBeneficios.Items.Clear();

            //// Obtener los beneficios desde la BLL
            //var beneficios = tipoAyudaBLL.ObtenerBeneficios();

            //// Agregar cada beneficio al ListBox
            //foreach (var beneficio in beneficios)
            //{
            //    lbBeneficios.Items.Add($"{beneficio.Codigo} - {beneficio.Nombre} (Beneficiarios: {beneficio.CantidadBeneficiados})");
            //}
        }

    }
}
