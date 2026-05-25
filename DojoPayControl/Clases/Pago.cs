using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DojoPayControl.Clases
{
    public class Pago
    {
        // Atributos
        private int idPago;
        private int idEstudiante;
        private decimal monto;
        private DateTime fechaPago;
        private string metodoPago;
        private string tipoPago;

        // Propiedades
        public int IdPago
        {
            get { return idPago; }
            set { idPago = value; }
        }

        public int IdEstudiante
        {
            get { return idEstudiante; }
            set { idEstudiante = value; }
        }

        public decimal Monto
        {
            get { return monto; }
            set { monto = value; }
        }

        public DateTime FechaPago
        {
            get { return fechaPago; }
            set { fechaPago = value; }
        }

        public string MetodoPago
        {
            get { return metodoPago; }
            set { metodoPago = value; }
        }

        public string TipoPago
        {
            get { return tipoPago; }
            set { tipoPago = value; }
        }

        // Constructor vacío
        public Pago()
        {

        }

        // Constructor con parámetros
        public Pago(int idPago, int idEstudiante, decimal monto,
                    DateTime fechaPago, string metodoPago, string tipoPago)
        {
            this.idPago = idPago;
            this.idEstudiante = idEstudiante;
            this.monto = monto;
            this.fechaPago = fechaPago;
            this.metodoPago = metodoPago;
            this.tipoPago = tipoPago;
        }

        // Método RegistrarPago
        public void RegistrarPago()
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                conexion.AbrirConexion();

                string consulta = "INSERT INTO Pago " +
                                  "(idEstudiante, monto, fechaPago, metodoPago, tipoPago) " +
                                  "VALUES (@idEstudiante, @monto, @fechaPago, @metodoPago, @tipoPago)";

                MySqlCommand comando = new MySqlCommand(consulta, conexion.Conexion);

                comando.Parameters.AddWithValue("@idEstudiante", idEstudiante);
                comando.Parameters.AddWithValue("@monto", monto);
                comando.Parameters.AddWithValue("@fechaPago", fechaPago);
                comando.Parameters.AddWithValue("@metodoPago", metodoPago);
                comando.Parameters.AddWithValue("@tipoPago", tipoPago);

                comando.ExecuteNonQuery();

                Console.WriteLine("Pago registrado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al registrar pago: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        // Método BuscarPago
        public void BuscarPago(int id)
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                conexion.AbrirConexion();

                string consulta = "SELECT idPago, idEstudiante, monto, fechaPago, metodoPago, tipoPago " +
                                  "FROM Pago " +
                                  "WHERE idPago = @idPago";

                MySqlCommand comando = new MySqlCommand(consulta, conexion.Conexion);

                comando.Parameters.AddWithValue("@idPago", id);

                MySqlDataReader lector = comando.ExecuteReader();

                if (lector.Read())
                {
                    idPago = Convert.ToInt32(lector["idPago"]);
                    idEstudiante = Convert.ToInt32(lector["idEstudiante"]);
                    monto = Convert.ToDecimal(lector["monto"]);
                    fechaPago = Convert.ToDateTime(lector["fechaPago"]);
                    metodoPago = lector["metodoPago"].ToString();
                    tipoPago = lector["tipoPago"].ToString();

                    Console.WriteLine("Pago encontrado.");
                }
                else
                {
                    Console.WriteLine("No se encontró el pago.");
                }

                lector.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al buscar pago: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        // Método MostrarHistorial
        public void MostrarHistorial(int estudianteId)
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                conexion.AbrirConexion();

                string consulta = "SELECT * FROM Pago " +
                                  "WHERE idEstudiante = @idEstudiante";

                MySqlCommand comando = new MySqlCommand(consulta, conexion.Conexion);

                comando.Parameters.AddWithValue("@idEstudiante", estudianteId);

                MySqlDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Console.WriteLine(
                        "ID Pago: " + lector["idPago"] +
                        " | Monto: " + lector["monto"] +
                        " | Fecha: " + lector["fechaPago"] +
                        " | Método: " + lector["metodoPago"] +
                        " | Tipo: " + lector["tipoPago"]
                    );
                }

                lector.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al mostrar historial: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        // Método ValidarPago
        public bool ValidarPago()
        {
            if (monto > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
