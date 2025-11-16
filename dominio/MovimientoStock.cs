using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class MovimientoStock
    {
        public int Id { get; set; }
        public Producto Producto { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public bool EsIngreso { get; set; } // true para ingreso, false para egreso
        public string Motivo { get; set; } 
    }
}
