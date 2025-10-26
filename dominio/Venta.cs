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
        public Cliente Cliente { get; set; } // TODO: Clase Cliente
        public Usuario Vendedor { get; set; } // TODO: Clase Usuario
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public List<DetalleVenta> Detalles { get; set; } // TODO: Clase DetalleVenta
    }
}
