using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;
namespace TPFinalNivel3_Ftacla
{
    public partial class ListaArticulos : System.Web.UI.Page
    {
        public bool FiltroAvanzado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
           

            FiltroAvanzado = chkAvanzado.Checked;
            if (!IsPostBack)
            {
                CargarDatos();
            }
        }

        protected void dgvArticulo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvArticulo.PageIndex = e.NewPageIndex;
            CargarDatos();
        }

        private void CargarDatos()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Session["listaArticulos"] = negocio.listar();
            dgvArticulo.DataSource = Session["listaArticulos"];
            dgvArticulo.DataBind();
        }

        protected void dgvArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvArticulo.SelectedDataKey.Value.ToString();
            Response.Redirect("FormularioArticulo.aspx?id=" + id);
        }

        protected void filtro_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> lista = (List<Articulo>)Session["listaArticulos"];
            List<Articulo> listaFiltrada = lista.FindAll(x => x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));
            dgvArticulo.DataSource = listaFiltrada;
            dgvArticulo.DataBind();
        }

        protected void chkAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            FiltroAvanzado = chkAvanzado.Checked;
            txtFiltro.Enabled = !FiltroAvanzado;
        }

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriterio.Items.Clear();
            if (ddlCampo.SelectedItem.ToString() == "Precio")
            {
                ddlCriterio.Items.Add("Igual a");
                ddlCriterio.Items.Add("Mayor a");
                ddlCriterio.Items.Add("Menor a");
            }
            else
            {
                ddlCriterio.Items.Add("Contiene");
                ddlCriterio.Items.Add("Comienza con");
                ddlCriterio.Items.Add("Termina con");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                dgvArticulo.DataSource = negocio.filtrar(
                    ddlCampo.SelectedItem.ToString(),
                    ddlCriterio.SelectedItem.ToString(),
                    txtFiltroAvanzado.Text);
                dgvArticulo.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }
    }
}