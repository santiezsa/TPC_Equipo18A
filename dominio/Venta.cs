using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Venta
    {
        public int Id { get; set; }
        public string NumeroFactura { get; set; }
        public Cliente Cliente { get; set; }
        public Usuario Vendedor { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public bool Activo { get; set; }
        public List<DetalleVenta> Detalles { get; set; } 

        public Venta() 
        {
            Detalles = new List<DetalleVenta>();
        }
    }
}
