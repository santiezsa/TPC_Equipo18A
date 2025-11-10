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
    public partial class GestionUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlConfirmacion.Visible = false;
                cargarGv();
            }
        }

        private void mostrarToast(string mensaje, string tipo)
        {
            string script = $"mostrarToast('{mensaje}', '{tipo}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ToastJS", script, true);
        }

        private void cargarGv()
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            try
            {
                gvUsuarios.DataSource = negocio.listar();
                gvUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                Session["error"] = ex.Message;
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnConfirmarEliminacion_Click(object sender, EventArgs e)
        {
            try
            {
                // Leo ID del usuario a eliminar
                int id = int.Parse(hfIdParaEliminar.Value);
                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.eliminar(id);
                cargarGv();

                // Mensaje de exito
                mostrarToast("Usuario eliminado correctamente.", "success");
            }
            catch (Exception ex)
            {
                mostrarToast(ex.Message, "danger");
            }
            finally
            {
                pnlConfirmacion.Visible = false;
                pnlGV.Visible = true;
            }
        }

        protected void btnCancelarEliminacion_Click(object sender, EventArgs e)
        {
            // Oculto panel y muestro gv
            pnlConfirmacion.Visible = false;
            pnlGV.Visible = true;
        }

        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                // Id del usuario a eliminar
                int id = int.Parse(e.CommandArgument.ToString());

                // Username del usuario a eliminar (asumiendo que está en la Celda 1)
                int rowIndex = Convert.ToInt32(((GridViewRow)((Control)e.CommandSource).NamingContainer).RowIndex);
                string username = gvUsuarios.Rows[rowIndex].Cells[1].Text;

                // Guardo ID
                hfIdParaEliminar.Value = id.ToString();

                // Muestro panel de confirmacion
                lblConfirmarTexto.Text = $"¿Está seguro que desea eliminar al usuario <strong>'{username}'</strong>?";

                // Muestro u oculto paneles
                pnlGV.Visible = false;
                pnlConfirmacion.Visible = true;
            }
        }
    }
}