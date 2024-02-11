using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinalNivel3_Ftacla
{
    public partial class VerDetalle : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Establece la cadena de conexión a tu base de datos

                // Reemplaza con tu cadena de conexión

                // Crea una conexión SQL
                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["cadenaConexion"]))
                {
                    // Define la consulta SQL para obtener el nombre de un cliente por su ID
                    string consulta = @"SELECT A.[Id], 
                                               A.[Codigo], 
                                               A.[Nombre], 
                                               A.[Descripcion] AS 'Descripcion_Articulo', 
                                               A.[ImagenUrl], 
                                               M.[Descripcion] AS 'Descripcion_Marca', 
                                               C.[Descripcion] AS 'Descripcion_Categoria', 
                                               A.[IdMarca], 
                                               A.[IdCategoria], 
                                               A.[Precio] 
                                               FROM [CATALOGO_WEB_DB].[dbo].[ARTICULOS] A 
                                               INNER JOIN [CATALOGO_WEB_DB].[dbo].[MARCAS] M ON A.[IdMarca] = M.[Id] 
                                               INNER JOIN [CATALOGO_WEB_DB].[dbo].[CATEGORIAS] C ON A.[IdCategoria] = C.[Id] 
                                               WHERE A.[Id] = @ID";
                    // Crea un comando SQL
                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        if (Request.QueryString["id"] != null)
                        {
                            int ID;
                            if (int.TryParse(Request.QueryString["id"], out ID))
                            {


                                // Establece el parámetro ID
                                comando.Parameters.AddWithValue("@ID", ID); // Reemplaza 123 con el ID del cliente que deseas

                                // Abre la conexión
                                conexion.Open();

                                // Ejecuta la consulta y obtén el resultado
                                SqlDataReader reader = comando.ExecuteReader();
                                if (reader.Read())
                                {
                                    // Asigna el valor al control Label
                                    lblNombre.Text = reader["Nombre"].ToString();
                                    string ImagenArti1= reader["ImagenUrl"].ToString();
                                    DescripcionArticulo.Text = reader["Descripcion_Articulo"].ToString();
                                    DescripcionMarca.Text = reader["Descripcion_Marca"].ToString();
                                    DescripcionCate.Text = reader["Descripcion_Categoria"].ToString();
                                    Precio.Text = Convert.ToDecimal(reader["Precio"]).ToString("0.00");
                                    Codigo.Text = reader["Codigo"].ToString();
                                    ImagenArti.ImageUrl= ImagenArti1;
                                }
                                else
                                {
                                    lblNombre.Text = "Cliente no encontrado";
                                }

                                // Cierra la conexión
                                conexion.Close();
                            }
                        }
                    }
                }
            }
        }
    }
}