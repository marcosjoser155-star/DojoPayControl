using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DojoPayControl.Clases
{
    public class AusenciaTemporal
    {
        // Atributos
        private int idAusencia;
        private int idEstudiante;
        private DateTime fechaInicio;
        private DateTime fechaFin;
        private string motivo;

        // Propiedades
        public int IdAusencia
        {
            get { return idAusencia; }
            set { idAusencia = value; }
        }

        public int IdEstudiante
        {
            get { return idEstudiante; }
            set { idEstudiante = value; }
        }

        public DateTime FechaInicio
        {
            get { return fechaInicio; }
            set { fechaInicio = value; }
        }

        public DateTime FechaFin
        {
            get { return fechaFin; }
            set { fechaFin = value; }
        }

        public string Motivo
        {
            get { return motivo; }
            set { motivo = value; }
        }

        // Constructor vacío
        public AusenciaTemporal()
        {

        }

        // Constructor con parámetros
        public AusenciaTemporal(int idAusencia, int idEstudiante,
                                DateTime fechaInicio, DateTime fechaFin,
                                string motivo)
        {
            this.idAusencia = idAusencia;
            this.idEstudiante = idEstudiante;
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            this.motivo = motivo;
        }

        // Método RegistrarAusencia
        public void RegistrarAusencia()
        {
            Console.WriteLine("Ausencia temporal registrada.");
        }

        // Método FinalizarAusencia
        public void FinalizarAusencia()
        {
            Console.WriteLine("Ausencia temporal finalizada.");
        }
    }
}