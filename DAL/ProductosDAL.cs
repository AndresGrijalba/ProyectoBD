using Entity;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ProductosDAL
    {
        public void agregarProductos(Productos productos)
        {
            using (OracleConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "INSERT INTO productos (Id, Descripcion, PrecioUnitario, Impuesto) " +
                               "VALUES (:Id, :Descripcion, :PrecioUnitario, :Impuesto)";

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter(":nombres", productos.Id));
                    command.Parameters.Add(new OracleParameter(":apellidos", productos.Descripcion));
                    command.Parameters.Add(new OracleParameter(":cedula", productos.PrecioUnitario));
                    command.Parameters.Add(new OracleParameter(":correo", productos.Impuesto));
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
