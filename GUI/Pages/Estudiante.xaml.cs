using BLL;
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
    /// Lógica de interacción para Estudiante.xaml
    /// </summary>
    public partial class Estudiante : Page
    {
        private EstudianteBLL bll = new EstudianteBLL();

        public Estudiante()
        {
            InitializeComponent();
        }

        private void Consultar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool recibieronBeneficio = cbFiltro.SelectedIndex == 0;
                var (estudiantes, conteoRecibidos, conteoNoRecibidos) = bll.ObtenerEstudiantesFiltrados(recibieronBeneficio);

                lbEstudiantes.Items.Clear();
                foreach (var estudiante in estudiantes)
                {
                    lbEstudiantes.Items.Add($"{estudiante.Id} - {estudiante.Nombre}");
                }

                txtConteo.Text = $"Total con beneficio: {conteoRecibidos}, Total sin beneficio: {conteoNoRecibidos}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar estudiantes: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
