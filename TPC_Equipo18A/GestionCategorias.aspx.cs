using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;

namespace TPC_Equipo18A
{
    public partial class GestionCategorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Oculto paneles cuando carga por primera vez
                pnlError.Visible = false;
                pnlConfirmacion.Visible = false;
                cargarGv();
            }
        }

        protected void gvCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                // Obtengo ID del arg
                int id = int.Parse(e.CommandArgument.ToString());

                // Obtengo la descripcion de la categoria con el indice de la fila
                int rowIndex = Convert.ToInt32(((GridViewRow)((Control)e.CommandSource).NamingContainer).RowIndex);
                string descripcion = gvCategorias.Rows[rowIndex].Cells[1].Text;

                // Guardo ID en hiddenfield
                hfIdParaEliminar.Value = id.ToString();

                // Muestro panel de confirmacion
                lblConfirmarTexto.Text = "¿Está seguro que desea eliminar la categoría: " + descripcion + "?";

                // Muestro u oculto panel de confirmacion
                pnlGV.Visible = false;
                pnlConfirmacion.Visible = true;
                pnlError.Visible = false;
            }
        }

        private void cargarGv()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            gvCategorias.DataSource = negocio.listar();
            gvCategorias.DataBind();
        }

        // Confirmacion de eliminacion
        protected void btnConfirmarEliminacion_Click(object sender, EventArgs e)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            try
            {
                // 1. Leo el ID desde el hf
                int id = int.Parse(hfIdParaEliminar.Value);

                // 2. Ejecuto la eliminacion
                negocio.eliminar(id);

                // 3. Recargo la grilla
                cargarGv();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                pnlError.Visible = true;
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
            pnlError.Visible = false;
        }
    }
}
