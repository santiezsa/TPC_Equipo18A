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
    public partial class RegistrarCompra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarCombos();
                if (Session["listaCompra"] == null)
                {
                    Session["listaCompra"] = new List<DetalleCompra>();

                    cargarCombos();
                }
            }
        }

        private void cargarCombos()
        {
            ProveedorNegocio proveedorNegocio = new ProveedorNegocio();
            ddlProveedor.DataSource = proveedorNegocio.listar();
            ddlProveedor.DataTextField = "RazonSocial";
            ddlProveedor.DataValueField = "Id";
            ddlProveedor.DataBind();
            ddlProveedor.Items.Insert(0, new ListItem("Seleccione Proveedor", ""));

            ProductoNegocio productoNegocio = new ProductoNegocio();
            ddlProducto.DataSource = productoNegocio.listar();
            ddlProducto.DataTextField = "Nombre";
            ddlProducto.DataValueField = "Id";
            ddlProducto.DataBind();
            ddlProducto.Items.Insert(0, new ListItem("Seleccione Producto", ""));
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones
                if (ddlProducto.SelectedIndex == 0 || txtCantidad.Text == "" || txtPrecio.Text == "")
                {
                    mostrarToast("Complete todos los campos del producto", "warning");
                    return;
                }

                // Datos del producto seleccionado
                int idProducto = int.Parse(ddlProducto.SelectedValue);
                ProductoNegocio prodNegocio = new ProductoNegocio();
                Producto productoSeleccionado = prodNegocio.buscarPorId(idProducto);

                // Creo detalle
                DetalleCompra detalle = new DetalleCompra();
                detalle.Producto = productoSeleccionado;
                detalle.Cantidad = int.Parse(txtCantidad.Text);
                detalle.PrecioUnitario = decimal.Parse(txtPrecio.Text); // Precio de costo

                // Agrego a la lista en la session
                List<DetalleCompra> lista = (List<DetalleCompra>)Session["listaCompra"];
                lista.Add(detalle);
                Session["listaCompra"] = lista;

                // Refrescar gv
                cargarGvDetalle();

                // Limpio campos de carga
                txtCantidad.Text = "";
                txtPrecio.Text = "";
                ddlProducto.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                mostrarToast("Error al agregar: " + ex.Message, "danger");
            }
        }

        protected void gvDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int index = int.Parse(e.CommandArgument.ToString());
                List<DetalleCompra> lista = (List<DetalleCompra>)Session["listaCompra"];

                // Borrar de la lista temporal
                lista.RemoveAt(index);

                Session["listaCompra"] = lista;
                cargarGvDetalle();
            }
        }

        private void cargarGvDetalle()
        {
            List<DetalleCompra> lista = (List<DetalleCompra>)Session["listaCompra"];

            if(lista == null)
            {
                lista = new List<DetalleCompra>();
            }
            gvDetalle.DataSource = lista;
            gvDetalle.DataBind();

            // Calcular total
            decimal total = 0;
            foreach (var item in lista)
            {
                total += item.SubTotal;
            }
            lblTotal.Text = total.ToString("C");
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                List<DetalleCompra> lista = (List<DetalleCompra>)Session["listaCompra"];

                // Validar que haya items
                if (lista == null || lista.Count == 0)
                {
                    mostrarToast("Debe agregar al menos un producto.", "warning");
                    return;
                }

                // Inicializo objeto compra
                Compra compra = new Compra();
                compra.Proveedor = new Proveedor();
                compra.Proveedor.Id = int.Parse(ddlProveedor.SelectedValue);

                // Usuario de la session
                compra.Usuario = (Usuario)Session["usuario"];

                compra.Fecha = DateTime.Now;
                compra.Detalles = lista;

                // Calculo de total
                decimal total = 0;
                foreach (var item in lista) total += item.SubTotal;
                compra.Total = total;

                // Guardo en DB
                CompraNegocio negocio = new CompraNegocio();
                negocio.agregar(compra);

                // Limpio y aviso
                Session["listaCompra"] = null; // Carrito vaciado
                mostrarToast("¡Compra registrada exitosamente!", "success");

                // Limpio pantalla
                ddlProveedor.SelectedIndex = 0;
                lblTotal.Text = "$0.00";
                cargarGvDetalle();
            }
            catch (Exception ex)
            {
                mostrarToast("Error al guardar: " + ex.Message, "danger");
            }
        }

        private void mostrarToast(string mensaje, string tipo)
        {
            string script = $"mostrarToast('{mensaje}', '{tipo}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ToastJS", script, true);
        }
    }
}