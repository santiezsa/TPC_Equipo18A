using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ProveedorNegocio
    {
        public List<Proveedor> listar()
        {
            List<Proveedor> lista = new List<Proveedor>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Id, Nombre, RazonSocial, CUIT, Email, Telefono FROM Proveedores");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Proveedor aux = new Proveedor();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.RazonSocial = (string)datos.Lector["RazonSocial"];
                    aux.CUIT = (string)datos.Lector["CUIT"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.Telefono = (string)datos.Lector["Telefono"];
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

        public Proveedor buscarPorId(int id)
        {
            Proveedor proveedor = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, Nombre, RazonSocial, CUIT, Email, Telefono FROM Proveedores WHERE Id = @Id");
                datos.setearParametro("@Id", id);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    proveedor = new Proveedor();

                    proveedor.Id = (int)datos.Lector["Id"];
                    proveedor.Nombre = (string)datos.Lector["Nombre"];
                    proveedor.RazonSocial = (string)datos.Lector["RazonSocial"];
                    proveedor.CUIT = (string)datos.Lector["CUIT"];
                    proveedor.Email = (string)datos.Lector["Email"];
                    proveedor.Telefono = (string)datos.Lector["Telefono"];
                }
                return proveedor;
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

        public void agregar(Proveedor nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Proveedores (Nombre, RazonSocial, CUIT, Email, Telefono) VALUES (@Nombre, @RazonSocial, @CUIT, @Email, @Telefono)");
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@RazonSocial", nuevo.RazonSocial);
                datos.setearParametro("@CUIT", nuevo.CUIT);
                datos.setearParametro("@Email", nuevo.Email);
                datos.setearParametro("@Telefono", nuevo.Telefono);
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

        public void modificar(Proveedor proveedor)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Proveedores SET Nombre = @Nombre, RazonSocial = @RazonSocial, CUIT = @CUIT, Email = @Email, Telefono = @Telefono WHERE Id = @Id");
                datos.setearParametro("@Nombre", proveedor.Nombre);
                datos.setearParametro("@RazonSocial", proveedor.RazonSocial);
                datos.setearParametro("@CUIT", proveedor.CUIT);
                datos.setearParametro("@Email", proveedor.Email);
                datos.setearParametro("@Telefono", proveedor.Telefono);
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
                datos.setearConsulta("DELETE FROM Proveedores WHERE Id = @Id");
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
    }
}
