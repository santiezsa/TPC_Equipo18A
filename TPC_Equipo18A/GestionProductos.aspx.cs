using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;

namespace TPC_Equipo18A
{
    public partial class GestionProductos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrid();
            }
        }

        private void CargarGrid()
        {
            ProductoNegocio negocio = new ProductoNegocio();
            List<Producto> listaProductos = negocio.listar();
            gvProductos.DataSource = listaProductos;
            gvProductos.DataBind();
        }
    }
}