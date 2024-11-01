using Entity;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

namespace DAL
{
    public class ClienteDAL
    {
        public void agregarCliente(Cliente cliente)
        {
            using (OracleConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "INSERT INTO clientes (nombres, apellidos, cedula, correo, telefono) " +
                               "VALUES (:nombres, :apellidos, :cedula, :correo, :telefono)";

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter(":nombres", cliente.Nombre));
                    command.Parameters.Add(new OracleParameter(":apellidos", cliente.Apellido));
                    command.Parameters.Add(new OracleParameter(":cedula", cliente.Cedula));
                    command.Parameters.Add(new OracleParameter(":correo", cliente.Correo));
                    command.Parameters.Add(new OracleParameter(":telefono", cliente.Telefono));
                    command.ExecuteNonQuery();
                }
            }
        }

        public bool CedulaExiste(string cedula)
        {
            using (OracleConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT COUNT(1) FROM clientes WHERE cedula = :cedula";

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter(":cedula", cedula));
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public Cliente ObtenerClientePorCedula(string cedula)
        {
            using (OracleConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM clientes WHERE cedula = :cedula";

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter(":cedula", cedula));

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Cliente
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Cedula = reader["cedula"].ToString(),
                                Nombre = reader["nombres"].ToString(),
                                Apellido = reader["apellidos"].ToString(),
                                Correo = reader["correo"].ToString(),
                                Telefono = reader["telefono"].ToString()
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
        public bool ActualizarCliente(Cliente cliente)
        {
            using (OracleConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "UPDATE clientes SET nombres = :nombres, apellidos = :apellidos, correo = :correo, telefono = :telefono WHERE cedula = :cedula";

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter(":cedula", cliente.Cedula));
                    command.Parameters.Add(new OracleParameter(":nombres", cliente.Nombre));
                    command.Parameters.Add(new OracleParameter(":apellidos", cliente.Apellido));
                    command.Parameters.Add(new OracleParameter(":correo", cliente.Correo));
                    command.Parameters.Add(new OracleParameter(":telefono", cliente.Telefono));

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public bool EliminarCliente(string cedula)
        {
            using (OracleConnection connection = DatabaseConnection.GetConnection())
            {
                string query = "DELETE FROM clientes WHERE cedula = :cedula";

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter(":cedula", cedula));
                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al eliminar el cliente: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        public List<Cliente> ObtenerClientes()
        {
            List<Cliente> clientes = new List<Cliente>();

            using (OracleConnection connection = DatabaseConnection.GetConnection())
            {
                string query = "SELECT * FROM clientes";

                connection.Open();
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cliente cliente = new Cliente
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Nombre = reader["nombres"].ToString(),
                                Apellido = reader["apellidos"].ToString(),
                                Cedula = reader["cedula"].ToString(),
                                Correo = reader["correo"].ToString(),
                                Telefono = reader["telefono"].ToString()
                            };

                            clientes.Add(cliente);
                        }
                    }
                }
            }
            return clientes;
        }

        public List<Cliente> BuscarClientesPorCedula(string cedula)
        {
            List<Cliente> clientes = new List<Cliente>();

            using (OracleConnection connection = DatabaseConnection.GetConnection())
            {
                string query = "SELECT * FROM clientes WHERE cedula LIKE '%' || :cedula || '%'";

                connection.Open();
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter(":cedula", cedula));

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cliente cliente = new Cliente
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Nombre = reader["nombres"].ToString(),
                                Apellido = reader["apellidos"].ToString(),
                                Cedula = reader["cedula"].ToString(),
                                Correo = reader["correo"].ToString(),
                                Telefono = reader["telefono"].ToString()
                            };

                            clientes.Add(cliente);
                        }
                    }
                }
            }
            return clientes;
        }
    }
}
