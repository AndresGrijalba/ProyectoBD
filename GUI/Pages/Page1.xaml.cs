using BLL;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Lógica de interacción para Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public List<Cliente> cliente = null;

        ClienteBLL servicio = new ClienteBLL();

        public Clientes()
        {
            InitializeComponent();
            DataContext = this;
            cliente = servicio.ObtenerClientes();
            ClientesDataGrid.ItemsSource = cliente;
        }

        private void RegistrarCliente_Click(object sender, EventArgs e)
        {
            Window clientesWindow = Window.GetWindow(this);
            AgregarCWindow cliWindow = new AgregarCWindow();

            cliWindow.Owner = clientesWindow;
            cliWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            cliWindow.ShowDialog();

            Clientes updateCliente = new Clientes();
            this.NavigationService.Navigate(updateCliente);
        }

        private void EditarCliente_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string cedula = button.Tag as string;
            EditarCliente editarClientePage = new EditarCliente(cedula);
            this.NavigationService.Navigate(editarClientePage);
        }
    }
}
