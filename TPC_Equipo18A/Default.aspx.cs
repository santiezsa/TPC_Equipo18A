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
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarStockBajo();
                cargarVentasRecientes();
            }
        }

        private void cargarStockBajo()
        {
            ProductoNegocio productoNegocio = new ProductoNegocio();
            List<Producto> lista = productoNegocio.listarStockBajo();

            rptStockBajo.DataSource = lista;
            rptStockBajo.DataBind();
        }
        private void cargarVentasRecientes()
        {
            VentaNegocio negocio = new VentaNegocio();
            List<Venta> ventas = negocio.ListarUltimasVentas(5);

            repVentasRecientes.DataSource = ventas;
            repVentasRecientes.DataBind();
        }
    }
}