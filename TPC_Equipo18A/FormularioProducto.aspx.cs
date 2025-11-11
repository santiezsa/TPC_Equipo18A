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

                    hfId.Value = productoSeleccionado.Id.ToString();
                    txtCodigo.Text = productoSeleccionado.Codigo;
                    txtNombre.Text = productoSeleccionado.Nombre;
                    txtDescripcion.Text = productoSeleccionado.Descripcion;
                    txtStockActual.Text = productoSeleccionado.StockActual.ToString();
                    ddlMarca.SelectedValue = productoSeleccionado.Marca.Id.ToString();
                    ddlCategoria.SelectedValue = productoSeleccionado.Categoria.Id.ToString();
                    txtStockMinimo.Text = productoSeleccionado.StockMinimo.ToString();
                    txtPorcentajeGanancia.Text = productoSeleccionado.PorcentajeGanancia.ToString();
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

                if (!decimal.TryParse(txtPorcentajeGanancia.Text, out decimal porcentaje) ||
                                    !int.TryParse(txtStockActual.Text, out int stock) ||
                                    !int.TryParse(txtStockMinimo.Text, out int stockMinimo))
                {
                    mostrarToast("Error en formato de campos numéricos (Porcentaje, Stock).", "danger");
                    return;
                }

                producto.PorcentajeGanancia = porcentaje;
                producto.StockActual = stock;
                producto.StockMinimo = stockMinimo;

                if (Request.QueryString["Id"] != null)
                {
                    producto.Id = int.Parse(hfId.Value);
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

        private void mostrarToast(string mensaje, string tipo)
        {
            string script = $"mostrarToast('{mensaje}', '{tipo}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ToastJS", script, true);
        }
    }
}