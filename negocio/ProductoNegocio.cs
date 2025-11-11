using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using dominio;

namespace negocio

//MODIFICAR LA BASE DE DATOS DE PRODUCTOS (CODIGO Y PRECIO)

{
    public class ProductoNegocio
    {
        public List<Producto> listar()
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT P.Id, P.Codigo, P.Nombre, P.Descripcion, P.PorcentajeGanancia, P.StockActual, P.StockMinimo, P.IdMarca, M.Descripcion AS MarcaDescripcion, P.IdCategoria, C.Descripcion AS CategoriaDescripcion FROM Productos AS P JOIN Marcas AS M ON P.IdMarca = M.Id JOIN Categorias AS C ON P.IdCategoria = C.Id");
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
                datos.setearConsulta("SELECT P.Id, P.Codigo, P.Nombre, P.Descripcion, P.PorcentajeGanancia, P.StockActual, P.StockMinimo, P.IdMarca, M.Descripcion AS MarcaDescripcion, P.IdCategoria, C.Descripcion AS CategoriaDescripcion FROM Productos AS P JOIN Marcas AS M ON P.IdMarca = M.Id JOIN Categorias AS C ON P.IdCategoria = C.Id WHERE P.Id = @Id");
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

                    producto.Marca = new Marca();
                    producto.Marca.Id = (int)datos.Lector["IdMarca"];
                    producto.Marca.Descripcion = (string)datos.Lector["MarcaDescripcion"];

                    producto.Categoria = new Categoria();
                    producto.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    producto.Categoria.Descripcion = (string)datos.Lector["CategoriaDescripcion"];
                }
                return producto;
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
                datos.setearConsulta("INSERT INTO Productos (Codigo, Nombre, Descripcion, PorcentajeGanancia, StockActual, StockMinimo, IdMarca, IdCategoria) VALUES (@Codigo, @Nombre, @Descripcion, @PorcentajeGanancia, @StockActual, @StockMinimo, @IdMarca, @IdCategoria)");
                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@PorcentajeGanancia", nuevo.PorcentajeGanancia);
                datos.setearParametro("@StockActual", nuevo.StockActual);
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
                datos.setearConsulta("UPDATE Productos SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, PorcentajeGanancia = @PorcentajeGanancia, StockActual = @StockActual, StockMinimo = @StockMinimo, IdMarca = @IdMarca, IdCategoria = @IdCategoria WHERE Id = @Id");
                datos.setearParametro("@Codigo", producto.Codigo);
                datos.setearParametro("@Nombre", producto.Nombre);
                datos.setearParametro("@Descripcion", producto.Descripcion);
                datos.setearParametro("@PorcentajeGanancia", producto.PorcentajeGanancia);
                datos.setearParametro("@StockActual", producto.StockActual);
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
                datos.setearConsulta("DELETE FROM Productos WHERE Id = @Id");
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

    }
}
