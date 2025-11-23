using dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    internal class VentaNegocio
    {
        public void registrar(Venta venta)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // 1) Si el total vino en 0, lo calculamos desde los detalles
                if (venta.Total == 0 && venta.Detalles != null)
                {
                    decimal total = 0;
                    foreach (DetalleVenta item in venta.Detalles)
                        total += item.Subtotal;

                    venta.Total = total;
                }

                // 2) Generar número de factura (ToDo: mejorar)
                if (string.IsNullOrEmpty(venta.NumeroFactura))
                    venta.NumeroFactura = GenerarNumeroFactura();

                // 3) Insertar VENTA y recuperar Id
                datos.setearConsulta(@"
                    INSERT INTO Ventas (NumeroFactura, IdCliente, IdVendedor, Fecha, Total, Activo)
                    OUTPUT INSERTED.Id
                    VALUES (@nro, @idCliente, @idVendedor, @fecha, @total, 1)");

                datos.setearParametro("@nro", venta.NumeroFactura);
                datos.setearParametro("@idCliente", venta.Cliente.Id);
                datos.setearParametro("@idVendedor", venta.Vendedor.Id); 
                datos.setearParametro("@fecha", venta.Fecha);
                datos.setearParametro("@total", venta.Total);

                int idVenta = Convert.ToInt32(datos.ejecutarLecturaScalar());
                venta.Id = idVenta;

                // 4) Insertar DETALLES y actualizar stock
                ProductoNegocio productoNegocio = new ProductoNegocio();

                foreach (DetalleVenta detalle in venta.Detalles)
                {
                    datos.limpiarParametros();
                    datos.setearConsulta(@"
                        INSERT INTO DetalleVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario)
                        VALUES (@idVenta, @idProducto, @cantidad, @precio)");

                    datos.setearParametro("@idVenta", idVenta);
                    datos.setearParametro("@idProducto", detalle.Producto.Id);
                    datos.setearParametro("@cantidad", detalle.Cantidad);
                    datos.setearParametro("@precio", detalle.PrecioUnitario);

                    datos.ejecutarAccion();

                    // stock
                    productoNegocio.ajustarStock(detalle.Producto.Id, detalle.Cantidad, "Venta", venta.Vendedor.Id, false);
                }
            }
            finally
            {
                datos.cerrarConexion();
            }

        }

        private string GenerarNumeroFactura()
        {
            return "FAC-" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }

    }
}