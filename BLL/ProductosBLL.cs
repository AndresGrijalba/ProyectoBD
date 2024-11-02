using DAL;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ProductosBLL
    {
        private ProductosDAL clienteDAL = new ProductosDAL();

        public void AgregarProducto(Guid id, string Descripcion, int Cantidad, double PrecioUnitario, double Impuesto)
        {

            ValidarCampoObligatorio(Descripcion, "Descripcion");
            ValidarCampoObligatorio(Cantidad, "Cantidad");
            ValidarCampoObligatorio(PrecioUnitario, "PrecioUnitario");
            ValidarCampoObligatorio(Impuesto, "Impuesto");
           

            if (ProductosDal.IdExiste(cedula))
            {
                throw new ArgumentException("El producto ya está registrado ya está registrada.");
            }

            Productos productos = new Productos
            {
                id = id,
                Descripcion = Descripcion,
                Cantidad = Cantidad,
                PrecioUnitario = PrecioUnitario,
                Impuesto = Impuesto
            };

            ProductosDAL.agregarProductos(productos);

        }

    }
}
