using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class CompraNegocio
    {
        public void agregar(Compra compra)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Compras (IdProveedor, IdUsuario, Fecha, Total, Activo) OUTPUT inserted.Id VALUES (@IdProveedor, @IdUsuario, @Fecha, @Total, 1)");
                datos.setearParametro("@IdProveedor", compra.Proveedor.Id);
                datos.setearParametro("@IdUsuario", compra.Usuario.Id);
                datos.setearParametro("@Fecha", compra.Fecha);
                datos.setearParametro("@Total", compra.Total);

                // Guardo el ID de la compra generada
                int idCompraGenerada = (int)datos.ejecutarLecturaScalar();

                datos.cerrarConexion();

                // Recorro productos
                ProductoNegocio productoNegocio = new ProductoNegocio();

                foreach (DetalleCompra item in compra.Detalles)
                {
                    // Insertar en DetalleCompra
                    datos = new AccesoDatos();
                    datos.setearConsulta("INSERT INTO DetalleCompra (IdCompra, IdProducto, Cantidad, PrecioUnitario) VALUES (@IdCompra, @IdProducto, @Cantidad, @PrecioUnitario)");
                    datos.setearParametro("@IdCompra", idCompraGenerada);
                    datos.setearParametro("@IdProducto", item.Producto.Id);
                    datos.setearParametro("@Cantidad", item.Cantidad);
                    datos.setearParametro("@PrecioUnitario", item.PrecioUnitario); // Costo
                    datos.ejecutarAccion();
                    datos.cerrarConexion();

                    // Actualizar Stock
                    datos = new AccesoDatos();
                    datos.setearConsulta("UPDATE Productos SET StockActual = StockActual + @Cantidad WHERE Id = @Id");
                    datos.setearParametro("@Cantidad", item.Cantidad);
                    datos.setearParametro("@Id", item.Producto.Id);
                    datos.ejecutarAccion();
                    datos.cerrarConexion();

                    // Autovinculo el proveedor (si no se vinculo prviamente)
                    // Usamos el método que creaste en ProductoNegocio
                    // (Nota: Tienes que haber agregado el método 'existeRelacion' en ProductoNegocio
                    // si no lo hiciste, avísame. Si no querés complicarte, podés borrar este bloque 'C' por ahora).
                    try
                    {
                        // Intentamos vincular. Si ya existe, SQL tirará error de Primary Key duplicada,
                        // así que lo envolvemos en try-catch vacío para que "siga de largo" si ya existe.
                        // Es una forma "rápida y sucia" de hacerlo sin consultar antes.
                        productoNegocio.vincularProveedor(item.Producto.Id, compra.Proveedor.Id);
                    }
                    catch { } // Si falla (ya existe), no pasa nada.
                }
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
        public List<Compra> listar()
        {
            List<Compra> lista = new List<Compra>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Traigo datos para mostrar nombres
                datos.setearConsulta(@"select C.Id, C.Fecha, C.Total, C.Activo, P.Id as idProveedor, P.Nombre, 
                    P.RazonSocial, U.Id as idUsuario, U.Username
                    from Compras C 
                    inner join Proveedores P on C.IdProveedor = P.Id
                    inner join Usuarios U on C.IdUsuario = U.Id
                    order by C.Fecha DESC;");

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Compra aux = new Compra();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.Total = (decimal)datos.Lector["Total"];
                    aux.Activo = (bool)datos.Lector["Activo"];

                    // Proveedor
                    aux.Proveedor = new Proveedor();
                    aux.Proveedor.Id = (int)datos.Lector["IdProveedor"];
                    aux.Proveedor.Nombre = (string)datos.Lector["Nombre"];
                    aux.Proveedor.RazonSocial = (string)datos.Lector["RazonSocial"];

                    // Usuario
                    aux.Usuario = new Usuario();
                    aux.Usuario.Id = (int)datos.Lector["IdUsuario"];
                    aux.Usuario.Username = (string)datos.Lector["Username"];

                    lista.Add(aux);
                }
                return lista;
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

        public void anular(int idCompra, int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // 1 Traigo items de la compra para devolver stock
                List<DetalleCompra> detalles = new List<DetalleCompra>();

                // Obtengo detalles (id prod y cantidad)
                datos.setearConsulta("SELECT IdProducto, Cantidad FROM DetalleCompra WHERE IdCompra = @IdCompra");
                datos.setearParametro("@IdCompra", idCompra);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    DetalleCompra detalle = new DetalleCompra();
                    detalle.Producto = new Producto { Id = (int)datos.Lector["IdProducto"] };
                    detalle.Cantidad = (int)datos.Lector["Cantidad"];
                    detalles.Add(detalle);
                }
                datos.cerrarConexion();

                // 2 Devuelvo stock
                ProductoNegocio productoNegocio = new ProductoNegocio();
                foreach (var item in detalles)
                {
                    // Actualizo stock 
                    productoNegocio.ajustarStock(item.Producto.Id, item.Cantidad, "Anulación Compra #" + idCompra, idUsuario, false);
                }

                // 3 Anulo la compra
                datos = new AccesoDatos();
                datos.setearConsulta("UPDATE Compras SET Activo = 0 WHERE Id = @Id");
                datos.setearParametro("@Id", idCompra);
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
