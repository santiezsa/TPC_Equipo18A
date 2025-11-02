using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;

namespace TPC_Equipo18A
{
    public partial class FormularioMarca : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Si es edicion
                if (Request.QueryString["id"] != null)
                {
                    lblTitulo.Text = "Editar Marca"; // Cambio el titulo

                    // Cargo los datos de la marca a editar
                    int id = int.Parse(Request.QueryString["id"]);
                    MarcaNegocio negocio = new MarcaNegocio();
                    Marca marcaSeleccionada = negocio.buscarPorId(id);

                    txtDescripcion.Text = marcaSeleccionada.Descripcion;

                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Valido la pagina
            Page.Validate(); // para el requiredfieldvalidator
            if(!Page.IsValid)
            {
                return;
            }

            MarcaNegocio negocio = new MarcaNegocio();
            Marca nueva = new Marca();
            nueva.Descripcion = txtDescripcion.Text;

            // Alta o modif?
            if(Request.QueryString["id"] != null)
            {
                // Modificacion
                nueva.Id = int.Parse(Request.QueryString["id"]);
                negocio.modificar(nueva);
            }
            else
            {
                // Alta
                negocio.agregar(nueva);
            }

            Response.Redirect("GestionMarcas.aspx");
        }
    }
}