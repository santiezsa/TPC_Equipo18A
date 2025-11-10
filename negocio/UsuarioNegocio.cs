using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class UsuarioNegocio
    {
        public List<Usuario> listar()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Id, Username, Password, IdPerfil, Activo FROM USUARIOS WHERE Activo = 1");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Username = (string)datos.Lector["Username"];
                    aux.Password = (string)datos.Lector["Password"];
                    aux.Perfil = (Perfil)(int)datos.Lector["IdPerfil"];
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

        public Usuario buscarPorId(int id)
        {
            Usuario usuario = null;
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Id, Username, Password, IdPerfil, Activo FROM Usuarios WHERE Id = @Id");
                datos.setearParametro("@Id", id);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    usuario = new Usuario();
                    usuario.Id = (int)datos.Lector["Id"];
                    usuario.Username = (string)datos.Lector["Username"];
                    usuario.Password = (string)datos.Lector["Password"];
                    usuario.Perfil = (Perfil)(int)datos.Lector["IdPerfil"];
                    usuario.Activo = (bool)datos.Lector["Activo"];
                }
                return usuario;
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

        public void agregar(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Usuarios (Username, Password, IdPerfil, Activo) VALUES (@Username, @Password, @IdPerfil, 1)");
                datos.setearParametro("@Username", nuevo.Username);
                datos.setearParametro("@Password", nuevo.Password);
                datos.setearParametro("@IdPerfil", (int)nuevo.Perfil);
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

        public void modificar(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Usuarios SET Username = @Username, Password = @Password, IdPerfil = @IdPerfil WHERE Id = @Id");
                datos.setearParametro("@Username", usuario.Username);
                datos.setearParametro("@Password", usuario.Password);
                datos.setearParametro("@IdPerfil", (int)usuario.Perfil);
                datos.setearParametro("@Id", usuario.Id);
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
                // Baja logica
                datos.setearConsulta("UPDATE Usuarios SET Activo = 0 WHERE Id = @Id");

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
