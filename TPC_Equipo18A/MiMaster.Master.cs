using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace TPC_Equipo18A
{
    public partial class MiMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Si no hay session
            if (!(Page is Login || Page is RecuperarPass || Page is Error) && Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx", false);
                return;
            }

            if (Session["usuario"] != null)
            {
                Usuario user = (Usuario)Session["usuario"];

                // Carga de datos
                lblNombreUsuario.Text = user.Username;
                lblPerfilUsuario.Text = user.Perfil.ToString();
                lblDropdownHeader.Text = "Hola, " + user.Username;

                // Gestion de permisos (vendedor)
                if (user.Perfil == Perfil.Vendedor)
                {
                    // Ocutlo ABMs administrativos
                    lnkProductos.Visible = false;
                    lnkMarcas.Visible = false;
                    lnkCategorias.Visible = false;
                    lnkProveedores.Visible = false;
                    lnkClientes.Visible = true;

                    // Oculta compras y reportes
                    lnkRegistrarCompra.Visible = false;
                    lnkReportes.Visible = false;
                    lnkGestionUsuarios.Visible = false;
                    lnkGestionVentas.Visible = false;
                }

                cargarNotificaciones();
            }
        }

        private void cargarNotificaciones()
        {
            try
            {
                ProductoNegocio negocio = new ProductoNegocio();
                List<Producto> listaAlertas = negocio.listarStockBajo();
                int cantidad = listaAlertas.Count;

                if (cantidad > 0)
                {
                    lblCantNotificaciones.Text = cantidad.ToString();
                    lblCantNotificaciones.Visible = true;

                    rptNotificaciones.DataSource = listaAlertas;
                    rptNotificaciones.DataBind();

                    pnlSinNotificaciones.Visible = false;
                }
                else
                {
                    lblCantNotificaciones.Visible = false;

                    rptNotificaciones.DataSource = null;
                    rptNotificaciones.DataBind();

                    pnlSinNotificaciones.Visible = true;
                }
            }
            catch (Exception)
            {
                // Oculto si hay error
                lblCantNotificaciones.Visible = false;
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}