using dominio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace negocio
{
    public class ArmadorDocumentos
    {
        public MemoryStream GenerarFacturaPDF(Venta venta)
        {
            // Config del pdf
            Document doc = new Document(PageSize.A4, 50, 50, 25, 25);
            MemoryStream ms = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, ms);

            doc.Open();

            // --- DISEÑO DEL COMPROBANTE ---

            // TITULO Y LOGO
            Paragraph titulo = new Paragraph("Mi Negocio - Factura", new Font(Font.FontFamily.HELVETICA, 20, Font.BOLD));
            titulo.Alignment = Element.ALIGN_CENTER;
            titulo.SpacingAfter = 20f;
            doc.Add(titulo);

            // DATOS DE LA VENTA
            Paragraph datos = new Paragraph();
            datos.Font = new Font(Font.FontFamily.HELVETICA, 12);
            datos.SpacingAfter = 20f;

            datos.Add($"Factura Nro: {venta.NumeroFactura}\n");
            datos.Add($"Fecha: {venta.Fecha.ToString("dd/MM/yyyy HH:mm")}\n");
            string nombreCliente = venta.Cliente != null ? $"{venta.Cliente.Apellido}, {venta.Cliente.Nombre}" : "Consumidor Final";
            datos.Add($"Cliente: {nombreCliente}\n");
            datos.Add($"Documento: {venta.Cliente.Documento}\n"); 

            doc.Add(datos);

            // PRODUCTOS
            PdfPTable tabla = new PdfPTable(4); // 4 columnas
            tabla.WidthPercentage = 100;
            tabla.SetWidths(new float[] { 40f, 20f, 20f, 20f }); // Anchos relativos

            // ESTILOS DE CELDAS
            var fontBold = new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD);
            var fontNormal = new Font(Font.FontFamily.HELVETICA, 10);
            PdfPCell celdaHeader = new PdfPCell() { BackgroundColor = BaseColor.LIGHT_GRAY, Padding = 5 };

            // ENCABEZADOS
            celdaHeader.Phrase = new Phrase("Producto", fontBold); tabla.AddCell(celdaHeader);
            celdaHeader.Phrase = new Phrase("Cantidad", fontBold); tabla.AddCell(celdaHeader);
            celdaHeader.Phrase = new Phrase("Precio Unit.", fontBold); tabla.AddCell(celdaHeader);
            celdaHeader.Phrase = new Phrase("Subtotal", fontBold); tabla.AddCell(celdaHeader);

            // FILAS
            foreach (var item in venta.Detalles)
            {
                tabla.AddCell(new Phrase(item.Producto.Nombre, fontNormal));
                tabla.AddCell(new Phrase(item.Cantidad.ToString(), fontNormal));
                tabla.AddCell(new Phrase("$" + item.PrecioUnitario.ToString("0.00"), fontNormal));
                tabla.AddCell(new Phrase("$" + item.Subtotal.ToString("0.00"), fontNormal));
            }
            doc.Add(tabla);

            // TOTAL final
            Paragraph total = new Paragraph($"\nTOTAL: ${venta.Total.ToString("0.00")}", new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD));
            total.Alignment = Element.ALIGN_RIGHT;
            doc.Add(total);

            doc.Close();
            return ms;
        }
    }
}
