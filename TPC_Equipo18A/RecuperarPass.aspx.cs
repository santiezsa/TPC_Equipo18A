using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Equipo18A
{
    public partial class RecuperarPass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
                // Validacion de que el mail existe
                if (!negocio.existeEmail(txtEmail.Text))
                {
                    lblMensaje.Text = "El email ingresado no existe.";
                    lblMensaje.CssClass = "d-block text-center mt-3 text-danger font-weight-bold";
                    return;
                }

                // Generar nueva pass random
                string nuevaPass = Guid.NewGuid().ToString().Substring(0, 8);

                // Piso password vieja en DB
                negocio.actualizarPassword(txtEmail.Text, nuevaPass);

                // Envio mail con nueva clave
                EmailService emailService = new EmailService();

                string cuerpo = $@"
                <div style='font-family: Arial, sans-serif; color: #333;'>
                    <h2 style='color: #007bff;'>Recuperación de Cuenta</h2>
                    <p>Hola,</p>
                    <p>Hemos recibido una solicitud para restablecer tu contraseña.</p>
                    <hr>
                    <p>Tu nueva contraseña temporal es: <strong style='font-size: 1.2em;'>{nuevaPass}</strong></p>
                    <hr>
                    <p>Por favor, iniciá sesión y cambiala lo antes posible.</p>
                    <p style='font-size: 0.8em; color: #666;'>Si no fuiste vos, contactá al administrador.</p>
                </div>";

                emailService.armarCorreo(txtEmail.Text, "Contraseña Restablecida", cuerpo);
                emailService.enviarEmail();

                // Mensaje de confirmacion
                lblMensaje.Text = "¡Listo! Contraseña enviada a tu correo.";
                lblMensaje.CssClass = "d-block text-center mt-3 text-success font-weight-bold";

                // Deshabilito para evitar doble envío
                btnRecuperar.Enabled = false;
                btnRecuperar.Text = "Enviado";
                btnRecuperar.CssClass = "btn btn-secondary btn-block font-weight-bold";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.CssClass = "d-block text-center mt-3 text-danger font-weight-bold";
            }
        }
    }
}