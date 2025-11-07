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
    public partial class FormularioProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    lblTitulo.Text = "Editar Producto";

                    int id = int.Parse(Request.QueryString["Id"]);
                    ProductoNegocio negocio = new ProductoNegocio();
                    Producto productoSeleccionado = negocio.buscarPorId(id);

                    txtDescripcion.Text = productoSeleccionado.Descripcion;

                }
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                Producto producto = new Producto();
                ProductoNegocio negocio = new ProductoNegocio();
                producto.Descripcion = txtDescripcion.Text;
                if (Request.QueryString["Id"] != null)
                {
                    // Modificación
                    producto.Id = int.Parse(Request.QueryString["Id"]);
                    negocio.modificar(producto);
                }
                else
                {
                    // Alta
                    negocio.agregar(producto);
                }
                Response.Redirect("GestionProductos.aspx");
            }
        }
    }
}