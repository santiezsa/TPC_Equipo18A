using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using dominio;

namespace negocio

{
    public class ProductoNegocio
    {
        public List<Producto> listar()
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT P.Id, P.Codigo, P.Nombre, P.Descripcion, P.PorcentajeGanancia, P.StockActual, P.StockMinimo, P.IdMarca, M.Descripcion AS MarcaDescripcion, P.IdCategoria, C.Descripcion AS CategoriaDescripcion, P.Activo FROM Productos AS P JOIN Marcas AS M ON P.IdMarca = M.Id JOIN Categorias AS C ON P.IdCategoria = C.Id WHERE P.Activo = 1");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();

                    //Datos basicos
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"];
                    aux.StockActual = (int)datos.Lector["StockActual"];
                    aux.StockMinimo = (int)datos.Lector["StockMinimo"];
                    aux.Activo = (bool)datos.Lector["Activo"];

                    // Datos de marca
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["MarcaDescripcion"];

                    // Datos de categoria
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["CategoriaDescripcion"];

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

        public Producto buscarPorId(int id)
        {
            Producto producto = null;
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT P.Id, P.Codigo, P.Nombre, P.Descripcion, P.PorcentajeGanancia, P.StockActual, P.StockMinimo, P.IdMarca, M.Descripcion AS MarcaDescripcion, P.IdCategoria, C.Descripcion AS CategoriaDescripcion, P.Activo FROM Productos AS P JOIN Marcas AS M ON P.IdMarca = M.Id JOIN Categorias AS C ON P.IdCategoria = C.Id WHERE P.Id = @Id");
                datos.setearParametro("@Id", id);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    producto = new Producto();
                    producto.Id = (int)datos.Lector["Id"];
                    producto.Codigo = (string)datos.Lector["Codigo"];
                    producto.Nombre = (string)datos.Lector["Nombre"];
                    producto.Descripcion = (string)datos.Lector["Descripcion"];
                    producto.PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"];
                    producto.StockActual = (int)datos.Lector["StockActual"];
                    producto.StockMinimo = (int)datos.Lector["StockMinimo"];
                    producto.Activo = (bool)datos.Lector["Activo"];

                    producto.Marca = new Marca();
                    producto.Marca.Id = (int)datos.Lector["IdMarca"];
                    producto.Marca.Descripcion = (string)datos.Lector["MarcaDescripcion"];

                    producto.Categoria = new Categoria();
                    producto.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    producto.Categoria.Descripcion = (string)datos.Lector["CategoriaDescripcion"];

                    return producto;
                }
                return null;
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

        public void agregar(Producto nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Productos (Codigo, Nombre, Descripcion, PorcentajeGanancia, StockActual, StockMinimo, IdMarca, IdCategoria, Activo) VALUES (@Codigo, @Nombre, @Descripcion, @PorcentajeGanancia, 0, @StockMinimo, @IdMarca, @IdCategoria, 1)");
                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@PorcentajeGanancia", nuevo.PorcentajeGanancia);
                datos.setearParametro("@StockMinimo", nuevo.StockMinimo);
                datos.setearParametro("@IdMarca", nuevo.Marca.Id);
                datos.setearParametro("@IdCategoria", nuevo.Categoria.Id);
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

        public void modificar(Producto producto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Productos SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, PorcentajeGanancia = @PorcentajeGanancia, StockMinimo = @StockMinimo, IdMarca = @IdMarca, IdCategoria = @IdCategoria WHERE Id = @Id");
                datos.setearParametro("@Codigo", producto.Codigo);
                datos.setearParametro("@Nombre", producto.Nombre);
                datos.setearParametro("@Descripcion", producto.Descripcion);
                datos.setearParametro("@PorcentajeGanancia", producto.PorcentajeGanancia);
                datos.setearParametro("@StockMinimo", producto.StockMinimo);
                datos.setearParametro("@IdMarca", producto.Marca.Id);
                datos.setearParametro("@IdCategoria", producto.Categoria.Id);
                datos.setearParametro("@Id", producto.Id);
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

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Productos SET Activo = 0 WHERE Id = @Id");
                datos.setearParametro("@Id", id);
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

        public List<Producto> buscarPorNombre(string nombre)
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT P.Id, P.Codigo, P.Nombre, P.Descripcion, P.PorcentajeGanancia, P.StockActual, P.StockMinimo, P.IdMarca, M.Descripcion AS MarcaDescripcion, P.IdCategoria, C.Descripcion AS CategoriaDescripcion FROM Productos AS P JOIN Marcas AS M ON P.IdMarca = M.Id JOIN Categorias AS C ON P.IdCategoria = C.Id WHERE P.Nombre LIKE @Nombre");
                datos.setearParametro("@Nombre", "%" + nombre + "%");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    //Datos basicos
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"];
                    aux.StockActual = (int)datos.Lector["StockActual"];
                    aux.StockMinimo = (int)datos.Lector["StockMinimo"];

                    // Datos de marca
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["MarcaDescripcion"];

                    // Datos de categoria
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["CategoriaDescripcion"];
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

        public List<Proveedor> listarProveedoresPorProducto(int idProducto)
        {
            List<Proveedor> lista = new List<Proveedor>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Uno tabla intermedia con la de proveedores para sacar los datos
                string consulta = "SELECT P.Id, P.RazonSocial, P.CUIT, P.Email " +
                                  "FROM Proveedores AS P " +
                                  "INNER JOIN Productos_x_Proveedores AS PxP ON P.Id = PxP.IdProveedor " +
                                  "WHERE PxP.IdProducto = @IdProducto AND P.Activo = 1";

                datos.setearConsulta(consulta);
                datos.setearParametro("@IdProducto", idProducto);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Proveedor aux = new Proveedor();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.RazonSocial = (string)datos.Lector["RazonSocial"];
                    aux.CUIT = (string)datos.Lector["CUIT"];
                    aux.Email = (string)datos.Lector["Email"];
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

        public void vincularProveedor(int idProducto, int idProveedor)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Valido si ya existe para no duplicar
                datos.setearConsulta("INSERT INTO Productos_x_Proveedores (IdProducto, IdProveedor) VALUES (@IdProducto, @IdProveedor)");
                datos.setearParametro("@IdProducto", idProducto);
                datos.setearParametro("@IdProveedor", idProveedor);
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

        public void desvincularProveedor(int idProducto, int idProveedor)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM Productos_x_Proveedores WHERE IdProducto = @IdProducto AND IdProveedor = @IdProveedor");
                datos.setearParametro("@IdProducto", idProducto);
                datos.setearParametro("@IdProveedor", idProveedor);
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

        public void ajustarStock(int idProducto, int cantidad, string motivo, int idUsuario, bool esIngreso)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string operador = esIngreso ? "+" : "-"; // ingreso sumo , egreso resto

                datos.setearConsulta($"UPDATE Productos SET StockActual = StockActual {operador} @Cantidad WHERE Id = @Id");
                datos.setearParametro("@Cantidad", cantidad);
                datos.setearParametro("@Id", idProducto);
                datos.ejecutarAccion();

                datos.cerrarConexion();
                datos = new AccesoDatos();

                datos.setearConsulta("INSERT INTO MovimientosStock (IdProducto, IdUsuario, Fecha, Cantidad, EsIngreso, Motivo) VALUES (@IdProducto, @IdUsuario, GETDATE(), @Cantidad, @EsIngreso, @Motivo)");
                datos.setearParametro("@IdProducto", idProducto);
                datos.setearParametro("@IdUsuario", idUsuario);
                datos.setearParametro("@Cantidad", cantidad);
                datos.setearParametro("@EsIngreso", esIngreso);
                datos.setearParametro("@Motivo", motivo);
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

        public decimal ObtenerPrecioVenta(int idProducto)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT TOP 1 dc.PrecioUnitario AS PrecioCompra, p.PorcentajeGanancia
                    FROM DetalleCompra dc
                    INNER JOIN Compras c ON c.Id = dc.IdCompra
                    INNER JOIN Productos p ON p.Id = dc.IdProducto
                    WHERE dc.IdProducto = @idProducto
                    ORDER BY c.Fecha DESC");

                datos.setearParametro("@idProducto", idProducto);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    decimal precioCompra = (decimal)datos.Lector["PrecioCompra"];
                    decimal porcentaje = (decimal)datos.Lector["PorcentajeGanancia"];

                    // si PorcentajeGanancia = 30 -> 30%
                    decimal precioVenta = precioCompra * (1 + (porcentaje / 100m));
                    return precioVenta;
                }
                else
                {
                    // no hay compras cargadas para ese producto
                    throw new Exception("No hay compras registradas para este producto.");
                }
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public bool existeRelacion(int idProducto, int idProveedor)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Contamos cuántas filas existen con esa combinación exacta
                datos.setearConsulta("SELECT COUNT(*) FROM Productos_x_Proveedores WHERE IdProducto = @IdProd AND IdProveedor = @IdProv");
                datos.setearParametro("@IdProd", idProducto);
                datos.setearParametro("@IdProv", idProveedor);

                // ejecutarLecturaScalar devuelve un object, lo casteamos a int
                int cantidad = (int)datos.ejecutarLecturaScalar();

                // Si la cantidad es mayor a 0, significa que YA EXISTE
                return cantidad > 0;
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

        public bool HayStockDisponible(int idProducto, int cantidadSolicitada, out int stockActual)
        {
            AccesoDatos datos = new AccesoDatos();
            stockActual = 0;

            try
            {
                datos.setearConsulta("SELECT StockActual FROM Productos WHERE Id = @id");
                datos.setearParametro("@id", idProducto);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    stockActual = (int)datos.Lector["StockActual"];
                    return stockActual >= cantidadSolicitada;
                }

                return false; 
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Producto> listarStockBajo(int limite = 15)
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, Nombre, StockActual FROM Productos WHERE StockActual <= @limite ORDER BY StockActual ASC");
                datos.setearParametro("@limite", limite);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto producto = new Producto();
                    producto.Id = (int)datos.Lector["Id"];
                    producto.Nombre = (string)datos.Lector["Nombre"];
                    producto.StockActual = (int)datos.Lector["StockActual"];

                    lista.Add(producto);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
