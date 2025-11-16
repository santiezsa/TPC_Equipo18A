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
                datos.setearConsulta("SELECT Id, RazonSocial, CUIT, Email, Telefono, Activo FROM Proveedores WHERE Activo = 1");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Proveedor aux = new Proveedor();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.RazonSocial = (string)datos.Lector["RazonSocial"];
                    aux.CUIT = (string)datos.Lector["CUIT"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.Telefono = (string)datos.Lector["Telefono"];
                    aux.Activo = (bool)datos.Lector["Activo"];
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
                datos.setearConsulta("SELECT Id, RazonSocial, CUIT, Email, Telefono, Activo FROM Proveedores WHERE Id = @Id");
                datos.setearParametro("@Id", id);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    proveedor = new Proveedor();

                    proveedor.Id = (int)datos.Lector["Id"];
                    proveedor.RazonSocial = (string)datos.Lector["RazonSocial"];
                    proveedor.CUIT = (string)datos.Lector["CUIT"];
                    proveedor.Email = (string)datos.Lector["Email"];
                    proveedor.Telefono = (string)datos.Lector["Telefono"];
                    proveedor.Activo = (bool)datos.Lector["Activo"];
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
                datos.setearConsulta("INSERT INTO Proveedores (RazonSocial, CUIT, Email, Telefono, Activo) VALUES (@RazonSocial, @CUIT, @Email, @Telefono, 1)");
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
                datos.setearConsulta("UPDATE Proveedores SET RazonSocial = @RazonSocial, CUIT = @CUIT, Email = @Email, Telefono = @Telefono WHERE Id = @Id");
                datos.setearParametro("@Id", proveedor.Id);
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
                datos.setearConsulta("UPDATE Proveedores SET Activo = 0 WHERE Id = @Id");
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
