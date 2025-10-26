using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    internal class Compra
    {
        public int Id { get; set; }
        //public Proveedor Proveedor { get; set; } // TODO: Clase Proveedor
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public List<DetalleCompra> Detalles { get; set; }
    }
}
