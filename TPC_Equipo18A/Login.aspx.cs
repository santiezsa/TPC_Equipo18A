using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Equipo18A
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
            {
                return;
            }
            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario usuario = new Usuario();

            try
            {
                // datos del form
                usuario.Username = txtUsername.Text;
                usuario.Password = txtPassword.Text;

                // logueo
                Usuario usuarioLogueado = negocio.loguear(usuario);

                // verifico resultado
                if (usuarioLogueado != null)
                {
                    // loguea exitosamente
                    Session["usuario"] = usuarioLogueado;

                    // redireccion a pagina principal
                    Response.Redirect("Default.aspx", false);
                }
                else
                {
                    // user/pass mal, o usuario inactivo
                    lblError.Text = "Usuario o contraseña incorrecta.";
                    pnlError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                // Error de base u otro
                lblError.Text = "Error: " + ex.Message;
                pnlError.Visible = true;
            }
        }
    }
}