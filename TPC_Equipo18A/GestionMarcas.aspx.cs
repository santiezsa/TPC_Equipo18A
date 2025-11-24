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
    public partial class GestionMarcas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Oculto paneles cuando carga por primera vez
                pnlConfirmacion.Visible = false;
                cargarGv();
            }
        }

        private void mostrarToast(string mensaje, string tipo)
        {
            string script = $"mostrarToast('{mensaje}', '{tipo}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ToastJS", script, true);
        }

        protected void gvMarcas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                // Obtengo ID del arg
                int id = int.Parse(e.CommandArgument.ToString());

                // Obtengo la descripcion de la marca con el indice de la fila
                int rowIndex = Convert.ToInt32(((GridViewRow)((Control)e.CommandSource).NamingContainer).RowIndex);
                string descripcion = gvMarcas.Rows[rowIndex].Cells[1].Text;

                // Guardo ID en hiddenfield
                hfIdParaEliminar.Value = id.ToString();

                // Muestro panel de confirmacion
                lblConfirmarTexto.Text = "¿Está seguro que desea eliminar la marca: " + descripcion + "?";

                // Muestro u oculto panel de confirmacion
                pnlGV.Visible = false;
                pnlConfirmacion.Visible = true;
            }
        }

        private void cargarGv()
        {
            MarcaNegocio negocio = new MarcaNegocio();
            gvMarcas.DataSource = negocio.listar();
            gvMarcas.DataBind();
        }

        // Confirmacion de eliminacion
        protected void btnConfirmarEliminacion_Click(object sender, EventArgs e)
        {
            MarcaNegocio negocio = new MarcaNegocio();
            try
            {
                // Leo el ID desde el hf
                int id = int.Parse(hfIdParaEliminar.Value);

                // Ejecuto la eliminacion
                negocio.eliminar(id);

                // Recargo la grilla
                cargarGv();

                // Mensaje de exito
                mostrarToast("Categoría eliminada exitosamente.", "success");
            }
            catch (Exception ex)
            {
                mostrarToast(ex.Message, "danger");
            }
            finally
            {
                // Oculto panel de confirmacion y muestro grilla
                pnlConfirmacion.Visible = false;
                pnlGV.Visible = true;
            }
        }

        protected void btnCancelarEliminacion_Click(object sender, EventArgs e)
        {
            // Oculto panel de confirmacion y muestro grilla
            pnlConfirmacion.Visible = false;
            pnlGV.Visible = true;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            filtrar();
        }
        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void filtrar()
        {
            MarcaNegocio negocio = new MarcaNegocio();
            try
            {
                if (txtFiltro.Text.Length > 1)
                    gvMarcas.DataSource = negocio.filtrar(txtFiltro.Text);
                else
                    gvMarcas.DataSource = negocio.listar();

                gvMarcas.DataBind();
            }
            catch (Exception ex) { mostrarToast("Error al filtrar: " + ex.Message, "danger"); }
        }
    }
}