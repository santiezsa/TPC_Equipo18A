using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Equipo18A
{
    public partial class MiMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Valido sesion
            if (Session["usuario"] == null)
            {
                // Nadie logeado y no estoy en paginas permitidas
                if (!(Page is Login || Page is Error))
                {
                    Response.Redirect("Login.aspx", false);
                    return;
                }
            }

            // Configuro menu segun perfil
            if (Session["usuario"] != null)
            {
                Usuario user = (Usuario)Session["usuario"];

                // Cargo datos en topbar
                lblNombreUsuario.Text = user.Username;
                lblPerfilUsuario.Text = user.Perfil.ToString();
                lblDropdownHeader.Text = "Hola, " + user.Username;

                // Logica para vendedor
                if (user.Perfil == Perfil.Vendedor)
                {
                    // Oculto menus de admin al venededor
                    lnkProductos.Visible = false;
                    lnkMarcas.Visible = false;
                    lnkCategorias.Visible = false;
                    lnkProveedores.Visible = false;
                    lnkRegistrarCompra.Visible = false;
                    lnkGestionUsuarios.Visible = false;
                }
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx"); // para volver al login
        }
    }
}