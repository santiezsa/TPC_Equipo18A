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
            ProductoNegocio productoNegocio = new ProductoNegocio();

            try
            {
                // Valido stock antes de insertar venta
                foreach (DetalleVenta det in venta.Detalles)
                {
                    int stockActual;
                    if (!productoNegocio.HayStockDisponible(det.Producto.Id, det.Cantidad, out stockActual))
                    {
                        throw new Exception(
                            $"No hay stock suficiente para el producto {det.Producto.Nombre}. Disponible: {stockActual}, solicitado: {det.Cantidad}."
                        );
                    }
                }
                // Si el total vino en 0, lo calculamos desde los detalles
                if (venta.Total == 0 && venta.Detalles != null)
                {
                    decimal total = 0;
                    foreach (DetalleVenta item in venta.Detalles)
                        total += item.Subtotal;

                    venta.Total = total;
                }

                // Generar número de factura (ToDo: mejorar)
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
                while (datos.Lector.Read())
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
            catch (Exception ex)
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

        public List<Venta> ListarUltimasVentas(int cantidad)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Venta> lista = new List<Venta>();

            try
            {
                datos.setearConsulta(@"
                    SELECT TOP (@cant)
                            V.Id,
                            V.Fecha,
                            V.Total,
                            C.Id AS IdCliente,
                            C.Nombre,
                            C.Apellido,
                            C.Documento
                    FROM Ventas V
                    INNER JOIN Clientes C ON V.IdCliente = C.Id
                    ORDER BY V.Fecha DESC");

                datos.setearParametro("@cant", cantidad);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Venta venta = new Venta();
                    venta.Id = (int)datos.Lector["Id"];
                    venta.Fecha = (DateTime)datos.Lector["Fecha"];
                    venta.Total = (decimal)datos.Lector["Total"];

                    venta.Cliente = new Cliente();
                    venta.Cliente.Id = (int)datos.Lector["IdCliente"];
                    venta.Cliente.Nombre = datos.Lector["Nombre"].ToString();
                    venta.Cliente.Apellido = datos.Lector["Apellido"].ToString();
                    venta.Cliente.Documento = datos.Lector["Documento"].ToString();

                    lista.Add(venta);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Venta> listarVentasPorMes(int mes, int anio)
        {
            List<Venta> lista = new List<Venta>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Fecha y total de las ventas activas del mes solicitado
                datos.setearConsulta("SELECT Id, Fecha, Total FROM Ventas WHERE MONTH(Fecha) = @Mes AND YEAR(Fecha) = @Anio AND Activo = 1");
                datos.setearParametro("@Mes", mes);
                datos.setearParametro("@Anio", anio);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Venta aux = new Venta();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.Total = (decimal)datos.Lector["Total"];

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

        // Metodo para generar factura con datos completos
        public Venta buscarPorIdConDetalles(int idVenta)
        {
            Venta venta = new Venta();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Consulta: datos de la venta, cliente y vendedor
                datos.setearConsulta(@"
                    SELECT V.Id, V.NumeroFactura, V.Fecha, V.Total, V.Activo,
                    C.Id AS IdCliente, C.Nombre, C.Apellido, C.Email, C.Documento, C.Direccion,
                    U.Id AS IdUsuario, U.Username
                    FROM Ventas V
                    INNER JOIN Clientes C ON V.IdCliente = C.Id
                    INNER JOIN Usuarios U ON V.IdUsuario = U.Id
                    WHERE V.Id = @Id");

                datos.setearParametro("@Id", idVenta);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    venta.Id = (int)datos.Lector["Id"];
                    venta.NumeroFactura = (string)datos.Lector["NumeroFactura"];
                    venta.Fecha = (DateTime)datos.Lector["Fecha"];
                    venta.Total = (decimal)datos.Lector["Total"];
                    venta.Activo = (bool)datos.Lector["Activo"];

                    // Carga cliente para la venta
                    venta.Cliente = new Cliente();
                    venta.Cliente.Id = (int)datos.Lector["IdCliente"];
                    venta.Cliente.Nombre = (string)datos.Lector["Nombre"];
                    venta.Cliente.Apellido = (string)datos.Lector["Apellido"];
                    venta.Cliente.Email = (string)datos.Lector["Email"];
                    venta.Cliente.Documento = (string)datos.Lector["Documento"];
                    venta.Cliente.Direccion = (string)datos.Lector["Direccion"];

                    // Carga vendedor
                    venta.Usuario = new Usuario();
                    venta.Usuario.Id = (int)datos.Lector["IdUsuario"];
                    venta.Usuario.Username = (string)datos.Lector["Username"];
                }
                datos.cerrarConexion();

                // Consulta productos vendidos
                datos = new AccesoDatos();
                datos.setearConsulta(@"
                    SELECT DV.Id, DV.Cantidad, DV.PrecioUnitario,
                    P.Id AS IdProducto, P.Nombre, P.Codigo
                    FROM DetalleVenta DV
                    INNER JOIN Productos P ON DV.IdProducto = P.Id
                    WHERE DV.IdVenta = @IdVenta");

                datos.setearParametro("@IdVenta", idVenta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    DetalleVenta detalle = new DetalleVenta();
                    detalle.Id = (int)datos.Lector["Id"];
                    detalle.IdVenta = idVenta;
                    detalle.Cantidad = (int)datos.Lector["Cantidad"];
                    detalle.PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"];

                    detalle.Producto = new Producto();
                    detalle.Producto.Id = (int)datos.Lector["IdProducto"];
                    detalle.Producto.Nombre = (string)datos.Lector["Nombre"];
                    detalle.Producto.Codigo = (string)datos.Lector["Codigo"];

                    // Agrego a la lista de venta
                    venta.Detalles.Add(detalle);
                }

                return venta;
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