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
        ProductoNegocio productoNegocio = new ProductoNegocio();
        MarcaNegocio marcaNegocio = new MarcaNegocio();
        CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlMarca.DataSource = marcaNegocio.listar();
                ddlMarca.DataTextField = "Descripcion";
                ddlMarca.DataValueField = "Id";
                ddlMarca.DataBind();
                ddlMarca.Items.Insert(0, new ListItem("Seleccione una Marca", ""));

                ddlCategoria.DataSource = categoriaNegocio.listar();
                ddlCategoria.DataTextField = "Descripcion";
                ddlCategoria.DataValueField = "Id";
                ddlCategoria.DataBind();
                ddlCategoria.Items.Insert(0, new ListItem("Seleccione una Categoría", ""));

                if (Request.QueryString["Id"] != null)
                {
                    lblTitulo.Text = "Editar Producto";

                    int id = int.Parse(Request.QueryString["Id"]);
                    Producto productoSeleccionado = productoNegocio.buscarPorId(id);

                    txtCodigo.Text = productoSeleccionado.Codigo;
                    txtNombre.Text = productoSeleccionado.Nombre;
                    txtDescripcion.Text = productoSeleccionado.Descripcion;
                    txtStockActual.Text = productoSeleccionado.StockActual.ToString();
                    ddlMarca.SelectedValue = productoSeleccionado.Marca.Id.ToString();
                    ddlCategoria.SelectedValue = productoSeleccionado.Categoria.Id.ToString();
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                Producto producto = new Producto();
                producto.Codigo = txtCodigo.Text;
                producto.Nombre = txtNombre.Text;
                producto.Descripcion = txtDescripcion.Text;
                producto.Marca = new Marca { Id = int.Parse(ddlMarca.SelectedValue) };
                producto.Categoria = new Categoria { Id = int.Parse(ddlCategoria.SelectedValue) };

                if (!decimal.TryParse(txtPrecio.Text, out decimal precio) ||
                    !int.TryParse(txtStockActual.Text, out int stock))
                {
                    //Validación fallida
                    return;
                }

                producto.Precio = precio;
                producto.StockActual = stock;
                if (Request.QueryString["Id"] != null)
                {
                    producto.Id = int.Parse(Request.QueryString["Id"]);
                    productoNegocio.modificar(producto);
                }
                else
                {
                    productoNegocio.agregar(producto);
                }
                Response.Redirect("GestionProductos.aspx");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }
    }
}