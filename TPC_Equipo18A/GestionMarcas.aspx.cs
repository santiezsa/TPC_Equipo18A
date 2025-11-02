using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;

namespace TPC_Equipo18A
{
    public partial class GestionMarcas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarGv();
            }
        }

        protected void gvMarcas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int id = int.Parse(e.CommandArgument.ToString());

                MarcaNegocio negocio = new MarcaNegocio();
                try
                {
                    negocio.eliminar(id);
                }
                catch (Exception ex)
                {
                    //Session["error"] = ex.Message;
                    //Response.Redirect("Error.aspx");
                    throw ex;
                }
                cargarGv();
            }
        }

        private void cargarGv()
        {
            MarcaNegocio negocio = new MarcaNegocio();
            gvMarcas.DataSource = negocio.listar();
            gvMarcas.DataBind();
        }
    }
}