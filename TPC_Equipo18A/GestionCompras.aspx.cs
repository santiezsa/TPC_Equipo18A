using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Equipo18A
{
    public partial class GestionCompras : System.Web.UI.Page
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
            CompraNegocio negocio = new CompraNegocio();
            try
            {
                gvCompras.DataSource = negocio.listar();
                gvCompras.DataBind();
            }
            catch (Exception ex)
            {
                mostrarToast("Error: " + ex.Message, "danger");
            }
        }

        protected void gvCompras_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCompras.PageIndex = e.NewPageIndex;
            cargarGv();
        }

        protected void gvCompras_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Obtengo compra y sus detalles de cada fila
                Compra compra = (Compra)e.Row.DataItem;

                // Busco btn anular
                Button btn = (Button)e.Row.FindControl("btnAnular");

                // Si la venta NO esta activa oculto el boton
                if (!compra.Activo)
                {
                    btn.Visible = false;
                    e.Row.CssClass = "table-secondary text-muted";
                }
            }
        }

        protected void gvCompras_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Anular")
            {
                int id = int.Parse(e.CommandArgument.ToString());

                hfIdParaAnular.Value = id.ToString();
                lblConfirmarTexto.Text = $"¿Está seguro que desea ANULAR la compra ?";

                pnlGV.Visible = false;
                pnlConfirmacion.Visible = true;
            }
        }

        protected void btnConfirmarAnulacion_Click(object sender, EventArgs e)
        {
            try
            {
                int idCompra = int.Parse(hfIdParaAnular.Value);
                CompraNegocio negocio = new CompraNegocio();

                // Obtengo id del usuario logeado
                Usuario usuarioLogueado = (Usuario)Session["usuario"];
                int idUsuario = usuarioLogueado.Id;

                // Anulo venta con usuario como responsable
                negocio.anular(idCompra, idUsuario);

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
        protected void btnExportarExcel_Click(object sender, EventArgs e)
        {
            // Desactivo paginación
            gvCompras.AllowPaging = false;

            // Vuelvo a cargar los datos completos
            CompraNegocio negocio = new CompraNegocio();
            List<Compra> compras = negocio.listar();
            gvCompras.DataSource = compras;
            gvCompras.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=ListadoCompras.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            using (StringWriter sw = new StringWriter())
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                gvCompras.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        private void mostrarToast(string mensaje, string tipo)
        {
            string script = $"mostrarToast('{mensaje}', '{tipo}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ToastJS", script, true);
        }
    }
}