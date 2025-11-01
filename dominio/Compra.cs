using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Compra
    {
        public int Id { get; set; }
        public Proveedor Proveedor { get; set; } 
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public List<DetalleCompra> Detalles { get; set; }
    }
}
