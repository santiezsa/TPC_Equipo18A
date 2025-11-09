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
    public partial class FormularioProveedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Si es edicion
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    lblTitulo.Text = "Editar Proveedor"; // Cambio el titulo

                    // Cargo los datos del proveedor a editar
                    ProveedorNegocio negocio = new ProveedorNegocio();
                    Proveedor seleccionado = negocio.buscarPorId(id);

                    // Si encuentra
                    if (seleccionado != null)
                    {
                        hfId.Value = seleccionado.Id.ToString();
                        txtRazonSocial.Text = seleccionado.RazonSocial;
                        txtCUIT.Text = seleccionado.CUIT;
                        txtNombre.Text = seleccionado.Nombre;
                        txtEmail.Text = seleccionado.Email;
                        txtTelefono.Text = seleccionado.Telefono;
                    }
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Valido la pagina
            Page.Validate(); // para el requiredfieldvalidator
            if (!Page.IsValid)
            {
                return;
            }

            ProveedorNegocio negocio = new ProveedorNegocio();
            Proveedor proveedor = new Proveedor();

            try
            {
                proveedor.RazonSocial = txtRazonSocial.Text;
                proveedor.CUIT = txtCUIT.Text;
                proveedor.Nombre = txtNombre.Text;
                proveedor.Email = txtEmail.Text;
                proveedor.Telefono = txtTelefono.Text;

                // Alta o modif?
                if (Request.QueryString["id"] != null)
                {
                    // Modificacion
                    proveedor.Id = int.Parse(hfId.Value);
                    negocio.modificar(proveedor);
                }
                else
                {
                    // Alta
                    negocio.agregar(proveedor);
                }

                Response.Redirect("GestionProveedores.aspx", false);
            }
            catch (Exception ex)
            {
                Session["error"] = ex.Message;
                Response.Redirect("Error.aspx");
            }
        }

    }
}