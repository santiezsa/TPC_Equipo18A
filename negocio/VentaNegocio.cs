using dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class VentaNegocio
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
                datos.limpiarParametros();
                datos.setearConsulta(@"
                    INSERT INTO Ventas (NumeroFactura, IdCliente, IdUsuario, Fecha, Total, Activo)
                    OUTPUT INSERTED.Id
                    VALUES (@nro, @idCliente, @idUsuario, @fecha, @total, 1)");

                datos.setearParametro("@nro", venta.NumeroFactura);
                datos.setearParametro("@idCliente", venta.Cliente.Id);
                datos.setearParametro("@idUsuario", venta.Usuario.Id);
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
                    productoNegocio.ajustarStock(detalle.Producto.Id, detalle.Cantidad, "Venta", venta.Usuario.Id, false);
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

        public List<Venta> listar()
        {
            List<Venta> lista = new List<Venta>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Traigo datos para mostrar nombres
                datos.setearConsulta(@"SELECT V.Id, V.NumeroFactura, V.Fecha, V.Total, V.Activo,
                   C.Id AS IdCliente, C.Nombre, C.Apellido, 
                   U.Id AS IdUsuario, U.Username
                    FROM Ventas V
                    INNER JOIN Clientes C ON V.IdCliente = C.Id
                    INNER JOIN Usuarios U ON V.IdUsuario = U.Id
                    ORDER BY V.Fecha DESC");

                datos.ejecutarLectura();
                while(datos.Lector.Read())
                {
                    Venta aux = new Venta();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.NumeroFactura = (string)datos.Lector["NumeroFactura"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.Total = (decimal)datos.Lector["Total"];
                    aux.Activo = (bool)datos.Lector["Activo"];

                    // Cliente
                    aux.Cliente = new Cliente();
                    aux.Cliente.Id = (int)datos.Lector["IdCliente"];
                    aux.Cliente.Nombre = (string)datos.Lector["Nombre"];
                    aux.Cliente.Apellido = (string)datos.Lector["Apellido"];

                    // Vendedor
                    aux.Usuario = new Usuario();
                    aux.Usuario.Id = (int)datos.Lector["IdUsuario"];
                    aux.Usuario.Username = (string)datos.Lector["Username"];

                    lista.Add(aux);
                }
                return lista;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void anular(int idVenta, int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // 1 Traigo items de la venta para devolver stock
                List<DetalleVenta> detalles = new List<DetalleVenta>();

                // Obtengo detalles (id prod y cantidad)
                datos.setearConsulta("SELECT IdProducto, Cantidad FROM DetalleVenta WHERE IdVenta = @IdVenta");
                datos.setearParametro("@IdVenta", idVenta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    DetalleVenta detalle = new DetalleVenta();
                    detalle.Producto = new Producto { Id = (int)datos.Lector["IdProducto"] };
                    detalle.Cantidad = (int)datos.Lector["Cantidad"];
                    detalles.Add(detalle);
                }
                datos.cerrarConexion();

                // 2 Devuelvo stock
                ProductoNegocio productoNegocio = new ProductoNegocio();
                foreach (var item in detalles)
                {
                    // Actualizo stock con cantidad positiva para sumar
                    productoNegocio.ajustarStock(item.Producto.Id, item.Cantidad, "Anulación Venta #" + idVenta, idUsuario, true);
                }

                // 3 Anulo la venta
                datos = new AccesoDatos();
                datos.setearConsulta("UPDATE Ventas SET Activo = 0 WHERE Id = @Id");
                datos.setearParametro("@Id", idVenta);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}