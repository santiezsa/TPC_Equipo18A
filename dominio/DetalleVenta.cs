using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    internal class DetalleVenta
    {
        public int Id { get; set; }
        public Producto Producto { get; set; } // TODO: Clase Producto
        public int Cantidad { get; set; }
        public decimal PrecioUnitarioVenta { get; set; }
    }
}
