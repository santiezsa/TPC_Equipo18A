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
    public partial class GestionVentas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlConfirmacion.Visible = false;
                cargarGv();
            }
        }

        private void cargarGv()
        {
            VentaNegocio negocio = new VentaNegocio();
            try
            {
                gvVentas.DataSource = negocio.listar();
                gvVentas.DataBind();
            }
            catch (Exception ex)
            {
                mostrarToast("Error: " + ex.Message, "danger");
            }
        }

        protected void gvVentas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Obtengo venta y sus detalles de cada fila
                Venta venta = (Venta)e.Row.DataItem;

                // Busco btn anular
                Button btn = (Button)e.Row.FindControl("btnAnular");

                // Si la venta NO esta activa oculto el boton
                if (!venta.Activo)
                {
                    btn.Visible = false;
                    e.Row.CssClass = "table-secondary text-muted";
                }
            }
        }

        protected void gvVentas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Anular")
            {
                int id = int.Parse(e.CommandArgument.ToString());

                // Obtengo el nro de factura para mostrar (Celda 0)
                int rowIndex = Convert.ToInt32(((GridViewRow)((Control)e.CommandSource).NamingContainer).RowIndex);
                string factura = gvVentas.Rows[rowIndex].Cells[0].Text;

                hfIdParaAnular.Value = id.ToString();
                lblConfirmarTexto.Text = $"¿Está seguro que desea ANULAR la venta <strong>{factura}</strong>?";

                pnlGV.Visible = false;
                pnlConfirmacion.Visible = true;
            }
        }

        protected void btnConfirmarAnulacion_Click(object sender, EventArgs e)
        {
            try
            {
                int idVenta = int.Parse(hfIdParaAnular.Value);
                VentaNegocio negocio = new VentaNegocio();

                // Obtengo id del usuario logeado
                Usuario usuarioLogueado = (Usuario)Session["usuario"];
                int idUsuario = usuarioLogueado.Id;

                // Anulo venta con usuario como responsable
                negocio.anular(idVenta, idUsuario);

                cargarGv();
                mostrarToast("Venta anulada y stock restaurado.", "success");
            }
            catch (Exception ex)
            {
                mostrarToast("Error al anular: " + ex.Message, "danger");
            }
            finally
            {
                pnlConfirmacion.Visible = false;
                pnlGV.Visible = true;
            }
        }

        protected void btnCancelarAnulacion_Click(object sender, EventArgs e)
        {
            pnlConfirmacion.Visible = false;
            pnlGV.Visible = true;
        }

        private void mostrarToast(string mensaje, string tipo)
        {
            string script = $"mostrarToast('{mensaje}', '{tipo}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ToastJS", script, true);
        }


    }
}