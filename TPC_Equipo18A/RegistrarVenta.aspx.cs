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
                Session["error"] = ex.Message;
                Response.Redirect("Error.aspx");
            }
        }
    }
} 