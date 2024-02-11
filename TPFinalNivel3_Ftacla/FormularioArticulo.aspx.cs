using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using dominio;
using Dominio;
using Negocio;

namespace TPFinalNivel3_Ftacla
{
    public partial class FormularioArticulo : System.Web.UI.Page
    {
      

        public bool ConfirmaEliminacion { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false;
            ConfirmaEliminacion = false;
            try
            {
              
                if (!IsPostBack)
                {
                    CategoriaNegocio negocioC = new CategoriaNegocio();
                    List<Categoria> listaC = negocioC.listarC();

                    MarcaNegocio negocioM = new MarcaNegocio();
                    List<Marca> listaM = negocioM.listarM();

                    ddlCategoria.DataSource = listaC;
                    ddlCategoria.DataValueField = "Id";
                    ddlCategoria.DataTextField = "DescripcionC";
                    ddlCategoria.DataBind();

                    ddlMarca.DataSource = listaM;
                    ddlMarca.DataValueField = "Id";
                    ddlMarca.DataTextField = "DescripcionM";
                    ddlMarca.DataBind();
                }

                //configuración si estamos modificando.
                string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
                if (id != "" && !IsPostBack)
                {
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    //List<Pokemon> lista = negocio.listar(id);
                    //Pokemon seleccionado = lista[0];
                    Articulo seleccionado = (negocio.listarId(id))[0];

                    //guardo pokemon seleccionado en session
                    Session.Add("ArtiSeleccionado", seleccionado);

                    //pre cargar todos los campos...
                    txtId.Text = id;
                    txtNombre.Text = seleccionado.Nombre;
                    txtDescripcion.Text = seleccionado.Descripcion;
                    txtImagenUrl.Text = seleccionado.Imagen;
                    txtPrecio.Text = seleccionado.Precio.ToString();
                    txtCodigo1.Text = seleccionado.Codigo;
                    ddlCategoria.SelectedValue = seleccionado.DescripcionC.Id.ToString();
                    ddlMarca.SelectedValue = seleccionado.DescripcionM.Id.ToString();
                    txtImagenUrl_TextChanged(sender, e);

                    
                   
                }

            }
            catch (Exception ex)
            {
                 Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            //    throw; 
           
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Articulo nuevo = new Articulo();
                ArticuloNegocio negocio = new ArticuloNegocio();

                nuevo.Precio = int.Parse(txtPrecio.Text);
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.Imagen = txtImagenUrl.Text;
                nuevo.Codigo = txtCodigo1.Text; 
                nuevo.DescripcionC = new Categoria();    
                nuevo.DescripcionC.Id = int.Parse(ddlCategoria.SelectedValue);
                nuevo.DescripcionM = new Marca();
                nuevo.DescripcionM.Id = int.Parse(ddlMarca.SelectedValue);

                if (Request.QueryString["id"] != null)
                {
                    nuevo.Id = int.Parse(txtId.Text);
                    negocio.modificar(nuevo);
                }
                else
                    negocio.agregar(nuevo);


                Response.Redirect("Default.aspx", false);
            }
            catch (Exception ex)
            {
                 Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            
                 
                }
        }

        protected void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            imgArticulo.ImageUrl = txtImagenUrl.Text;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ConfirmaEliminacion = true;
        }

        protected void btnConfirmaEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkConfirmaEliminacion.Checked)
                {
                   ArticuloNegocio negocio = new ArticuloNegocio();
                    negocio.eliminar(int.Parse(txtId.Text));
                 
                    Response.Redirect("Default.aspx");
                }
            }
            catch (Exception ex)
            {
                  Session.Add("error", ex.ToString());
                 Response.Redirect("Error.aspx");
                 
            }
        }

      
    }
}