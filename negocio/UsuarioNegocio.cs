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

        public Usuario loguear(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Busco usuario que coincida con el username y password y activo = 1
                datos.setearConsulta("SELECT Id, IdPerfil, Username, Activo FROM Usuarios WHERE Username = @user AND Password = @pass AND Activo = 1");
                datos.setearParametro("@user", usuario.Username);
                datos.setearParametro("@pass", usuario.Password);

                datos.ejecutarLectura();

                // Si el lector encuentra una fila, login exitoso
                if (datos.Lector.Read())
                {
                    Usuario usuarioLogueado = new Usuario();
                    usuarioLogueado.Id = (int)datos.Lector["Id"];
                    usuarioLogueado.Username = (string)datos.Lector["Username"];
                    usuarioLogueado.Perfil = (Perfil)(int)datos.Lector["IdPerfil"];
                    usuarioLogueado.Activo = (bool)datos.Lector["Activo"];
                    return usuarioLogueado;
                }
                else
                {
                    return null; // Login fallido
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

        // Verifica si existe un usuario con ese email
        public bool existeEmail(string email)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM Usuarios WHERE Email = @email AND Activo = 1");
                datos.setearParametro("@email", email);

                int cantidad = (int)datos.ejecutarLecturaScalar();

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

        public void actualizarPassword(string email, string nuevaPassword)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Usuarios SET Password = @pass WHERE Email = @email");
                datos.setearParametro("@pass", nuevaPassword);
                datos.setearParametro("@email", email);
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
