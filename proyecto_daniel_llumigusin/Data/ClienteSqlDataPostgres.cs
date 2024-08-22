using Npgsql;
using proyecto_javier.Models;
using System.Collections.Generic;
using System.Data;

namespace proyecto_javier.Data
{
    public class ClienteSqlDataPostgres
    {
        string connectionString = "Host=localhost;Port=5432;Database=dbproductos;Username=postgres;Password=admin";

        public IEnumerable<ClientePS> GetAllClientes()
        {
            List<ClientePS> lst = new List<ClientePS>();
            NpgsqlConnection con = null;
            NpgsqlCommand cmd = null;
            NpgsqlDataReader reader = null;

            try
            {
                con = new NpgsqlConnection(connectionString);
                cmd = new NpgsqlCommand("select * from Clientes", con);
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ClientePS cliente = new ClientePS
                    {
                        Codigo = reader["codigo"] != DBNull.Value ? Convert.ToInt32(reader["codigo"]) : 0,
                        Cedula = reader["cedula"] != DBNull.Value ? reader["cedula"].ToString() : string.Empty,
                        Apellidos = reader["apellidos"] != DBNull.Value ? reader["apellidos"].ToString() : string.Empty,
                        Nombres = reader["nombres"] != DBNull.Value ? reader["nombres"].ToString() : string.Empty,
                        FechaNacimiento = reader["fechanacimiento"] != DBNull.Value ? Convert.ToDateTime(reader["fechanacimiento"]) : DateTime.MinValue,
                        Mail = reader["mail"] != DBNull.Value ? reader["mail"].ToString() : string.Empty,
                        Telefono = reader["telefono"] != DBNull.Value ? reader["telefono"].ToString() : string.Empty,
                        Direccion = reader["direccion"] != DBNull.Value ? reader["direccion"].ToString() : string.Empty,
                        Estado = reader["estado"] != DBNull.Value ? Convert.ToBoolean(reader["estado"]) : false,
                        Saldo = reader["saldo"] != DBNull.Value ? Convert.ToDecimal(reader["saldo"]) : 0m
                    };
                    lst.Add(cliente);
                }
            }
            finally
            {
                if (reader != null) reader.Close();
                if (con != null) con.Close();
            }
            return lst;
        }

        public void AddCliente(ClientePS Cliente)
        {
            NpgsqlConnection con = null;
            NpgsqlCommand cmd = null;

            try
            {
                con = new NpgsqlConnection(connectionString);
                string query = "INSERT INTO Clientes (cedula, apellidos, nombres, fechanacimiento, mail, telefono, direccion, estado, saldo) " +
                               "VALUES (@cedula, @apellidos, @nombres, @fechanacimiento, @mail, @telefono, @direccion, @estado, @saldo)";
                cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.AddWithValue("@cedula", Cliente.Cedula);
                cmd.Parameters.AddWithValue("@apellidos", Cliente.Apellidos);
                cmd.Parameters.AddWithValue("@nombres", Cliente.Nombres);
                cmd.Parameters.AddWithValue("@fechanacimiento", Cliente.FechaNacimiento.Date);
                cmd.Parameters.AddWithValue("@mail", Cliente.Mail);
                cmd.Parameters.AddWithValue("@telefono", Cliente.Telefono);
                cmd.Parameters.AddWithValue("@direccion", Cliente.Direccion);
                cmd.Parameters.AddWithValue("@estado", Cliente.Estado);
                cmd.Parameters.AddWithValue("@saldo", Cliente.Saldo);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (con != null) con.Close();
            }
        }

        public void UpdateCliente(ClientePS Cliente)
        {
            NpgsqlConnection con = null;
            NpgsqlCommand cmd = null;

            try
            {
                string query = "UPDATE Clientes SET apellidos = @apellidos, nombres = @nombres, " +
                    "fechanacimiento = @fechanacimiento, mail = @mail, telefono = @telefono, " +
                    "direccion = @direccion, estado = @estado, saldo = @saldo, cedula = @cedula  " +
                    "WHERE codigo = @codigo;";
                con = new NpgsqlConnection(connectionString);
                cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.AddWithValue("@codigo", Cliente.Codigo);
                cmd.Parameters.AddWithValue("@cedula", Cliente.Cedula);
                cmd.Parameters.AddWithValue("@apellidos", Cliente.Apellidos);
                cmd.Parameters.AddWithValue("@nombres", Cliente.Nombres);
                cmd.Parameters.AddWithValue("@fechanacimiento", Cliente.FechaNacimiento);
                cmd.Parameters.AddWithValue("@mail", Cliente.Mail);
                cmd.Parameters.AddWithValue("@telefono", Cliente.Telefono);
                cmd.Parameters.AddWithValue("@direccion", Cliente.Direccion);
                cmd.Parameters.AddWithValue("@estado", Cliente.Estado);
                cmd.Parameters.AddWithValue("@saldo", Cliente.Saldo);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (con != null) con.Close();
            }
        }

        public ClientePS GetClienteData(int? codigo)
        {
            ClientePS Cliente = new ClientePS();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                //NpgsqlCommand cmd = new NpgsqlCommand("call cliente_selectbyid", con);
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM Clientes WHERE codigo = @codigo", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigo", codigo);
                con.Open();

                NpgsqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Cliente.Codigo = Convert.ToInt32(rdr["codigo"]);
                    Cliente.Cedula = rdr["cedula"].ToString();
                    Cliente.Apellidos = rdr["apellidos"].ToString();
                    Cliente.Nombres = rdr["nombres"].ToString();
                    Cliente.FechaNacimiento = Convert.ToDateTime(rdr["fechanacimiento"]);
                    Cliente.Mail = rdr["mail"].ToString();
                    Cliente.Telefono = rdr["telefono"].ToString();
                    Cliente.Direccion = rdr["direccion"].ToString();
                    Cliente.Estado = Convert.ToBoolean(rdr["estado"]);
                    Cliente.Saldo = rdr["saldo"] != DBNull.Value ? Convert.ToDecimal(rdr["saldo"]) : 0m;
                }
            }

            return Cliente;
        }

        public void DeleteCliente(int? codigo)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM Clientes WHERE codigo = @codigo", con);
                //cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@codigo", codigo);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public String GenerarCedulaValida(String provincia)
        {
            // El cuerpo de la cédula tiene 9 dígitos
            int[] cuerpo = new int[9];  

            // Asignar la provincia
            if (int.TryParse(provincia.Substring(0, 2), out int provinciaValue))
            {
                cuerpo[0] = provinciaValue / 10;
                cuerpo[1] = provinciaValue % 10;
            }
            else
            {
                throw new ArgumentException("La provincia debe contener solo dígitos");
            }

            Random rnd = new Random();
            for (int i = 2; i <= 8; i++)
            {
                cuerpo[i] = rnd.Next(0, 10);
            }

            // Calcular el dígito verificador usando el algoritmo Módulo 10
            int suma = 0;
            for (int i = 0; i <= 8; i++)
            {
                int coeficiente = (i % 2 == 0) ? 2 : 1;
                int valor = cuerpo[i] * coeficiente;
                suma += (valor > 9) ? valor - 9 : valor;
            }

            int digitoVerificador = (10 - (suma % 10)) % 10;

            return string.Join("", cuerpo) + digitoVerificador.ToString();
        }

        public ClientePS GetClienteByCedula(string cedula)
        {
            ClientePS Cliente = new ClientePS();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM Clientes WHERE cedula = @cedula", con);
                cmd.Parameters.AddWithValue("@cedula", cedula);
                con.Open();

                NpgsqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    //Cliente.Codigo = Convert.ToInt32(rdr["codigo"]);
                    Cliente.Cedula = rdr["cedula"].ToString();
                    Cliente.Apellidos = rdr["apellidos"].ToString();
                    Cliente.Nombres = rdr["nombres"].ToString();
                    Cliente.FechaNacimiento = Convert.ToDateTime(rdr["fechanacimiento"]);
                    Cliente.Mail = rdr["mail"].ToString();
                    Cliente.Telefono = rdr["telefono"].ToString();
                    Cliente.Direccion = rdr["direccion"].ToString();
                    Cliente.Estado = Convert.ToBoolean(rdr["estado"]);
                    Cliente.Saldo = rdr["saldo"] != DBNull.Value ? Convert.ToDecimal(rdr["saldo"]) : 0m;
                }
                else
                {
                    return null; // Retorna null si no se encuentra el cliente
                }
            }

            return Cliente;
        }

        public void UpdateClienteByCedula(ClientePS Cliente)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                string query = "UPDATE Clientes SET apellidos = @apellidos, nombres = @nombres, " +
                    "fechanacimiento = @fechanacimiento, mail = @mail, telefono = @telefono, " +
                    "direccion = @direccion, estado = @estado, saldo = @saldo " +
                    "WHERE cedula = @cedula;";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.AddWithValue("@cedula", Cliente.Cedula);
                cmd.Parameters.AddWithValue("@apellidos", Cliente.Apellidos);
                cmd.Parameters.AddWithValue("@nombres", Cliente.Nombres);
                cmd.Parameters.AddWithValue("@fechanacimiento", Cliente.FechaNacimiento);
                cmd.Parameters.AddWithValue("@mail", Cliente.Mail);
                cmd.Parameters.AddWithValue("@telefono", Cliente.Telefono);
                cmd.Parameters.AddWithValue("@direccion", Cliente.Direccion);
                cmd.Parameters.AddWithValue("@estado", Cliente.Estado);
                cmd.Parameters.AddWithValue("@saldo", Cliente.Saldo);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteCliente(string cedula)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM Clientes WHERE cedula = @cedula", con);
                cmd.Parameters.AddWithValue("@cedula", cedula);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
