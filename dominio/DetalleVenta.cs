using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class DetalleVenta
    {
        public int Id { get; set; }
        public int IdVenta { get; set; } 
        public Producto Producto { get; set; } 
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
