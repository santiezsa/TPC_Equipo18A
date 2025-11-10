using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Equipo18A
{
    public partial class FormularioCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Si es edicion
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    lblTitulo.Text = "Editar Cliente"; // Cambio el titulo

                    // Cargo los datos del cliente a editar
                    ClienteNegocio negocio = new ClienteNegocio();
                    Cliente seleccionado = negocio.buscarPorId(id);

                    // Si encuentra
                    if (seleccionado != null)
                    {
                        hfId.Value = seleccionado.Id.ToString();
                        txtNombre.Text = seleccionado.Nombre;
                        txtApellido.Text = seleccionado.Apellido;
                        txtEmail.Text = seleccionado.Email;
                        txtTelefono.Text = seleccionado.Telefono;
                        txtDireccion.Text = seleccionado.Direccion;
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

            ClienteNegocio negocio = new ClienteNegocio();
            Cliente cliente = new Cliente();

            try
            {
                cliente.Nombre = txtNombre.Text;
                cliente.Apellido = txtApellido.Text;
                cliente.Email = txtEmail.Text;
                cliente.Telefono = txtTelefono.Text;
                cliente.Direccion = txtDireccion.Text;

                // Alta o modif?
                if (Request.QueryString["id"] != null)
                {
                    // Modificacion
                    cliente.Id = int.Parse(hfId.Value);
                    negocio.modificar(cliente);
                }
                else
                {
                    // Alta
                    negocio.agregar(cliente);
                }

                Response.Redirect("GestionClientes.aspx", false);
            }
            catch (Exception ex)
            {
                Session["error"] = ex.Message;
                Response.Redirect("Error.aspx");
            }
        }
    }
}