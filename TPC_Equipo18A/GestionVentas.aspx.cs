using dominio;
using iTextSharp.text;
using iTextSharp.text.pdf;
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

            if (e.CommandName == "Imprimir")
            {
                int idVenta = int.Parse(e.CommandArgument.ToString());
                generarFacturaPDF(idVenta);
            }
        }

        private void generarFacturaPDF(int idVenta)
        {
            VentaNegocio ventaNegocio = new VentaNegocio();
            Venta venta = ventaNegocio.buscarPorIdConDetalles(idVenta);

            if (venta == null)
            {
                return;
            }

            // Config del pdf
            Document doc = new Document(PageSize.A4, 50, 50, 25, 25);
            MemoryStream ms = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, ms);

            doc.Open();

            // --- DISEÑO DEL COMPROBANTE ---

            // TITULO Y LOGO
            Paragraph titulo = new Paragraph("Comercio Grupo 18A - Comprobante de Venta\n\n", new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD));
            titulo.Alignment = Element.ALIGN_CENTER;
            doc.Add(titulo);

            // DATOS DE LA VENTA
            Paragraph datos = new Paragraph();
            datos.Font = new Font(Font.FontFamily.HELVETICA, 12);
            datos.Add($"Factura Nro: {venta.NumeroFactura}\n");
            datos.Add($"Fecha: {venta.Fecha.ToString("dd/MM/yyyy HH:mm")}\n");
            datos.Add($"Cliente: {venta.Cliente.Nombre} {venta.Cliente.Apellido}\n");
            datos.Add($"Documento: {venta.Cliente.Documento}\n"); // Si tenés este campo
            datos.Add($"Vendedor: {venta.Usuario.Username}\n\n");
            doc.Add(datos);

            // TABLA PRODUCTOS
            PdfPTable tabla = new PdfPTable(4); // 4 columnas
            tabla.WidthPercentage = 100;
            tabla.SetWidths(new float[] { 40f, 20f, 20f, 20f }); // anchos relativos

            // Encabezados de tabla
            tabla.AddCell(new PdfPCell(new Phrase("Producto", new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD))) { BackgroundColor = BaseColor.LIGHT_GRAY });
            tabla.AddCell(new PdfPCell(new Phrase("Cantidad", new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD))) { BackgroundColor = BaseColor.LIGHT_GRAY });
            tabla.AddCell(new PdfPCell(new Phrase("Precio Unit.", new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD))) { BackgroundColor = BaseColor.LIGHT_GRAY });
            tabla.AddCell(new PdfPCell(new Phrase("Subtotal", new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD))) { BackgroundColor = BaseColor.LIGHT_GRAY });

            // Filas de productos
            foreach (var item in venta.Detalles)
            {
                tabla.AddCell(new Phrase(item.Producto.Nombre, new Font(Font.FontFamily.HELVETICA, 10)));
                tabla.AddCell(new Phrase(item.Cantidad.ToString(), new Font(Font.FontFamily.HELVETICA, 10)));
                tabla.AddCell(new Phrase("$" + item.PrecioUnitario.ToString("0.00"), new Font(Font.FontFamily.HELVETICA, 10)));
                tabla.AddCell(new Phrase("$" + item.Subtotal.ToString("0.00"), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10)));
            }
            doc.Add(tabla);

            // TOTAL final
            Paragraph total = new Paragraph($"\nTOTAL: ${venta.Total.ToString("0.00")}", new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD));
            total.Alignment = Element.ALIGN_RIGHT;
            doc.Add(total);

            doc.Close();

            // Descargar el archivo
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", $"attachment;filename=Factura-{venta.NumeroFactura}.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(ms.ToArray());
            Response.End();
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