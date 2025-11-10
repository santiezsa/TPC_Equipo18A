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
    public partial class GestionClientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                pnlConfirmacion.Visible = false;
                //mostrarToast("prueba","success");
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
            ClienteNegocio negocio = new ClienteNegocio();
            try
            {
                gvClientes.DataSource = negocio.listar();
                gvClientes.DataBind();
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
                // Leo ID del cliente a eliminar
                int id = int.Parse(hfIdParaEliminar.Value);
                ClienteNegocio negocio = new ClienteNegocio();
                negocio.eliminar(id);
                cargarGv();

                // Mensaje de exito
                mostrarToast("Cliente eliminado correctamente.", "success");
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

        protected void gvClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                // Id del cliente a eliminar
                int id = int.Parse(e.CommandArgument.ToString());

                // Nombre del cliente a eliminar
                int rowIndex = Convert.ToInt32(((GridViewRow)((Control)e.CommandSource).NamingContainer).RowIndex);
                string nombre = gvClientes.Rows[rowIndex].Cells[0].Text;

                // Guardo ID
                hfIdParaEliminar.Value = id.ToString();
                // Muestro panel de confirmacion
                lblConfirmarTexto.Text = $"¿Está seguro que desea eliminar el cliente <strong>'{nombre}'</strong>?";

                // Muestro u oculto paneles
                pnlGV.Visible = false;
                pnlConfirmacion.Visible = true;
            }
        }

    }
}