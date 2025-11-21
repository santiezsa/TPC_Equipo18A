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
                mostrarToast("Error al cargar formulario: " + ex.Message, "danger"); //Chequear mensaje
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

        protected void btnAgregar_Click(object sender, EventArgs e)
        {

        }

        private void mostrarToast(string mensaje, string tipo)
        {
            string script = $"mostrarToast('{mensaje}', '{tipo}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ToastJS", script, true);
        }
    }
} 