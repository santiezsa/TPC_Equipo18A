using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    internal class Producto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        //public Marca Marca { get; set; }
        //public Categoria Categoria { get; set; }
        //public Proveedor Provedor { get; set; }
        public decimal Precio { get; set; }
        public int stock { get; set; }

    }
}
