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
        ProveedorNegocio proveedorNegocio = new ProveedorNegocio();

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

                // Carga Proveedores
                ddlProveedores.DataSource = proveedorNegocio.listar();
                ddlProveedores.DataTextField = "RazonSocial";
                ddlProveedores.DataValueField = "Id";
                ddlProveedores.DataBind();
                ddlProveedores.Items.Insert(0, new ListItem("Seleccione Proveedor...", "0"));

                // Verifico Edicion o alta
                if (Request.QueryString["Id"] != null)
                {
                    lblTitulo.Text = "Editar Producto";

                    int id = int.Parse(Request.QueryString["Id"]);
                    Producto productoSeleccionado = productoNegocio.buscarPorId(id);

                    if (productoSeleccionado != null)
                    {
                        hfId.Value = productoSeleccionado.Id.ToString();
                        txtCodigo.Text = productoSeleccionado.Codigo;
                        txtNombre.Text = productoSeleccionado.Nombre;
                        txtDescripcion.Text = productoSeleccionado.Descripcion;
                        txtStockActual.Text = productoSeleccionado.StockActual.ToString();
                        ddlMarca.SelectedValue = productoSeleccionado.Marca.Id.ToString();
                        ddlCategoria.SelectedValue = productoSeleccionado.Categoria.Id.ToString();
                        txtStockMinimo.Text = productoSeleccionado.StockMinimo.ToString();
                        txtPorcentajeGanancia.Text = productoSeleccionado.PorcentajeGanancia.ToString("0.00");

                        // Config del panel de proveedores - visible en edicion
                        pnlProveedores.Visible = true;
                        lblMensajeProveedores.Visible = false;
                        cargarProveedoresAsignados(id);
                    }
                }
                else
                {
                    // Alta: oculto panel de proveedores
                    pnlProveedores.Visible = false;
                    lblMensajeProveedores.Visible = true;
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
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
                    producto.Activo = true;

                    // Validación numérica
                    if (!decimal.TryParse(txtPorcentajeGanancia.Text, out decimal porcentaje) ||
                        !int.TryParse(txtStockActual.Text, out int stock) ||
                        !int.TryParse(txtStockMinimo.Text, out int stockMinimo))
                    {
                        mostrarToast("Error en formato de campos numéricos.", "danger");
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

                    Response.Redirect("GestionProductos.aspx", false);
                }
            }
            catch (Exception ex)
            {
                mostrarToast("Error al guardar el producto: " + ex.Message, "danger");
            }
        }


        private void cargarProveedoresAsignados(int idProducto)
        {
            List<Proveedor> lista = productoNegocio.listarProveedoresPorProducto(idProducto);
            gvProveedoresProducto.DataSource = lista;
            gvProveedoresProducto.DataBind();
        }

        protected void btnAgregarProveedor_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que haya seleccionado algo
                if (ddlProveedores.SelectedValue == "0")
                {
                    mostrarToast("Seleccione un proveedor válido.", "warning");
                    return;
                }

                int idProducto = int.Parse(hfId.Value);
                int idProveedor = int.Parse(ddlProveedores.SelectedValue);

                productoNegocio.vincularProveedor(idProducto, idProveedor);
                cargarProveedoresAsignados(idProducto);

                mostrarToast("Proveedor vinculado correctamente.", "success");
            }
            catch (Exception ex)
            {
                mostrarToast("Error al vincular: " + ex.Message, "danger");
            }
        }

        protected void gvProveedoresProducto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                try
                {
                    int idProveedor = int.Parse(e.CommandArgument.ToString());
                    int idProducto = int.Parse(hfId.Value);

                    productoNegocio.desvincularProveedor(idProducto, idProveedor);
                    cargarProveedoresAsignados(idProducto);

                    mostrarToast("Proveedor desvinculado.", "warning");
                }
                catch (Exception ex)
                {
                    mostrarToast("Error al desvincular: " + ex.Message, "danger");
                }
            }
        }

        private void mostrarToast(string mensaje, string tipo)
        {
            string script = $"mostrarToast('{mensaje}', '{tipo}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ToastJS", script, true);
        }
    }
}