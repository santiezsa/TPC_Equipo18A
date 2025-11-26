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
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuario"] != null)
                {
                    Usuario user = (Usuario)Session["usuario"];
                    txtUser.Text = user.Username;
                    txtPerfil.Text = user.Perfil.ToString();
                    txtEmail.Text = user.Email;
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();
                if (!Page.IsValid) return;

                // Obtiene usuario actual de la sesion
                Usuario userSesion = (Usuario)Session["usuario"];

                // Valido que la pass actual sea correcta
                if (txtPassActual.Text != userSesion.Password)
                {
                    mostrarToast("La contraseña actual es incorrecta.", "warning");
                    return;
                }

                // Actualiza datos en objeto
                if (!string.IsNullOrEmpty(txtPassNueva.Text))
                {
                    userSesion.Password = txtPassNueva.Text;
                }

                userSesion.Email = txtEmail.Text;

                // Guarda en DB
                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.modificar(userSesion);

                // Actualiza session
                Session["usuario"] = userSesion;

                mostrarToast("Perfil actualizado correctamente.", "success");
            }
            catch (Exception ex)
            {
                mostrarToast("Error al actualizar: " + ex.Message, "danger");
            }
        }

        private void mostrarToast(string mensaje, string tipo)
        {
            string script = $"mostrarToast('{mensaje}', '{tipo}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ToastJS", script, true);
        }
    }
}