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
    }
}
