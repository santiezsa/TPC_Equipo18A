using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;

namespace TPC_Equipo18A
{
    public partial class GestionReportes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFechaDesde.Text = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                txtFechaHasta.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            DateTime fechaDesdeParsed, fechaHastaParsed;

            DateTime? desde = DateTime.TryParse(txtFechaDesde.Text, out fechaDesdeParsed)
                ? (DateTime?)fechaDesdeParsed
                : null;

            DateTime? hasta = DateTime.TryParse(txtFechaHasta.Text, out fechaHastaParsed)
                ? (DateTime?)fechaHastaParsed
                : null;

            var negocio = new ReporteNegocio();
            List<object> lista = null;

            switch (ddlTipoReporte.SelectedValue)
            {
                case "VentasPeriodo":
                    lista = negocio.ReporteVentasPorPeriodo(desde, hasta);
                    break;

                case "VentasProducto":
                    lista = negocio.ReporteVentasPorProducto(desde, hasta);
                    break;

                case "VentasCliente":
                    lista = negocio.ReporteVentasPorCliente(desde, hasta);
                    break;

                case "ComprasProveedor":
                    lista = negocio.ReporteComprasPorProveedor(desde, hasta);
                    break;

                case "StockMinimo":
                    int minimo = int.Parse(txtStockMinimo.Text);
                    lista = negocio.ReporteStockBajoMinimo(minimo);
                    break;
            }

            gvReporte.DataSource = lista;
            gvReporte.DataBind();

            Session["ReporteActual"] = lista;
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            var lista = Session["ReporteActual"] as List<object>;
            if (lista == null || lista.Count == 0)
                return;

            GridView gv = new GridView();
            gv.DataSource = lista;
            gv.DataBind();

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=Reporte.xls");
            Response.ContentType = "application/excel";

            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

            gv.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }
    }
}