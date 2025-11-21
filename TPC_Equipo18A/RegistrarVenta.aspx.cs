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
    public partial class RegistrarVenta : System.Web.UI.Page
    {
        ProductoNegocio productoNegocio = new ProductoNegocio();
        ClienteNegocio clienteNegocio = new ClienteNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ddlProductoVenta.DataSource = productoNegocio.listar();
                    ddlProductoVenta.DataTextField = "Nombre";
                    ddlProductoVenta.DataValueField = "Id";
                    ddlProductoVenta.DataBind();
                    ddlProductoVenta.Items.Insert(0, new ListItem("Seleccione un Producto", ""));

                    ddlClienteVenta.DataSource = clienteNegocio.listar();
                    ddlClienteVenta.DataTextField = "NombreCompleto";
                    ddlClienteVenta.DataValueField = "Id";
                    ddlClienteVenta.DataBind();
                    ddlClienteVenta.Items.Insert(0, new ListItem("Seleccione un Cliente", ""));
                }
            }
            catch (Exception ex)
            {
                mostrarToast("Error al cargar formulario: " + ex.Message, "danger");
            }
        }

        private List<DetalleVenta> DetalleActual
        {
            get
            {
                if (Session["DetalleVenta"] == null)
                    Session["DetalleVenta"] = new List<DetalleVenta>();
                return (List<DetalleVenta>)Session["DetalleVenta"];
            }
            set
            {
                Session["DetalleVenta"] = value;
            }
        }
        private void cargarDetalle()
        {
            List<DetalleVenta> detalle = DetalleActual;

            gvDetalleVenta.DataSource = detalle;
            gvDetalleVenta.DataBind();

            // Calcular total
            decimal total = 0;
            foreach (DetalleVenta item in detalle)
                total += item.Subtotal;

            lblTotalVenta.Text = total.ToString("C2");
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlProductoVenta.SelectedIndex <= 0)
                    return;

                if (!int.TryParse(txtCantidadVenta.Text, out int cantidad) || cantidad <= 0)
                    return;

                int idProducto = int.Parse(ddlProductoVenta.SelectedValue);

                ProductoNegocio productoNegocio = new ProductoNegocio();

                Producto producto = productoNegocio.buscarPorId(idProducto);

                decimal precioVenta = productoNegocio.ObtenerPrecioVenta(idProducto);

                List<DetalleVenta> detalle = DetalleActual;

                DetalleVenta existente = detalle.Find(d => d.Producto.Id == idProducto);

                if (existente != null)
                {
                    existente.Cantidad += cantidad;
                }
                else
                {
                    DetalleVenta nuevo = new DetalleVenta
                    {
                        Producto = producto,
                        Cantidad = cantidad,
                        PrecioUnitario = precioVenta
                    };

                    detalle.Add(nuevo);
                }

                DetalleActual = detalle; // guardar de nuevo en Session
                cargarDetalle();
            } 
            catch (Exception ex)
            {
                mostrarToast("Error al agregar producto: " + ex.Message, "danger");
            }
        }

        private void mostrarToast(string mensaje, string tipo)
        {
            string script = $"mostrarToast('{mensaje}', '{tipo}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ToastJS", script, true);
        }
    }
} 