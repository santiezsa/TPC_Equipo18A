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
                // Si vengo de un redirect después de registrar la venta
                if (Session["mensajeExito"] != null)
                {
                    mostrarToast(Session["mensajeExito"].ToString(), "success");
                    Session["mensajeExito"] = null;
                }

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
        
        protected void gvDetalleVenta_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                // Traigo la lista actual desde Session
                List<DetalleVenta> detalle = DetalleActual;

                if (index >= 0 && index < detalle.Count)
                {
                    detalle.RemoveAt(index);  
                    DetalleActual = detalle;     
                    cargarDetalle();           
                }
            }
        }
        
        protected void btnAgregarProductoVenta_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlProductoVenta.SelectedIndex <= 0)
                    return;

                if (!int.TryParse(txtCantidadVenta.Text, out int cantidadNueva) || cantidadNueva <= 0)
                    return;

                int idProducto = int.Parse(ddlProductoVenta.SelectedValue);

                ProductoNegocio productoNegocio = new ProductoNegocio();

                Producto producto = productoNegocio.buscarPorId(idProducto);

                decimal precioVenta = productoNegocio.ObtenerPrecioVenta(idProducto);

                List<DetalleVenta> detalle = DetalleActual;

                DetalleVenta existente = detalle.Find(d => d.Producto.Id == idProducto);

                int cantidadTotalSolicitada = cantidadNueva + (existente?.Cantidad ?? 0);

                // Validar stock
                int stockActual;
                if (!productoNegocio.HayStockDisponible(idProducto, cantidadTotalSolicitada, out stockActual))
                {
                    mostrarToast($"No hay stock suficiente. Disponible: {stockActual}.", "warning");
                    return;
                }

                if (existente != null)
                {
                    existente.Cantidad += cantidadNueva;
                }
                else
                {
                    DetalleVenta nuevo = new DetalleVenta
                    {
                        Producto = producto,
                        Cantidad = cantidadNueva,
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

        protected void btnConfirmarVenta_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlClienteVenta.SelectedIndex <= 0)
                {
                    mostrarToast("Debe seleccionarun cliente.", "warning");
                    return;
                }

                if (DetalleActual.Count == 0)
                {
                    mostrarToast("Debe agregar productos a la venta.", "warning");
                    return;
                }

                Venta venta = new Venta();
                venta.Cliente = new Cliente();
                venta.Cliente.Id = int.Parse(ddlClienteVenta.SelectedValue);
                venta.Fecha = DateTime.Now;
                venta.Activo = true;
                venta.Detalles = DetalleActual;

                if (Session["usuario"] == null)
                {
                    mostrarToast("Su sesión expiró. Vuelva a iniciar sesión.", "warning");
                    return;
                }
                venta.Usuario = (Usuario)Session["usuario"]; ; 

                // Calculo total
                venta.Total = venta.Detalles.Sum(importe => importe.Subtotal);

                // Registro venta en la base
                VentaNegocio ventaNegocio = new VentaNegocio(); 
                ventaNegocio.registrar(venta);

                // Limpio carrito
                Session["DetalleVenta"] = null;

                // Seteo mensaje de éxito y redirijo
                Session["mensajeExito"] = "Venta registrada correctamente.";
                Response.Redirect("RegistrarVenta.aspx", false);
                return;

            } catch(Exception ex)
            {
                mostrarToast("Error al registrar venta: " + ex.Message, "danger");
            }
        }

        private void mostrarToast(string mensaje, string tipo)
        {
            string script = $"mostrarToast('{mensaje}', '{tipo}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ToastJS", script, true);
        }
    }
} 