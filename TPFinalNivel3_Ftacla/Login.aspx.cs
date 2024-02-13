using dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

namespace TPFinalNivel3_Ftacla
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
            protected void btnLogin_Click(object sender, EventArgs e)
            {
                Usuario usuario = new Usuario();
            UsuarioNegocio negocio = new UsuarioNegocio();
                try
                {
                    if (Validacion.validaTextoVacio(txtEmail) || Validacion.validaTextoVacio(txtPassword))
                    {
                        Session.Add("error", "Debes completar ambos campos...");
                        Response.Redirect("Error.aspx");
                    }

                usuario.Email = txtEmail.Text;
                usuario.Pass = txtPassword.Text;
                    if (negocio.Login(usuario))
                    {
                        Session.Add("usuario", usuario);
                        Response.Redirect("Default.aspx", false);
                    }
                    else
                    {
                        Session.Add("error", "Usuario o Contraseña incorrectos");
                        Response.Redirect("Error.aspx", false);
                    }
                }
                catch (System.Threading.ThreadAbortException ex) {
            
            }
                catch (Exception ex)
                {
                    Session.Add("error", ex.ToString());
                    Response.Redirect("Error.aspx");
                }
            }

            private void Page_Error(object sender, EventArgs e)
            {
                Exception exc = Server.GetLastError();


                Session.Add("error", exc.ToString());
                //Response.Redirect("Error.aspx");
                Server.Transfer("Error.aspx");
            }
        }
    }