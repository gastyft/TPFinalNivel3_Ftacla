using dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinalNivel3_Ftacla
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           /*  if (!(Page is Login))
              {
                  if (!Seguridad.sesionActiva(Session["usuario"])) /// SI O SI TE OBLIGA A LOGUEAR PARA VER TODO
                      Response.Redirect("Login.aspx", false);
              }
             */ 

            {

            }



        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Eliminar la sesión del usuario
            Session.Remove("usuario");

            // Redireccionar a la página de inicio
            Response.Redirect("~/Default.aspx");
        }
    }
}