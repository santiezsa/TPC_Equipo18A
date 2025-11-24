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
    public partial class AjusteStock : System.Web.UI.Page
    {
        ProductoNegocio productoNegocio = new ProductoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarProductos();

                // Si venimos desde la gv con un ID preseleccionado
                if (Request.QueryString["id"] != null)
                {
                    ddlProducto.SelectedValue = Request.QueryString["id"];
                }
            }
        }

        private void cargarProductos()
        {
            //ProductoNegocio productoNegocio = new ProductoNegocio();

            ddlProducto.DataSource = productoNegocio.listar();
            ddlProducto.DataTextField = "Nombre";
            ddlProducto.DataValueField = "Id";
            ddlProducto.DataBind();
            ddlProducto.Items.Insert(0, new ListItem("Seleccione producto...", ""));
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();
                if (!Page.IsValid) return;

                // Obtengo los datos del form
                int idProducto = int.Parse(ddlProducto.SelectedValue);
                int cantidad = int.Parse(txtCantidad.Text);
                string motivo = txtMotivo.Text;
                bool esIngreso = (ddlTipo.SelectedValue == "SUM"); // lo traigo desde el dropdown

                // Validacion previa de cantidades de stock
                if (!esIngreso)
                {
                    // Busco el producto para saber su stock actual
                    Producto producto = productoNegocio.buscarPorId(idProducto);

                    if(producto.StockActual < cantidad)
                    {
                        mostrarToast("No se puede realizar el ajuste. La cantidad a restar excede el stock actual.", "warning");
                        return;
                    }

                }

                // Obtengo datos del usuario
                int idUsuario = 1;
                if (Session["usuario"] != null)
                {
                    idUsuario = ((Usuario)Session["usuario"]).Id;
                }

                // Ajusto stock
                productoNegocio.ajustarStock(idProducto, cantidad, motivo, idUsuario, esIngreso);

                // Redireccion
                Response.Redirect("GestionProductos.aspx");
            }
            catch (Exception ex)
            {
                mostrarToast("Error al ajustar el stock: " + ex.Message, "danger");
            }
        }

        private void mostrarToast(string mensaje, string tipo)
        {
            string script = $"mostrarToast('{mensaje}', '{tipo}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ToastJS", script, true);
        }


    }
}