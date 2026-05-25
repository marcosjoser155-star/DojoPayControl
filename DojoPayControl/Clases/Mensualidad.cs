using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DojoPayControl.Clases
{
    internal class Mensualidad : Pago
    {
        // Atributo
        private string mesCorrespondiente;

        // Propiedad
        public string MesCorrespondiente
        {
            get { return mesCorrespondiente; }
            set { mesCorrespondiente = value; }
        }

        // Constructor vacío
        public Mensualidad()
        {

        }

        // Constructor con parámetros
        public Mensualidad(int idPago, int idEstudiante, decimal monto,
                           DateTime fechaPago, string metodoPago,
                           string tipoPago, string mesCorrespondiente)
            : base(idPago, idEstudiante, monto, fechaPago, metodoPago, tipoPago)
        {
            this.mesCorrespondiente = mesCorrespondiente;
        }

        // Método RegistrarMensualidad
        public void RegistrarMensualidad()
        {
            TipoPago = "Mensualidad";

            if (ValidarMensualidad())
            {
                RegistrarPago();

                Console.WriteLine("Mensualidad registrada correctamente.");
            }
            else
            {
                Console.WriteLine("Error: el mes correspondiente está vacío.");
            }
        }

        // Método ValidarMensualidad
        public bool ValidarMensualidad()
        {
            if (!string.IsNullOrEmpty(mesCorrespondiente))
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