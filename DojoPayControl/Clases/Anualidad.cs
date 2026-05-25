using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DojoPayControl.Clases
{
    public class Anualidad : Pago
    {
        // Atributo
        private int añoCorrespondiente;

        // Propiedad
        public int AñoCorrespondiente
        {
            get { return añoCorrespondiente; }
            set { añoCorrespondiente = value; }
        }

        // Constructor vacío
        public Anualidad()
        {

        }

        // Constructor con parámetros
        public Anualidad(int idPago, int idEstudiante, decimal monto,
                          DateTime fechaPago, string metodoPago,
                          string tipoPago, int añoCorrespondiente)
            : base(idPago, idEstudiante, monto, fechaPago, metodoPago, tipoPago)
        {
            this.añoCorrespondiente = añoCorrespondiente;
        }

        // Método RegistrarAnualidad
        public void RegistrarAnualidad()
        {
            TipoPago = "Anualidad";

            if (ValidarAnualidad())
            {
                RegistrarPago();

                Console.WriteLine("Anualidad registrada correctamente.");
            }
            else
            {
                Console.WriteLine("Error: el año correspondiente no es válido.");
            }
        }

        // Método ValidarAnualidad
        public bool ValidarAnualidad()
        {
            if (añoCorrespondiente > 0)
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
