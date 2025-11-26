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

                // Obtengo ID usuario logeado
                Usuario userSesion = (Usuario)Session["usuario"];

                // Busco datos de la DB
                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario userBaseDatos = negocio.buscarPorId(userSesion.Id);

                // Valido pass actual
                if (txtPassActual.Text.Trim() != userBaseDatos.Password.Trim())
                {
                    mostrarToast("La contraseña actual es incorrecta.", "warning");
                    return;
                }

                // Actualizo datos
                if (!string.IsNullOrEmpty(txtPassNueva.Text))
                {
                    userBaseDatos.Password = txtPassNueva.Text;
                }

                userBaseDatos.Email = txtEmail.Text;

                // Guardo en DB
                negocio.modificar(userBaseDatos);

                // Actualizo datos en session
                Session["usuario"] = userBaseDatos;

                // Limpio los campos de password
                txtPassActual.Text = "";
                txtPassNueva.Text = "";
                txtPassRepetir.Text = "";

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