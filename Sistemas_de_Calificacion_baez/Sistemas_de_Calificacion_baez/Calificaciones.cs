using System;
using System.Collections.Generic;
using System.Text;

namespace Sistemas_de_Calificacion_baez
{
    public class Calificaciones
    {
        public int ID { get; set; }
        public string Profesor { get; set; }
        public string Matricula { get; set; }
        public string Nombre { get; set; }
        public decimal Asistencia { get; set; }
        public decimal TrabajoPractico { get; set; }
        public decimal ExamenParcial { get; set; }
        public decimal ExamenFinal { get; set; }
        public decimal NotaFinal { get; set; }

        public Calificaciones(int ID, string profesor, string matricula, string nombre, decimal asistencia, decimal trabajoPractico, decimal examenParcial, decimal examenFinal, decimal notaFinal)
        {
            this.ID = ID;
            Profesor = profesor;
            Matricula = matricula;
            Nombre = nombre;
            Asistencia = asistencia;
            TrabajoPractico = trabajoPractico;
            ExamenParcial = examenParcial;
            ExamenFinal = examenFinal;
            NotaFinal = notaFinal;
        }
    }

}
