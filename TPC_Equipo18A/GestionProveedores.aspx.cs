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
    public partial class GestionProveedores : System.Web.UI.Page
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
            ProveedorNegocio negocio = new ProveedorNegocio();
            try
            {
                gvProveedores.DataSource = negocio.listar();
                gvProveedores.DataBind();
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
                // Leo ID del proveedor a eliminar
                int id = int.Parse(hfIdParaEliminar.Value);
                ProveedorNegocio negocio = new ProveedorNegocio();
                negocio.eliminar(id);
                cargarGv();

                // Mensaje de exito
                mostrarToast("Proveedor eliminado correctamente.", "success");
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

        protected void gvProveedores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                // Id del proveedor a eliminar
                int id = int.Parse(e.CommandArgument.ToString());

                // Razon social del prov a eliminar
                int rowIndex = Convert.ToInt32(((GridViewRow)((Control)e.CommandSource).NamingContainer).RowIndex);
                string razonSocial = gvProveedores.Rows[rowIndex].Cells[0].Text;

                // Guardo ID
                hfIdParaEliminar.Value = id.ToString();
                // Muestro panel de confirmacion
                lblConfirmarTexto.Text = $"¿Está seguro que desea eliminar al proveedor <strong>'{razonSocial}'</strong>?";

                // Muestro u oculto paneles
                pnlGV.Visible = false;
                pnlConfirmacion.Visible = true;
            }
        }
    }
}