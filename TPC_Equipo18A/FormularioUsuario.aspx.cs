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
    public partial class FormularioUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    // Si es edicion
                    if (Request.QueryString["id"] != null)
                    {
                        int id = int.Parse(Request.QueryString["id"]);
                        lblTitulo.Text = "Editar Usuario"; // Cambio el título

                        // Cargo los datos del usuario a editar
                        UsuarioNegocio negocio = new UsuarioNegocio();
                        Usuario seleccionado = negocio.buscarPorId(id);

                        // Si encuentra
                        if (seleccionado != null)
                        {
                            hfId.Value = seleccionado.Id.ToString();
                            txtUsername.Text = seleccionado.Username;
                            txtPassword.Text = seleccionado.Password;

                            // Selecciono perfil con dropdown
                            // enum a int y luego a string para que coincida con el value del listItem
                            ddlPerfil.SelectedValue = ((int)seleccionado.Perfil).ToString();
                        }
                    }
                }
            }


        }

        private void mostrarToast(string mensaje, string tipo)
        {
            string script = $"mostrarToast('{mensaje}', '{tipo}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ToastJS", script, true);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Valido la página
            Page.Validate();
            if (!Page.IsValid)
            {
                return;
            }

            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario usuario = new Usuario();

            try
            {
                usuario.Username = txtUsername.Text;
                usuario.Password = txtPassword.Text;
                // string del dropdown al Enum Perfil
                usuario.Perfil = (Perfil)int.Parse(ddlPerfil.SelectedValue);

                // activo siempre (TODO: checkbox para desactivar)
                usuario.Activo = true;

                // Alta o modif?
                if (Request.QueryString["id"] != null)
                {
                    // modif
                    usuario.Id = int.Parse(hfId.Value);
                    negocio.modificar(usuario);
                }
                else
                {
                    // alta
                    negocio.agregar(usuario);
                }

                Response.Redirect("GestionUsuarios.aspx", false);
            }
            catch (Exception ex)
            {
                mostrarToast("Error al guardar: " + ex.Message, "danger");
            }
        }
    }
}